using UnityEngine;
using UnityEngine.AI;

namespace AiScripts
{
    public class ChaseState: AiState
    {
        public ChaseState(GameObject npc, NavMeshAgent agent, Transform player, float attackDistance): base(npc, agent, player, attackDistance)
        {
            base.agent.speed = 5;
            base.agent.isStopped = false;
        }

        protected override void Enter()
        {
            // anim.ResetTrigger("isChasing");
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
                    nextAiState = new AttackState(npc, agent, player, AttackDistance);
                    stage = Event.Exit;
                }
            }
        }
        protected override void Exit()
        {
            // anim.ResetTrigger("isChasing");
            base.Exit();
        }
    }

    public class AttackState : AiState
    {

        public AttackState(GameObject npc, NavMeshAgent agent, Transform player, float attackDistance): base(npc, agent, player, attackDistance)
        {
        }

        protected override void Enter()
        {
            // anim.SetTrigger("isAttacking");
            agent.isStopped = true;
            base.Enter();
        }

        protected override void Update()
        {
            // Debug.Log(npc.name + " " + "enemy attacking");
            if (AttackDistance>=10) //is a ranged player?
            {
                AlignActorRotation();
            }

            if (IsInRange())
            {
                //&& !anim.GetCurrentAnimatorStateInfo(0).IsName("YourAnimationName")  da aggiungere nell'if sopra
                AlignActorRotation();
            }
            
            
            if (!IsInRange())
            {
                //&& anim.GetCurrentAnimatorStateInfo(0).IsName("YourAnimationName")  da aggiungere nell'if sopra.
                nextAiState = new ChaseState(npc, agent, player, AttackDistance);
                stage = Event.Exit;
            }

        
        }
        
        private const float RotationSpeed = 10.0f;
        private void AlignActorRotation()
        {
            Vector3 direction = player.position - npc.transform.position;
            npc.transform.rotation = Quaternion.Slerp(npc.transform.rotation, Quaternion.LookRotation(direction),
                Time.deltaTime * RotationSpeed);
        }

        protected override void Exit()
        {
            // anim.ResetTrigger("isAttacking");
            base.Exit();
        }
    }
}