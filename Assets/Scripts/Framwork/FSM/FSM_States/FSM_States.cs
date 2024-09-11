using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM_States<T>
{
   protected FSM_StateMachine<T> stateMachine;
    
   public virtual void OnEnter() { }
   public virtual void OnUpdate() { }
   public virtual void OnExit() { }
}
