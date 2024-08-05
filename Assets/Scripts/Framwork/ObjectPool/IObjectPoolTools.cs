using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 每个ObjectPool在ResetInstance时有不同的的逻辑
/// </summary>
public interface IObjectPoolTools 
{
    public void ResetInstance();

}
