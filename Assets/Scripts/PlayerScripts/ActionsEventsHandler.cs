using UnityEngine;
using UnityEngine.InputSystem;

public class ActionsEventsHandler : MonoBehaviour
{
    private PlayerInput _playerInput;
    private WeaponRange _weaponRange;
    private Animator _animator;
    private static readonly int DebugAttack = Animator.StringToHash("DebugAttack");
    private static readonly int DebugDash = Animator.StringToHash("DebugDash");
    private static readonly int DebugRun = Animator.StringToHash("DebugRun");

    private void Awake()
    {
        _playerInput = new PlayerInput();
        _playerInput.Enable();
        _weaponRange = GetComponentInChildren<WeaponRange>();
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
    }
    
    private void OnAttackStarted(InputAction.CallbackContext context)
    {
        if (_weaponRange.isEnemyInRange)
        {
            foreach (var enemy in _weaponRange.triggerList)
            {
                // enemy.gameObject.GetComponentInParent<AiController>().RemoveHealth(_weaponRange.weaponDamage); invoke from animation
            }
            _animator.SetTrigger(DebugAttack);
            VisualDebugger.PrintText("Player Attacks!");
            Debug.Log("Player Attacks!");
        }
        else
        {
            VisualDebugger.PrintText("Player misses target!");
            Debug.Log("Player misses target!");
        }
    }
    private void OnAttackCanceled(InputAction.CallbackContext context)
    {
        _animator.ResetTrigger(DebugAttack);
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
    }
    
}
