using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WalkStrategy : I_Strategy
{
    readonly Transform entity;
    readonly NavMeshAgent agent;
    readonly List<Transform> patrolPoints;
    readonly float patrolSpeed;
    int currentIndex;
    bool isPathCalculated;
    public Node.Status Process()
    {
        if (currentIndex == patrolPoints.Count) return Node.Status.Success;

        var target = patrolPoints[currentIndex];
        agent.SetDestination(target.position);
        entity.LookAt(target);

        Debug.Log("isPathCalculated:" + isPathCalculated);
        Debug.Log("agent.remainingDistance:" + agent.remainingDistance);
        Debug.Log("agent.pathPending:" + agent.pathPending);

        if (isPathCalculated && agent.remainingDistance < 0.1f)
        {
            currentIndex++;
            isPathCalculated = false;
        }
        if (agent.pathPending)
        {
            isPathCalculated = true;
        }
        return Node.Status.Running;
    }

    public void Reset() => currentIndex = 0;


    public WalkStrategy(Transform entity, NavMeshAgent agent, List<Transform> patrolPoints, float patrolSpeed = 2)
    {
        this.entity = entity;
        this.agent = agent;
        this.patrolPoints = patrolPoints;
        this.patrolSpeed = patrolSpeed;
    }
}

public class ActionStrategy : I_Strategy
{
    readonly Action doSomthing;
    public ActionStrategy(Action doSomthing)
    {
        this.doSomthing = doSomthing;
    }
    public Node.Status Process()
    {
        doSomthing();
        return Node.Status.Success;
    }
}