/*
日期：
功能：定时的任务  用于处理常见的定时回调操作
作者：小人
版本号：
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Timer 
{
#region  相关属性参数
    //等待时间
    public float _waitTime;
    //需执行操作
    public Action _opeartion;
    //重复次数
    public int _repeatCount;

    //任务已经经过了多久
    private float _passedTime;
    // 任务已经执行了几次
    private float _passedCount;
    //任务是否处于暂停状态
    private bool _pause;
    //任务停止
    private bool _stop;

    #endregion
    //开启任务
    public void Start(float waitTime, Action action, int repeatCount)
    {
        _waitTime = waitTime;
        _opeartion = action;
        _repeatCount = repeatCount;

        _passedTime = 0;
        _passedCount = 0;
        _pause = false;
        _stop = false;
    }


    //当前任务是使用Time.deltaTime进行驱动
    public void Loop()
    {
        if (_stop == false)
        {
            if (_pause == false)
            {
                //非停止和暂停状态
                //任务重复有限次
                if (_repeatCount > 0)
                {
                    _passedTime += Time.deltaTime;
                    if (_passedTime > _waitTime)
                    {
                        if (_passedCount < _repeatCount)
                        {
                            _opeartion?.Invoke();
                            _passedTime -= _waitTime;
                            _passedCount += 1;
                        }
                        else
                        {
                            //停止
                            Stop();
                            return;
                        }


                    }
                }
                //任务执行无限次
                if (_repeatCount == -1)
                {
                    _passedTime += Time.deltaTime;
                    if (_passedTime > _waitTime)
                    {
                        _opeartion?.Invoke();
                        _passedTime -= _waitTime;
                    }
                }
            }
        }
    }

    //---------------------------------------------------------------
    //暂停恢复功能后续遇到问题再修改
    //暂停
    public void Pause()
    {
        _pause = true;
        _stop = false;
    }
    //恢复
    public void Recovery()
    {
        _pause = false;
        _stop = false;
    }
    //停止
    public void Stop()
    {
        _passedTime = 0;
        _passedCount = 0;
        _pause = false;
        _stop = true;
      
    }


    
    //如果不是精确的驱动 就用Time.deltatime  如果是要较准确的更新 可能要用 Time.fixedDeltaTime


   
}
