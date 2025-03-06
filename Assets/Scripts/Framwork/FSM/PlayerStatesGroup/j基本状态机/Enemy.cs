using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyStateMachine stateMachine = new EnemyStateMachine();
    public EnemyIdleState idleState;
    public EnemyRushState rushState;
    public EnemyAttackState attackState;
    public EnemyPatrolState patrolState;
    Animator anim;
    private void Awake()
    {
        anim=GetComponent<Animator>();
        idleState = new EnemyIdleState(this, stateMachine, "idle", anim);
        rushState = new EnemyRushState(this, stateMachine, "rush", anim);
        attackState = new EnemyAttackState(this, stateMachine, "attack", anim);
        patrolState = new EnemyPatrolState(this, stateMachine, "patrol", anim);
        stateMachine.InitializeState(idleState);
    }
    void Update()
    {
        stateMachine.currentState.OnUpdate();
    }
}
