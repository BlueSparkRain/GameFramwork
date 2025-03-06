using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class EnemyIdleState : EnemyState
{
    public EnemyIdleState(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName, Animator anim) : base(enemy, stateMachine, animBoolName, anim)
    {
    }

    public override void OnEnter()
    {
        base.OnEnter();
        Debug.Log("EnemyΩ¯»Î¡ÀIdleState");
    }

    public override void OnExit()
    {
        base.OnExit();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        if (Input.GetKey(KeyCode.Alpha1)) 
        {
            stateMachine.ChangState(enemy.patrolState);
           
        }
    }
}
