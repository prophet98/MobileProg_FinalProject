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
    // protected Animator anim;
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
        this.AttackDistance = attackDistance;
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
    

    protected bool CanAttackPlayer()
    {
        var direction = player.position - npc.transform.position;
        if (direction.magnitude < AttackDistance)
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
        agent.SetDestination(player.position);
        if (agent.hasPath)
        {
            if (CanAttackPlayer())
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
        npc.transform.rotation = Quaternion.Slerp(npc.transform.rotation, Quaternion.LookRotation(direction),
                                            Time.deltaTime * rotationSpeed);
        if (!CanAttackPlayer())
        {
            nextAiState = new Chase(npc, agent, player, AttackDistance);
            stage = Event.Exit;
        }
    }

    protected override void Exit()
    {
        // anim.ResetTrigger("isAttacking");
        base.Exit();
    }
}
