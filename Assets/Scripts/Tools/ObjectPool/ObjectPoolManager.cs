using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//解决问题：
//1.频繁的实例化对象会带来一定的性能开销
//2.对象的销毁会造成大量的内存垃圾，会造成GC的频繁触发，导致有内存释放带来的卡顿感
//思想：
//通过重复利用已经创建的对象，避免频繁的创建和销毁过程，减少系统的内存分配和垃圾回收带来的开销
public class ObjectPoolManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
