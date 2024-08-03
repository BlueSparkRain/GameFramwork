using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ResourcesManager.Instance.LoadAsync<GameObject>("Cap", (a) => { Instantiate(a); });
        ResourcesManager.Instance.LoadAsync("Cap",typeof(GameObject), (a) => { Instantiate(a); });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
