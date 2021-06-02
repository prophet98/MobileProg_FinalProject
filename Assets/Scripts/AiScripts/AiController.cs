using System;
using System.Linq;
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

        private void OnDestroy()
        {
            if (player == null) return;
            player.GetComponentInChildren<PlayerWeaponComponent>().killCounter++;
            player.GetComponentInChildren<PlayerWeaponComponent>().triggerList.Remove(this.GetComponentInChildren<Collider>());
            if (player.GetComponentInChildren<PlayerWeaponComponent>().killCounter == GameObject.FindGameObjectsWithTag("Spawner").Length)
            {
                // Debug.Log("combattimento finito");
                var battleMoney = GameObject.FindGameObjectsWithTag("Spawner").Sum(spawner => spawner.GetComponent<EnemySpawner>().enemyCoinValue);
                player.GetComponent<BattleRewardSystem>().RewardPlayer(battleMoney);
                player.GetComponent<BattleRewardSystem>().canPassGate = true;
            }

        }
    }

}

