public class FSM_StateMachine<T>
{
    public FSM_States<T> currentState;
    public virtual void InitializeState(FSM_States<T> initState)
    {
        currentState = initState;
    }

    public virtual void ChangeState(FSM_States<T> newState)
    {
        currentState.OnExit();
        currentState = newState;
        currentState.OnExit();
    }
}
