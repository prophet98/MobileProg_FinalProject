using UnityEngine;
using UnityEngine.InputSystem;

public class ActionsEventsHandler : MonoBehaviour
{
    private PlayerInput _playerInput;
    private WeaponComponent _weaponComponent;
    private Animator _animator;
    private static readonly int DebugAttack = Animator.StringToHash("DebugAttack");
    private static readonly int DebugDash = Animator.StringToHash("DebugDash");
    private static readonly int DebugRun = Animator.StringToHash("DebugRun");

    public static int comboCounter;
    
    private void Awake()
    {
        _playerInput = new PlayerInput();
        _playerInput.Enable();
        _weaponComponent = GetComponentInChildren<WeaponComponent>();
        _animator = GetComponentInChildren<Animator>();
    }

    private void OnEnable()
    {
        _playerInput.PlayerActions.Attack.started += OnAttackStarted;
        _playerInput.PlayerActions.Attack.canceled += OnAttackCanceled;
        _playerInput.PlayerActions.Dash.performed += OnDashPerformed;
        _playerInput.PlayerActions.Skill.performed += OnSkillPerformed;
        _playerInput.PlayerActions.Move.performed += OnMovePerformed;
        _playerInput.PlayerActions.Move.canceled += OnMoveCanceled;

        DamageAnimationEvents.OnDamagedSuccess += ApplyDamage;
        DamageAnimationEvents.OnDamagedFail += CancelDamage;
    }

    private void ApplyDamage()
    {
        VisualDebugger.PrintText("Player Attacks!");
        // Debug.Log("Player Attacks!");
        foreach (var enemy in _weaponComponent.triggerList)
        {
            enemy.GetComponentInParent<AiController>().RemoveHealth(_weaponComponent.weaponDamage);
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
        if (_weaponComponent.isEnemyInRange)
        {
            comboCounter++;
        }
        else
        {
            comboCounter = 0;
            _animator.ResetTrigger(DebugAttack);
        }
    }
    private void OnAttackCanceled(InputAction.CallbackContext context)
    {
        
    }
    private void OnDashPerformed(InputAction.CallbackContext obj)
    {
        VisualDebugger.PrintText("Player Dashes!");
        Debug.Log("Player Dashes!");

    }
    private void OnSkillPerformed(InputAction.CallbackContext obj)
    {
        VisualDebugger.PrintText("Player used an active skill!");
        Debug.Log("Player used an active skill!");
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
        _playerInput.PlayerActions.Attack.canceled -= OnAttackCanceled;
        _playerInput.PlayerActions.Dash.performed -= OnDashPerformed;
        _playerInput.PlayerActions.Skill.performed -= OnSkillPerformed;
        _playerInput.PlayerActions.Move.performed -= OnMovePerformed;
        _playerInput.PlayerActions.Move.canceled -= OnMoveCanceled;
        
        DamageAnimationEvents.OnDamagedSuccess -= ApplyDamage;
        DamageAnimationEvents.OnDamagedFail -= CancelDamage;
    }
    
}
