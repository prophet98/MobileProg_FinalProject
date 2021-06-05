using UnityEngine;
using UnityEngine.AI;

namespace AiScripts
{
    public class ChaseState: AiState
    {
        private static readonly int DebugRun = Animator.StringToHash("DebugRun");

        public ChaseState(GameObject npc, NavMeshAgent agent, Transform player, Animator anim, AiAgentStats stats): base(npc, agent, player, anim, stats)
        {
            agent.speed = stats.agentMovementSpeed;
            agent.isStopped = false;
        }

        protected override void Enter()
        {
            anim.SetTrigger(DebugRun);
            base.Enter();
        }

        protected override void Update()
        {
            agent.SetDestination(player.position - Vector3.right);
            if (agent.hasPath)
            {
                if (!IsInRange()) return;
                nextAiState = new AttackState(npc, agent, player, anim, agentStats );
                stage = Event.Exit;
            }
        }
        protected override void Exit()
        {
            anim.ResetTrigger(DebugRun);
            base.Exit();
        }
    }
    
    public class AttackState : AiState
    { 
        private static readonly int DebugAttack = Animator.StringToHash("DebugAttack");
        public AttackState(GameObject npc, NavMeshAgent agent, Transform player, Animator anim, AiAgentStats stats): base(npc, agent, player, anim, stats)
        {
        }

        protected override void Enter()
        {
            anim.SetTrigger(DebugAttack);
            agent.isStopped = true;
            base.Enter();
        }

        protected override void Update()
        {
            if (agentStats.attackDistance>=10) //is a ranged enemy?
            {
                AlignActorRotation();
            }

            if (IsInSight() && IsInRange())
            {
                anim.SetTrigger(DebugAttack);
            }
            if (IsInRange() && !IsInSight())
            {
                anim.ResetTrigger(DebugAttack);
                AlignActorRotation();
            } 
            if (!IsInRange() && !IsInSight())
            {
                nextAiState = new ChaseState(npc, agent, player, anim, agentStats);
                stage = Event.Exit;
            }
        }
        
        private void AlignActorRotation()
        {
            var direction = player.position - npc.transform.position;
            npc.transform.rotation = Quaternion.Slerp(npc.transform.rotation, Quaternion.LookRotation(direction),
                Time.deltaTime * agentStats.agentRotationSpeed);
        }

        protected override void Exit()
        {
            anim.ResetTrigger(DebugAttack);
            base.Exit();
        }
    }
}