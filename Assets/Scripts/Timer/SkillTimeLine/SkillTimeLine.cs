/*
日期：
功能：网上看到的其他人写的，直接Copy来用了，搭配LineEvent用
作者：小人
版本号：
*/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTimeLine
{

    private bool _isStart;//是否开始
    private bool _isPause;//是否暂停
    private float _curTime;//当前计时    
    private Action _reset;//重置事件
    private Action<float> _update;//每帧回调

    public SkillTimeLine()
    {
        Reset();
    }

    /// <summary>
    /// 添加事件
    /// </summary>
    /// <param name="delay">延迟时间</param>
    /// <param name="id">ID（穿透参数）</param>
    /// <param name="method">执行的回调</param>
    public void AddEvent(float delay, int id, Action<int> method)
    {
        LineEvent param = new LineEvent(delay, id, method);
        _update += param.Invoke;//条件判断
        _reset += param.Reset;
    }

    //开始时间线
    public void Start()
    {
        Reset();
        _isStart = true;
        _isPause = false;
    }

    //重置（还原）
    public void Reset()
    {
        _curTime = 0;//时间线计时归零
        _isStart = false;//不开始
        _isPause = false;//没开始就不用谈暂停

        if (null != _reset)
        {
            _reset();//在时间线的LineEvent里面去调用所有事件（Event）的reset函数，所有的时间线事件（LineEvent）也要归零
        }
    }

    public void Loop(float deltaTime)
    {

        if (!_isStart || _isPause)//时间线开始并且没有被暂停就进入下面
        {
            return;
        }
        _curTime += deltaTime;
        if (null != _update)
        {
            _update(_curTime);
        }
    }
}
