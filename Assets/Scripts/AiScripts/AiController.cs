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
        private const string PlayerTag = "Player";
        private const string SpawnerTag = "Spawner";
        public AiAgentStats agentStats;

        private PlayerWeaponComponent _playerWeaponComponent;
        private BattleRewardSystem _battleRewardSystem;
        private void Awake() //set up components
        {
            _agent = GetComponent<NavMeshAgent>();
            anim = GetComponentInChildren<Animator>();
            player = GameObject.FindWithTag(PlayerTag).transform;
            if (!player) return;
            _playerWeaponComponent = player.GetComponentInChildren<PlayerWeaponComponent>();
            _battleRewardSystem = player.GetComponent<BattleRewardSystem>();
        }

        private void Start() //find player target and set the current state to default chasing.
        {
            player = GameObject.FindWithTag(PlayerTag).transform;
            _currentAiState = new ChaseState(gameObject, _agent, player, anim, agentStats);
        }

        private void Update() //process whatever state is active at the moment.
        {
            _currentAiState = _currentAiState.Process();
        }

        private void OnDestroy() //when the ai gets killed, update player components accordingly and 
        {
            if (player == null) return;
            _playerWeaponComponent.killCounter++;
            _playerWeaponComponent.triggerList.Remove(this.GetComponentInChildren<Collider>());
            if (_playerWeaponComponent.killCounter == GameObject.FindGameObjectsWithTag(SpawnerTag).Length)
            {
                var battleMoney = GameObject.FindGameObjectsWithTag(SpawnerTag)
                    .Sum(spawner => spawner.GetComponent<EnemySpawner>().enemyCoinValue);
                _battleRewardSystem.RewardPlayer(battleMoney);
                _battleRewardSystem.canPassGate = true;
            }
        }
    }
}