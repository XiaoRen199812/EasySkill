/*
日期：
功能：继承Mono的泛型单例
作者：小人
版本号：
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T: MonoBehaviour
{
    
    private static T _instance;

    public static T Instance {  
        get { 
            if(_instance==null)
            {
                _instance = FindObjectOfType<T>();
                if(_instance == null)
                {
                    
                    GameObject go = new GameObject(typeof(T).Name);
                    _instance = go.AddComponent<T>();
                }
            }
            return _instance; 
        }
    
    }

    protected virtual void Awake()
    {
        GameObject.DontDestroyOnLoad(this);
    }
    
}
