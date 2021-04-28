using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiState
{
    protected enum State
    {
        Chasing, Attacking
    };

    protected enum Event
    {
        Enter, Update, Exit
    };

    protected State name;
    protected Event stage;
    protected readonly GameObject npc;
    protected Animator anim;
    protected readonly Transform player;
    protected AiState nextAiState;
    protected readonly NavMeshAgent agent;

    protected float AttackDistance { get; }

    protected AiState(GameObject npc, NavMeshAgent agent, Transform player, float attackDistance)
    {
        this.npc = npc;
        this.agent = agent;
        // anim = _anim;
        stage = Event.Enter;
        this.player = player;
        AttackDistance = attackDistance;
    }

    protected virtual void Enter() { stage = Event.Update; }
    protected virtual void Update() { stage = Event.Update; }
    protected virtual void Exit() { stage = Event.Exit; }

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
        var angle = Vector3.Angle(direction.normalized, npc.transform.forward);
        if ((direction.magnitude < AttackDistance) && angle < 45f)
        {
            return true;
        }
        return false;
    }
}

public class Chase: AiState
{
    public Chase(GameObject npc, NavMeshAgent agent, Transform player, float attackDistance): base(npc, agent, player, attackDistance)
    {
        name = State.Chasing;
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
        Debug.Log(npc.name + " " + "enemy chasing");
        
        agent.SetDestination(player.position - new Vector3(-1,0,0));
        if (agent.hasPath)
        {
            if (IsInRange())
            {
                nextAiState = new Attack(npc, agent, player, AttackDistance);
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

public class Attack : AiState
{
    float rotationSpeed = 10.0f;
    public Attack(GameObject npc, NavMeshAgent agent, Transform player, float attackDistance): base(npc, agent, player, attackDistance)
    {
        name = State.Attacking;
    }

    protected override void Enter()
    {
        // anim.SetTrigger("isAttacking");
        agent.isStopped = true;
        base.Enter();
    }

    protected override void Update()
    {
        Debug.Log(npc.name + " " + "enemy attacking");
        Vector3 direction = player.position - npc.transform.position;
        direction.y = 0;
        if (AttackDistance>=10)
        {
            AlignActorRotation();
        }

        if (IsInRange())
        {
            //&& !anim.GetCurrentAnimatorStateInfo(0).IsName("YourAnimationName")  da aggiungere nell'if sopra
            npc.transform.LookAt(player.transform);
        }
        
        if (!IsInRange())
        {
            //&& anim.GetCurrentAnimatorStateInfo(0).IsName("YourAnimationName")  da aggiungere nell'if sopra.
            nextAiState = new Chase(npc, agent, player, AttackDistance);
            stage = Event.Exit;
        }

        
    }

    private void AlignActorRotation()
    {
        Vector3 direction = player.position - npc.transform.position;
        npc.transform.rotation = Quaternion.Slerp(npc.transform.rotation, Quaternion.LookRotation(direction),
            Time.deltaTime * rotationSpeed);
    }

    protected override void Exit()
    {
        // anim.ResetTrigger("isAttacking");
        base.Exit();
    }
}
