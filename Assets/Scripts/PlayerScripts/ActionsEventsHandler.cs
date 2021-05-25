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
    // private static readonly int DebugDash = Animator.StringToHash("DebugDash");
    private static readonly int DebugRun = Animator.StringToHash("DebugRun");
    public static int comboCounter;
    private GameObject _hud;
    private Button[] _playerButtons;
    
    private void Awake()
    {
        _playerInput = new PlayerInput();
        _playerInput.Enable();
        _animator = GetComponentInChildren<Animator>();
        _playerWeaponComponent = GetComponentInChildren<PlayerWeaponComponent>();
        PlayerDamageAnimationEvents = GetComponentInChildren<PlayerDamageAnimationEvents>();
        _skillSlotsController = GetComponent<SkillSlotsController>();
        _hud = GameObject.FindGameObjectWithTag("HUD");
    }

    private void OnEnable()
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


    }

    private void ReceiveDamage(int damage)
    {
        GetComponent<IDamageable<int>>().RemoveHealth(damage);
    }
    

    private void ApplyDamage()
    {
        VisualDebugger.PrintText("Player Attacks!");
        _playerWeaponComponent.weaponParticleInstance.GetComponent<ParticleSystem>().Stop();
        _playerWeaponComponent.weaponParticleInstance.GetComponent<ParticleSystem>().Play();
        // Debug.Log("Player Attacks!");
        foreach (var enemy in PlayerWeaponComponent.TriggerList.ToList())
        {
            enemy.GetComponentInParent<IDamageable<int>>().RemoveHealth(_playerWeaponComponent.weaponDamage);
        }
    }
    private void CancelDamage()
    {
        //suono di fail?
        comboCounter = 0;
        _animator.ResetTrigger(DebugAttack);
        VisualDebugger.PrintText("Player misses target!");
        // Debug.Log("Player misses target!");
    }

    private void OnAttackStarted(InputAction.CallbackContext context)
    {
        _animator.SetTrigger(DebugAttack);
        if (PlayerWeaponComponent.TriggerList.Count>0)
        {
            comboCounter++;
        }
        else
        {
            comboCounter = 0;
            _animator.ResetTrigger(DebugAttack); 
        }
    }

    private void OnDashPerformed(InputAction.CallbackContext obj)
    {
        if (_skillSlotsController.lowerSlotSkill.state == BaseSkill.AbilityState.Ready)
        {
            VisualDebugger.PrintText("Player Dashes!");
            _skillSlotsController.lowerSlotSkill.Activate(gameObject);
            _skillSlotsController.lowerSlotSkill.state = BaseSkill.AbilityState.Activated;
            _skillSlotsController.SetUpSkillTimers();
            SoundManager.instance?.Play( _skillSlotsController.lowerSlotSkill.skillSound);
        }
    }
    private void OnSkillPerformed(InputAction.CallbackContext obj)
    {

        if (_skillSlotsController.upperSlotSkill.state == BaseSkill.AbilityState.Ready)
        {
            VisualDebugger.PrintText("Player used an active skill!"); 
            _skillSlotsController.upperSlotSkill.Activate(gameObject);
            _skillSlotsController.upperSlotSkill.state = BaseSkill.AbilityState.Activated;
            _skillSlotsController.SetUpSkillTimers();
            SoundManager.instance?.Play( _skillSlotsController.upperSlotSkill.skillSound);

        }
    }

    private void OnMovePerformed(InputAction.CallbackContext obj)
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
    
    private void LateUpdate()
    {
        UpdateButtonState();
    }

    private void UpdateButtonState()
    {
        if (_playerButtons == null) return;
        if (PlayerWeaponComponent.TriggerList.Count<=0)
        {
            _playerButtons[0].interactable = false;
        }

        else if (PlayerWeaponComponent.TriggerList.Count>0)
        {
            _playerButtons[0].interactable = true;
        }

        _playerButtons[1].interactable = _skillSlotsController.lowerSlotSkill.state switch
        {
            BaseSkill.AbilityState.Cooldown => false,
            BaseSkill.AbilityState.Ready => true,
            _ => _playerButtons[1].interactable
        };
        _playerButtons[2].interactable = _skillSlotsController.upperSlotSkill.state switch
        {
            BaseSkill.AbilityState.Cooldown => false,
            BaseSkill.AbilityState.Ready => true,
            _ => _playerButtons[2].interactable
        };
    }
}
