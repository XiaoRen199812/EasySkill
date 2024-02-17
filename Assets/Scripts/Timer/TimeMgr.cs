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
   
    //开始任务 参数1 等待时间  参数2 具体做啥 参数3重复几次 -1表示无数次
    public Timer StartTask(float waitTime, Action action, int repeatCount)
    {
        Timer timer=new Timer();
        timer.Start(waitTime, action, repeatCount);
        tasks += timer.Loop;
        return timer;
    }

   //驱动任务执行
    public void Update()
    {
        tasks?.Invoke();
        
    }

   
  
}
