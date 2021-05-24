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

    public GameObject buttons;
    private Button[] playerButtons;
    
    private void Awake()
    {
        _playerInput = new PlayerInput();
        _playerInput.Enable();
        _animator = GetComponentInChildren<Animator>();
        _playerWeaponComponent = GetComponentInChildren<PlayerWeaponComponent>();
        PlayerDamageAnimationEvents = GetComponentInChildren<PlayerDamageAnimationEvents>();
        _skillSlotsController = GetComponent<SkillSlotsController>();
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

        playerButtons = buttons.GetComponentsInChildren<Button>();

    }

    private void Start()
    {
        InvokeRepeating(nameof(UpdateButtonState), 0.0f, .5f);
    }

    private void ReceiveDamage(int damage)
    {
        GetComponent<IDamageable<int>>().RemoveHealth(damage);
    }


    public void UpdateButtonState()
    {
        if (PlayerWeaponComponent.TriggerList.Count<=0)
        {
            playerButtons[0].interactable = false;
        }

        else
        {
            playerButtons[0].interactable = true;
        }
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
        VisualDebugger.PrintText("Player Dashes!");
        if (_skillSlotsController.lowerSlotSkill.state == BaseSkill.AbilityState.Ready)
        {
            _skillSlotsController.lowerSlotSkill.Activate(gameObject);
            _skillSlotsController.lowerSlotSkill.state = BaseSkill.AbilityState.Activated;
            _skillSlotsController.SetUpSkillTimers();
            SoundManager.instance?.Play(Sound.Names.DashSound);
        }
    }
    private void OnSkillPerformed(InputAction.CallbackContext obj)
    {
        VisualDebugger.PrintText("Player used an active skill!"); 
        if (_skillSlotsController.upperSlotSkill.state == BaseSkill.AbilityState.Ready)
        {
            _skillSlotsController.upperSlotSkill.Activate(gameObject);
            _skillSlotsController.upperSlotSkill.state = BaseSkill.AbilityState.Activated;
            _skillSlotsController.SetUpSkillTimers();
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
}
