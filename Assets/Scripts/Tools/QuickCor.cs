/*
日期：
功能：普通C#类 能够快速使用协程 停止协程
作者：
版本号：
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickCor 
{
    private static QuickCor _instance=null;

    public static QuickCor Instance
    {
        get {
            if (_instance==null)
            {
                _instance = new QuickCor();
               // _instance.Init();
            }
            return _instance;
        }
    }

    public GameObject go;
    public MonoEx monoEx;

    public  void Init()
    {
        go = new GameObject("QuickCor");
        monoEx= go.AddComponent<MonoEx>();
    }

    // 开始协程
    public void StartCor(IEnumerator ie)
    {
        monoEx.StartCoroutine(ie);
    }

    //停止协程
    public void StopCor(IEnumerator ie)
    {
        monoEx.StopCoroutine(ie);
    }
}

//该脚本主要是避免直接添加MonoBehaviour报错
public class MonoEx:MonoBehaviour
{ 

}
