public class EnemyStateMachine
{
    public EnemyState currentState;
    /// <summary>
    /// ״̬��ʼ��
    /// </summary>
    /// <param name="initState">��ʼ��״̬</param>
    public void InitializeState(EnemyState initState)
    {
        currentState = initState;
        currentState.OnEnter();
    }

    /// <summary>
    /// ״̬�л�
    /// </summary>
    /// <param name="newState">��״̬</param>
    public void ChangState(EnemyState newState)
    {
        currentState.OnExit();
        currentState = newState;
        currentState.OnEnter();
    }
}
