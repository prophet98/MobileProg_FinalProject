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
        private const string PlayerTag= "Player";
        public AiAgentStats agentStats;
        private void Start()
        {
            _agent = GetComponent<NavMeshAgent>();
            anim = GetComponentInChildren<Animator>();
            player = GameObject.FindWithTag(PlayerTag).transform;
            _currentAiState = new ChaseState(gameObject, _agent, player, anim, agentStats);
        }
        private void Update()
        {
            _currentAiState = _currentAiState.Process();
        }
    }

}

