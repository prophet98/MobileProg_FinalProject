using UnityEngine;
using UnityEngine.AI;

namespace AiScripts
{
    public class ChaseState: AiState
    {
        private static readonly int DebugRun = Animator.StringToHash("DebugRun");

        public ChaseState(GameObject npc, NavMeshAgent agent, Transform player, Animator anim,float attackDistance): base(npc, agent, player, anim, attackDistance)
        {
            base.agent.speed = 5;
            base.agent.isStopped = false;
        }

        protected override void Enter()
        {
            anim.SetTrigger(DebugRun);
            base.Enter();
        }

        protected override void Update()
        {
            // Debug.Log(npc.name + " " + "enemy chasing");
        
            agent.SetDestination(player.position - Vector3.right);
            if (agent.hasPath)
            {
                if (IsInRange())
                {
                    nextAiState = new AttackState(npc, agent, player, anim, AttackDistance);
                    stage = Event.Exit;
                }
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
        private const string AttackAnimName = "ATTACK01";
        private static readonly int DebugAttack = Animator.StringToHash("DebugAttack");

        public AttackState(GameObject npc, NavMeshAgent agent, Transform player, Animator anim, float attackDistance): base(npc, agent, player, anim, attackDistance)
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
            // Debug.Log(npc.name + " " + "enemy attacking");
            if (AttackDistance>=10) //is a ranged enemy?
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
                nextAiState = new ChaseState(npc, agent, player, anim, AttackDistance);
                stage = Event.Exit;
            }
        }
        
        private const float RotationSpeed = 5.0f;
        private void AlignActorRotation()
        {
            Vector3 direction = player.position - npc.transform.position;
            npc.transform.rotation = Quaternion.Slerp(npc.transform.rotation, Quaternion.LookRotation(direction),
                Time.deltaTime * RotationSpeed);
        }

        protected override void Exit()
        {
            anim.ResetTrigger(DebugAttack);
            base.Exit();
        }
    }
}