using UnityEngine;
using UnityEngine.AI;

namespace AiScripts
{
    public class AiController : MonoBehaviour
    {
        private NavMeshAgent _agent;
        public Animator anim;
        public Transform player;
        private AiState _currentAiState;

        public float attackRange;
        private void Start()
        {
            _agent = GetComponent<NavMeshAgent>();
            anim = this.GetComponentInChildren<Animator>();
            _currentAiState = new ChaseState(gameObject, _agent, player, anim, attackRange);
        }
        private void Update()
        {
            _currentAiState = _currentAiState.Process();
        }

    }
    
    
}

