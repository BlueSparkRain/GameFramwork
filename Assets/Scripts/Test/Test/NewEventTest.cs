using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewEventTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        EventCenter.Instance.EventTrigger_Priority(E_EventType.E_Test, 1);  
    }

    private void OnEnable()
    {
        EventCenter.Instance.AddEventListener(E_EventType.E_Test, new PriorityAction<int>(1, Fun1));
        EventCenter.Instance.AddEventListener(E_EventType.E_Test, new PriorityAction<int>(3, Fun2));
        EventCenter.Instance.AddEventListener(E_EventType.E_Test, new PriorityAction<int>(2, Fun3));
    }

    void Fun1(int i) 
    {
        Debug.Log(i+1);
    } 
    void Fun2(int i) 
    {
        Debug.Log(i+2);
    }
    
    void Fun3(int i) 
    {
        Debug.Log(i+3);
    }

    private void OnDisable()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
