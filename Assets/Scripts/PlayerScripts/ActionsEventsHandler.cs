using System.Linq;
using DamageScripts;
using UnityEngine;
using UnityEngine.InputSystem;

public class ActionsEventsHandler : MonoBehaviour
{
    private PlayerInput _playerInput;
    private Animator _animator;
    private PlayerWeaponComponent _playerWeaponComponent;
    private PlayerDamageAnimationEvents _playerDamageAnimationEvents;
    private SkillSlotsController _skillSlotsController;
    private static readonly int DebugAttack = Animator.StringToHash("DebugAttack");
    // private static readonly int DebugDash = Animator.StringToHash("DebugDash");
    private static readonly int DebugRun = Animator.StringToHash("DebugRun");
    

    public static int comboCounter;
    
    private void Awake()
    {
        _playerInput = new PlayerInput();
        _playerInput.Enable();
        _animator = GetComponentInChildren<Animator>();
        _playerWeaponComponent = GetComponentInChildren<PlayerWeaponComponent>();
        _playerDamageAnimationEvents = GetComponentInChildren<PlayerDamageAnimationEvents>();
        _skillSlotsController = GetComponent<SkillSlotsController>();
    }

    private void OnEnable()
    {
        _playerInput.PlayerActions.Attack.started += OnAttackStarted;
        _playerInput.PlayerActions.Dash.performed += OnDashPerformed;
        _playerInput.PlayerActions.Skill.performed += OnSkillPerformed;
        _playerInput.PlayerActions.Move.performed += OnMovePerformed;
        _playerInput.PlayerActions.Move.canceled += OnMoveCanceled;
        _playerDamageAnimationEvents.OnDamagedSuccess += ApplyDamage;
        _playerDamageAnimationEvents.OnDamagedFail += CancelDamage;
        
        AiDamageAnimationEvents.OnPlayerDamagedSuccess += ReceiveDamage;
    }

    private void ReceiveDamage(int damage)
    {
        GetComponent<IDamageable<int>>().RemoveHealth(damage);
    }

    private void ApplyDamage()
    {
        VisualDebugger.PrintText("Player Attacks!");
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
        _skillSlotsController.SetUpSkillTimers();
        if (_skillSlotsController.lowerSlotSkill.state == BaseSkill.AbilityState.Ready)
        {
            _skillSlotsController.lowerSlotSkill.Activate(gameObject);
            _skillSlotsController.lowerSlotSkill.state = BaseSkill.AbilityState.Activated;
        }
       
    }
    private void OnSkillPerformed(InputAction.CallbackContext obj)
    {
        VisualDebugger.PrintText("Player used an active skill!");
        _skillSlotsController.SetUpSkillTimers();
        if (_skillSlotsController.upperSlotSkill.state == BaseSkill.AbilityState.Ready)
        {
            _skillSlotsController.upperSlotSkill.Activate(gameObject);
            _skillSlotsController.upperSlotSkill.state = BaseSkill.AbilityState.Activated;
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
        
        _playerDamageAnimationEvents.OnDamagedSuccess -= ApplyDamage;
        _playerDamageAnimationEvents.OnDamagedFail -= CancelDamage;
        
        AiDamageAnimationEvents.OnPlayerDamagedSuccess -= ReceiveDamage;
    }
}
