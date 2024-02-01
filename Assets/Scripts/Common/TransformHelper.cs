/*
日期：
功能：Transform助手类
作者：小人
版本号：
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TransformHelper
{ 

    //删除某节点下所有子物体
  public static void  DestoryAllChild(Transform transform)
    {
        for(int i=0;i<transform.childCount;i++)
        {
            GameObject.Destroy(transform.GetChild(i).gameObject);
        }
    }
    
    //通过名字 从父节点查找是否有一个名为xx的物体
    // 是否包括隐藏的物体 默认不包含
    //节点结构不复杂 层级不太深
    public static Transform FindChild(Transform parent,string name,bool includeActive=false)
    {

     Transform[] arr=   parent.GetComponentsInChildren<Transform>(includeActive);
        for(int i=0;i<arr.Length;i++)
        {
            if (arr[i].name == name)
            {
                return arr[i];
            }
        }
        return null;
        
    }

    //递归查找
    //节点结构较复杂 层级较深
    public static Transform FindChild(Transform parent, string name)
    {
        Transform child = parent.Find(name);
        if(child!=null)
        {
            return child;
        }

        for(int i=0; i<parent.childCount;i++)
        {
            FindChild(parent.GetChild(i), name);
        }

        return null;
    }
}
