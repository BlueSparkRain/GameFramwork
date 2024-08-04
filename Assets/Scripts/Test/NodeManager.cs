using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class NodeManager : MonoBehaviour
{
    //存储了路径的所有节点
   public List<GameObject> nodes = new List<GameObject>();
  

   
    void Update()
    {
        for (int i = 0; i < nodes.Count-1; i++) 
        {
            Debug.DrawLine(nodes[i].transform.position, nodes[i + 1].transform.position, Color.blue, Time.deltaTime);
        }
    }
}
