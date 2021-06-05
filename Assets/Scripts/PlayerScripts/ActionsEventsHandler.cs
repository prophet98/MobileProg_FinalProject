using System;
using System.Linq;
using DamageScripts;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ActionsEventsHandler : MonoBehaviour
{
    private PlayerInput _playerInput;
    private Animator _animator;
    private PlayerWeaponComponent _playerWeaponComponent;
    public PlayerDamageAnimationEvents PlayerDamageAnimationEvents { get; private set; }
    private SkillSlotsController _skillSlotsController;

    private static readonly int DebugAttack = Animator.StringToHash("DebugAttack");
    private static readonly int DebugRun = Animator.StringToHash("DebugRun");
    public static int comboCounter;
    private GameObject _hud;
    private Button[] _playerButtons;
    private bool _isLowerSlotSkillNotNull;
    private bool _isUpperSlotSkillNotNull;

    private void Awake() //sets up all the needed components.
    {
        _playerInput = new PlayerInput();
        _playerInput.Enable();
        _animator = GetComponentInChildren<Animator>();
        _playerWeaponComponent = GetComponentInChildren<PlayerWeaponComponent>();
        PlayerDamageAnimationEvents = GetComponentInChildren<PlayerDamageAnimationEvents>();
        _skillSlotsController = GetComponent<SkillSlotsController>();
        _hud = GameObject.FindGameObjectWithTag("HUD");
    }

    private void OnEnable() //subscribes to the input actions created with the new Unity input system.
    {
        _playerInput.PlayerActions.Attack.started += OnAttackStarted;
        _playerInput.PlayerActions.Dash.performed += OnDashPerformed;
        _playerInput.PlayerActions.Skill.performed += OnSkillPerformed;
        _playerInput.PlayerActions.Move.performed += OnMovePerformed;
        _playerInput.PlayerActions.Move.canceled += OnMoveCanceled;
        PlayerDamageAnimationEvents.OnDamagedSuccess += ApplyDamage;
        PlayerDamageAnimationEvents.OnDamagedFail += CancelDamage;

        AiDamageAnimationEvents.OnPlayerDamagedSuccess += ReceiveDamage;

        _playerButtons = _hud.GetComponentsInChildren<Button>();

        comboCounter = 0;
    }

    private void ReceiveDamage(int damage)
    {
        GetComponent<IDamageable<int>>().RemoveHealth(damage);
    }


    private void ApplyDamage() //removes health to all the enemies in range
    {
        _playerWeaponComponent.weaponParticleInstance.GetComponent<ParticleSystem>().Stop();
        _playerWeaponComponent.weaponParticleInstance.GetComponent<ParticleSystem>().Play();
        foreach (var enemy in _playerWeaponComponent.triggerList.ToList())
        {
            enemy.GetComponentInParent<IDamageable<int>>().RemoveHealth(_playerWeaponComponent.weaponDamage);
        }
    }

    private void CancelDamage()
    {
        comboCounter = 0;
        _animator.ResetTrigger(DebugAttack);
    }

    private void OnAttackStarted(InputAction.CallbackContext context) //update the combo counter.
    {
        _animator.SetTrigger(DebugAttack);
        if (_playerWeaponComponent.triggerList.Count > 0)
        {
            comboCounter++;
        }
        else
        {
            comboCounter = 0;
            _animator.ResetTrigger(DebugAttack);
        }
    }

    private void
        OnDashPerformed(
            InputAction.CallbackContext obj) //on dash and skill performed, update skill button state and play the correct sound. 
    {
        if (_skillSlotsController.lowerSlotSkill.state == BaseSkill.AbilityState.Ready)
        {
            _skillSlotsController.lowerSlotSkill.Activate(gameObject);
            _skillSlotsController.lowerSlotSkill.state = BaseSkill.AbilityState.Activated;
            _skillSlotsController.SetUpSkillTimers();
            SoundManager.instance?.Play(_skillSlotsController.lowerSlotSkill.skillSound);
        }
    }

    private void OnSkillPerformed(InputAction.CallbackContext obj)
    {
        if (_skillSlotsController.upperSlotSkill.state == BaseSkill.AbilityState.Ready)
        {
            _skillSlotsController.upperSlotSkill.Activate(gameObject);
            _skillSlotsController.upperSlotSkill.state = BaseSkill.AbilityState.Activated;
            _skillSlotsController.SetUpSkillTimers();
            SoundManager.instance?.Play(_skillSlotsController.upperSlotSkill.skillSound);
        }
    }

    private void OnMovePerformed(InputAction.CallbackContext obj) //handle player animation
    {
        _animator.ResetTrigger(DebugAttack);
        _animator.SetTrigger(DebugRun);
    }

    private void OnMoveCanceled(InputAction.CallbackContext obj)
    {
        _animator.ResetTrigger(DebugRun);
    }

    private void OnDisable()
    {
        _playerInput.PlayerActions.Attack.started -= OnAttackStarted;
        _playerInput.PlayerActions.Dash.performed -= OnDashPerformed;
        _playerInput.PlayerActions.Skill.performed -= OnSkillPerformed;
        _playerInput.PlayerActions.Move.performed -= OnMovePerformed;
        _playerInput.PlayerActions.Move.canceled -= OnMoveCanceled;

        PlayerDamageAnimationEvents.OnDamagedSuccess -= ApplyDamage;
        PlayerDamageAnimationEvents.OnDamagedFail -= CancelDamage;

        AiDamageAnimationEvents.OnPlayerDamagedSuccess -= ReceiveDamage;
    }

    private void Start() //get the skills and enable the buttons on screen
    {
        _isUpperSlotSkillNotNull = _skillSlotsController.upperSlotSkill != null;
        _isLowerSlotSkillNotNull = _skillSlotsController.lowerSlotSkill != null;
        _playerButtons[1].gameObject.SetActive(_skillSlotsController.lowerSlotSkill != null);
        _playerButtons[2].gameObject.SetActive(_skillSlotsController.upperSlotSkill != null);
    }

    private void OnDestroy() //stop skill sounds if they are still being played when we are dead 
    {
        if (!_skillSlotsController.upperSlotSkill || !_skillSlotsController.lowerSlotSkill) return;
        SoundManager.instance?.StopSound(_skillSlotsController.upperSlotSkill.skillSound);
        SoundManager.instance?.StopSound(_skillSlotsController.lowerSlotSkill.skillSound);
    }

    private void LateUpdate()
    {
        UpdateButtonState();
    }

    private void UpdateButtonState() //update the state of the hud buttons based on player usage of skills.
    {
        if (_playerButtons == null) return;
        if (_playerWeaponComponent.triggerList.Count <= 0)
        {
            _playerButtons[0].interactable = false;
        }

        else if (_playerWeaponComponent.triggerList.Count > 0)
        {
            _playerButtons[0].interactable = true;
        }


        if (_isLowerSlotSkillNotNull)
        {
            _playerButtons[1].interactable = _skillSlotsController.lowerSlotSkill.state switch
            {
                BaseSkill.AbilityState.Cooldown => false,
                BaseSkill.AbilityState.Ready => true,
                _ => _playerButtons[1].interactable
            };
        }

        if (_isUpperSlotSkillNotNull)
        {
            _playerButtons[2].interactable = _skillSlotsController.upperSlotSkill.state switch
            {
                BaseSkill.AbilityState.Cooldown => false,
                BaseSkill.AbilityState.Ready => true,
                _ => _playerButtons[2].interactable
            };
        }
    }
}