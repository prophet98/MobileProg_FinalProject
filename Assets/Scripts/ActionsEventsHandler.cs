
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class ActionsEventsHandler : MonoBehaviour
{
    private PlayerInput _playerInput;

    private void Awake()
    {
        _playerInput = new PlayerInput();
        _playerInput.Enable();
    }

    private void OnEnable()
    {
        _playerInput.PlayerActions.Attack.performed += OnAttackPerformed;
        _playerInput.PlayerActions.Dash.performed += OnDashPerformed;
        _playerInput.PlayerActions.Skill.performed += OnSkillPerformed;
    }

    private void OnAttackPerformed(InputAction.CallbackContext context)
    {
        Debug.Log("Player Attacks!");
    }
    private void OnDashPerformed(InputAction.CallbackContext obj)
    {
        Debug.Log("Player Dashes!");
    }
    private void OnSkillPerformed(InputAction.CallbackContext obj)
    {
        Debug.Log("Player used an active skill!");
    }
    
    private void OnDisable()
    {
        _playerInput.PlayerActions.Attack.performed -= OnAttackPerformed;
        _playerInput.PlayerActions.Dash.performed -= OnDashPerformed;
        _playerInput.PlayerActions.Skill.performed -= OnSkillPerformed;
    }
}
