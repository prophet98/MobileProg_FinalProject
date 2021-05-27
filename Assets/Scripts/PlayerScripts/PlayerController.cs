
using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float playerSpeed = 2.0f;
    #region InputVariables
    private PlayerInput _playerInput;
    private CharacterController _controller;
    private Vector3 _playerVelocity;
    private Vector2 _movementInput;
    private Vector3 _moveVector;
    private bool _groundedPlayer;
    private static readonly int DebugRun = Animator.StringToHash("DebugRun");
    private const float GravityValue = -9.81f;
    #endregion

    private void Awake()
    {
        _playerInput = new PlayerInput();
        _controller = GetComponent<CharacterController>();
    }

    private void OnEnable()
    {
        _playerInput.Enable();
    }

    private void OnDisable()
    {
        _playerInput.Disable();
    }

    private void LateUpdate()
    {
        //TODO: Refactor this is just for testing.
         HandleMovement();  
        
    }

    private void HandleMovement()
    {
        _groundedPlayer = _controller.isGrounded;
        if (_groundedPlayer && _playerVelocity.y < 0)
        {
            _playerVelocity.y = 0f;
        }

        _movementInput= _playerInput.PlayerActions.Move.ReadValue<Vector2>();
        if (Math.Abs(_movementInput.x) > 0.2f || Math.Abs(_movementInput.y) > 0.2f)
        {
            _moveVector = new Vector3(_movementInput.x, 0, _movementInput.y);
        }
        else
        {
            return;
        }
        
        
        if (_moveVector != Vector3.zero)
        {
            gameObject.transform.forward = _moveVector;
        }
        _controller.Move(_moveVector * (Time.deltaTime * playerSpeed));
        _playerVelocity.y += GravityValue * Time.deltaTime;
    }
    
}

    
