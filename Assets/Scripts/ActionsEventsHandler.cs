
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class ActionsEventsHandler : MonoBehaviour
{
    private PlayerInput _playerInput;
    private WeaponRange _weaponRange;
    private void Awake()
    {
        _playerInput = new PlayerInput();
        _playerInput.Enable();
        _weaponRange = GetComponentInChildren<WeaponRange>();
    }

    private void OnEnable()
    {
        _playerInput.PlayerActions.Attack.performed += OnAttackPerformed;
        _playerInput.PlayerActions.Dash.performed += OnDashPerformed;
        _playerInput.PlayerActions.Skill.performed += OnSkillPerformed;
    }
    
    private void OnAttackPerformed(InputAction.CallbackContext context)
    {
        if (_weaponRange.isEnemyInRange)
        {
            VisualDebugger.PrintText("Player Attacks!");
            Debug.Log("Player Attacks!");
        }
        else
        {
            VisualDebugger.PrintText("Player misses target!");
            Debug.Log("Player misses target!");
        }
        
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

    private void OnDisable()
    {
        _playerInput.PlayerActions.Attack.performed -= OnAttackPerformed;
        _playerInput.PlayerActions.Dash.performed -= OnDashPerformed;
        _playerInput.PlayerActions.Skill.performed -= OnSkillPerformed;
    }
    
}
