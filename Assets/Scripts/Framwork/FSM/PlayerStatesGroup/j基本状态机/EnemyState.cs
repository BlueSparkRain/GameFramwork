using UnityEngine;

public class EnemyState
{
    protected Enemy enemy;
    //����������Ķ������ƣ��ȼ���Enemy�ж���
    protected string animBoolName;
    //�����������Animator���ȼ���Enemy�ж���
    protected Animator anim;

    protected EnemyStateMachine stateMachine;

    /// <summary>
    /// �˹��캯������״̬����ĳ�ʼ��
    /// </summary>
    /// <param name="enemy">����������</param>
    /// <param name="stateMachine">�����������״̬��</param>
    /// <param name="animBoolName">����������Ķ�������</param>
    public EnemyState(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName, Animator anim)
    {
        this.enemy = enemy;
        this.animBoolName = animBoolName;
        this.stateMachine = stateMachine;
        this.anim = anim;
    }

    /// <summary>
    /// ���뱾״̬ʱ����
    /// </summary>
    public virtual void OnEnter()
    {
        //�����Ҫ�ٽ���״̬ʱ�Ͳ��Ŷ��������ڴ˴�����
        SetBoolAnimPlay(animBoolName);
    }

    /// <summary>
    /// ��״̬ʱ�̸���ʱ����
    /// </summary>
    public virtual void OnUpdate() { }

    /// <summary>
    /// �˳���״̬ʱ����
    /// </summary>
    public virtual void OnExit() { }

    //���Ի�������������
    public virtual void SetBoolAnimPlay(string animName)
    {
        anim.SetBool(animName, true);
    }
}
