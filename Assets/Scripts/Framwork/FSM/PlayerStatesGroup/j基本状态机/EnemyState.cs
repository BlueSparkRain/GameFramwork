using UnityEngine;

public class EnemyState
{
    protected Enemy enemy;
    //控制器对象的动画名称，先假设Enemy有动画
    protected string animBoolName;
    //控制器对象的Animator，先假设Enemy有动画
    protected Animator anim;

    protected EnemyStateMachine stateMachine;

    /// <summary>
    /// 此构造函数用于状态对象的初始化
    /// </summary>
    /// <param name="enemy">控制器对象</param>
    /// <param name="stateMachine">控制器对象的状态机</param>
    /// <param name="animBoolName">控制器对象的动画名称</param>
    public EnemyState(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName, Animator anim)
    {
        this.enemy = enemy;
        this.animBoolName = animBoolName;
        this.stateMachine = stateMachine;
        this.anim = anim;
    }

    /// <summary>
    /// 进入本状态时调用
    /// </summary>
    public virtual void OnEnter()
    {
        //如果需要再进入状态时就播放动画，就在此处调用
        SetBoolAnimPlay(animBoolName);
    }

    /// <summary>
    /// 本状态时刻更新时调用
    /// </summary>
    public virtual void OnUpdate() { }

    /// <summary>
    /// 退出本状态时调用
    /// </summary>
    public virtual void OnExit() { }

    //个性化函数，不必须
    public virtual void SetBoolAnimPlay(string animName)
    {
        anim.SetBool(animName, true);
    }
}
