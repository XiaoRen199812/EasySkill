/*
日期：
功能：网上看到的其他人写的，直接Copy来用了
作者：小人
版本号：
*/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class LineEvent
{

    public float Delay { get; protected set; }//延迟时间
    public int Id { get; protected set; }//操作的ID，本质上存的是Method中的参数
    public Action<int> Method { get; protected set; }//回调函数
    public bool isInvoke = false;

    public LineEvent(float delay, int id, Action<int> method)
    {
        Delay = delay;
        Id = id;
        Method = method;
        Reset();//重置各种状态
    }

    public void Reset()
    {
        isInvoke = false;
    }

    //每帧执行（自己驱动的帧）,time是从时间线开始，到目前为止经过的时间
    public void Invoke(float time)
    {
        //当前事件还没到延迟时间，直接返回
        if (time < Delay)
        {
            return;
        }
        if (!isInvoke && null != Method)
        {
            isInvoke = true;
            Method(Id);//保证Method在时间线的整个生存周期内只会执行一次
        }
    }
}
