public class PlayerStateMachine : FSM_StateMachine<Player>
{
    public override void ChangeState(FSM_States<Player> newState)
    {
        base.ChangeState(newState);
    }

    public override void InitializeState(FSM_States<Player> initState)
    {
        base.InitializeState(initState);
    }
}
