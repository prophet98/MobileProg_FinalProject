using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiController : MonoBehaviour
{
    private NavMeshAgent _agent;
    // Animator anim;
    public Transform player;
    private AiState _currentAiState;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        // anim = this.GetComponent<Animator>();
        _currentAiState = new Chase(gameObject, _agent, player);
    }

    private void Update()
    {
        _currentAiState = _currentAiState.Process();
    }
}

