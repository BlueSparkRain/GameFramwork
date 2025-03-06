using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolState : EnemyState
{
    public EnemyPatrolState(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName, Animator anim) : base(enemy, stateMachine, animBoolName, anim)
    {
    }

    public override void OnEnter()
    {
        base.OnEnter();
        Debug.Log("EnemyΩ¯»Î¡ÀPatrolState");
    }

    public override void OnExit()
    {
        base.OnExit();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        if (Input.GetKey(KeyCode.Alpha2))
        {
            stateMachine.ChangState(enemy.rushState);
           
        }
    }
}
