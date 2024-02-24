/*
日期：
功能:事件中心 管理事件 提供事件的注册及注销 广播等功能
作者：小人
版本号：
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class EventCenter : MonoSingleton<EventCenter>
{
   //存储事件    事件名   委托/事件
   //一般使用无返回值的委托
    private Dictionary<string,EventHandler> eventDic= new Dictionary<string,EventHandler>();


    //注册事件  事件名  相关函数
    public void RegesitEvent(string eventName,EventHandler e)
    {
        if(eventDic.ContainsKey(eventName))
        {
            eventDic[eventName] += e;
        }
        else
        {
            eventDic.Add(eventName, e);
        }
    }

    //移除事件  事件名 相关函数
    public void RemoveEvent(string eventName, EventHandler e)
    {
       if(eventDic.ContainsKey(eventName))
       {
            eventDic[eventName] -= e;
       }
    }

    // 触发事件  事件名 事件源 
    public void TriggerEventAndBroadcastAll(string eventName,object sender)
    {
        if(eventDic.ContainsKey(eventName))
        {
            eventDic[eventName]?.Invoke(sender, EventArgs.Empty);
        }
    }

    //触发事件  事件名 事件源 事件参数
    public void TriggerEventAndBroadcastAll(string eventName,object sender,EventArgs e)
    {
        if(eventDic.ContainsKey(eventName))
        {
            eventDic[eventName]?.Invoke(sender, e);
        }
    }

    
    public void ClearDic()
    {
        eventDic.Clear();
    }
}
