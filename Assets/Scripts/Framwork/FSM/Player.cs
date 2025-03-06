using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerStateMachine stateMachine { get; private set; }
    public PlayerIdleState idleState { get; private set; }
    public PlayerWalkState walkState { get; private set; }
    public PlayerRushState rushState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }

    private void Awake()
    {
        stateMachine = new PlayerStateMachine();
        idleState = new PlayerIdleState(this, stateMachine, "idle");
        walkState = new PlayerWalkState(this, stateMachine, "walk");
        rushState = new PlayerRushState(this, stateMachine, "rush");
        jumpState = new PlayerJumpState(this, stateMachine, "jump");
    }
    private void Update()
    {
        stateMachine.currentState.OnUpdate();
    }
}
