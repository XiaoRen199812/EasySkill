/*
日期：
功能：
作者：
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

    private void Awake()
    {
        GameObject.DontDestroyOnLoad(this);
    }

}
