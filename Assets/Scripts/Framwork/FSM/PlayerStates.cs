using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStates : FSM_States<Player>
{
    protected Player player;
    protected string animBoolName;
    public PlayerStates(Player _player,PlayerStateMachine _stateMachine,string _animBoolName) 
    {
        player = _player;
        stateMachine = _stateMachine;
        animBoolName = _animBoolName;
    
    }

    public override void OnEnter()
    {
        base.OnEnter();
    }

    public override void OnExit()
    {
        base.OnExit();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
    }
}
