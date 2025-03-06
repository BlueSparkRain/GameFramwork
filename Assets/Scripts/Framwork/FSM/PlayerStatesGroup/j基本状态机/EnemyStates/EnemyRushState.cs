using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRushState : EnemyState
{
    public EnemyRushState(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName, Animator anim) : base(enemy, stateMachine, animBoolName, anim)
    {
    }

    public override void OnEnter()
    {
        base.OnEnter();
        Debug.Log("EnemyΩ¯»Î¡ÀRushState");
    }

    public override void OnExit()
    {
        base.OnExit();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        if (Input.GetKey(KeyCode.Alpha4))
        {
            stateMachine.ChangState(enemy.idleState);
        }
    }
}
