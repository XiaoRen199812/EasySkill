/*
日期：
功能：定时任务管理脚本，用于循环任务 
作者：小人
版本号：
*/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeMgr : MonoSingleton<TimeMgr>
{
    //默认使用Time.deltaTime 驱动
    public event Action tasks;
   
    public Timer StartTask(float waitTime, Action action, int repeatCount)
    {
        Timer timer=new Timer();
        timer.Start(waitTime, action, repeatCount);
        tasks += timer.Loop;
        return timer;
    }

   
    public void Update()
    {
        tasks?.Invoke();
    }
}
