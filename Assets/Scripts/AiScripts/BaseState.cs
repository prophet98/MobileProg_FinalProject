using UnityEngine;
using UnityEngine.AI;

namespace AiScripts
{
    public abstract class AiState
    {
        protected enum Event
        {
            Enter,
            Update,
            Exit
        };

        protected Event stage;
        protected readonly GameObject npc;
        protected readonly Animator anim;
        protected readonly Transform player;
        protected AiState nextAiState;
        protected readonly NavMeshAgent agent;
        protected readonly AiAgentStats agentStats;

        protected AiState(GameObject npc, NavMeshAgent agent, Transform player, Animator anim, AiAgentStats stats)
        {
            this.npc = npc;
            this.agent = agent;
            this.anim = anim;
            stage = Event.Enter;
            this.player = player;
            agentStats = stats;
        }

        protected virtual void Enter()
        {
            stage = Event.Update;
        }

        protected virtual void Update()
        {
            stage = Event.Update;
        }

        protected virtual void Exit()
        {
            stage = Event.Exit;
        }

        public AiState Process()
        {
            if (stage == Event.Enter) Enter();
            if (stage == Event.Update) Update();
            if (stage == Event.Exit)
            {
                Exit();
                return nextAiState;
            }

            return this;
        }

        protected bool IsInRange()
        {
            var direction = player.position - npc.transform.position;
            return direction.magnitude < agentStats.attackDistance;
        }

        protected bool IsInSight()
        {
            var direction = player.position - npc.transform.position;
            var angle = Vector3.Angle(direction.normalized, npc.transform.forward);
            return (direction.magnitude < agentStats.attackDistance) && angle < agentStats.sightAngle;
        }
    }
}