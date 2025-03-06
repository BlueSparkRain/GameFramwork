using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public interface I_Strategy
{
    Node.Status Process();
    void Reset() { }
}

public class Condition : I_Strategy
{
    //触发条件
    readonly Func<bool> predicate;
    public Condition(Func<bool> predicate)
    {
        this.predicate = predicate;
    }
    public Node.Status Process() => predicate() ? Node.Status.Success : Node.Status.Failure;
}

public class PrioritySelector : Node
{
    List<Node> sortedChildren;
    List<Node> SortedChildren => sortedChildren ??= SortChildren();
    protected virtual List<Node> SortChildren() => children.OrderByDescending(child => child.priority).ToList();
    public PrioritySelector(string name) : base(name) { }
    public override void Reset()
    {
        base.Reset();
        sortedChildren = null;
    }

    public override Status Process()
    {
        foreach (var child in SortedChildren)
        {
            switch (child.Process())
            {
                case Status.Running:
                    return Status.Running;
                case Status.Success:
                    return Status.Success;
                default:
                    continue;
            }
        }
        return Status.Failure;
    }
}

public class Selector : Node
{
    public Selector(string name) : base(name) { }
    public override Status Process()
    {
        if (currentChild < children.Count)
        {
            switch (children[currentChild].Process())
            {
                case Status.Running:
                    return Status.Running;
                case Status.Success:
                    Reset();
                    return Status.Success;
                default:
                    currentChild++;
                    return Status.Running;
            }
        }
        Reset();
        return Status.Failure;
    }
}

public class Sequence : Node
{
    public Sequence(string name, int priority = 0) : base(name, priority) { }
    public override Status Process()
    {
        if (currentChild < children.Count)
        {
            switch (children[currentChild].Process())
            {
                case Status.Running:
                    return Status.Running;
                case Status.Failure:
                    Reset();
                    return Status.Failure;
                default:
                    currentChild++;
                    return currentChild == children.Count ? Status.Success : Status.Running;//这里打错了，注意注意！
            }
        }
        Reset();
        return Status.Success;
    }
}

public class BehaviourTree : Node
{
    public BehaviourTree(string name) : base(name) { }

    public override Status Process()
    {
        while (currentChild < children.Count)
        {
            var status = children[currentChild].Process();
            if (status != Status.Success)
            {
                Debug.Log("TreeTree的:" + status);
                return status;
            }
            currentChild++;
        }
        Debug.Log("TreeTree的:Success");
        return Status.Success;//已经处理的本节点下所有的子节点
    }
}

/// <summary>
/// 叶子只负责执行策略
/// </summary>
public class Leaf : Node
{
    readonly I_Strategy strategy;
    public Leaf(string name, I_Strategy strategy, int priority = 0) : base(name, priority)
    {
        this.strategy = strategy;
    }
    public override Status Process() => strategy.Process();

    public override void Reset() => strategy.Reset();

}

public class Node
{
    public enum Status { Success, Failure, Running }
    public string name;
    public readonly int priority;
    public readonly List<Node> children = new();
    protected int currentChild;

    public Node(string name = "Node", int priority = 0)
    {
        this.name = name;
        this.priority = priority;
    }
    public void AddChild(Node child) => children.Add(child);
    public virtual Status Process() => children[currentChild].Process();

    public virtual void Reset()
    {
        currentChild = 0;
        foreach (Node child in children)
        {
            child.Reset();
        }
    }
}

