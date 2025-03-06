public class EnemyStateMachine
{
    public EnemyState currentState;
    /// <summary>
    /// ×´Ì¬³õÊ¼»¯
    /// </summary>
    /// <param name="initState">³õÊ¼»¯×´Ì¬</param>
    public void InitializeState(EnemyState initState)
    {
        currentState = initState;
        currentState.OnEnter();
    }

    /// <summary>
    /// ×´Ì¬ÇÐ»»
    /// </summary>
    /// <param name="newState">ÐÂ×´Ì¬</param>
    public void ChangState(EnemyState newState)
    {
        currentState.OnExit();
        currentState = newState;
        currentState.OnEnter();
    }
}
