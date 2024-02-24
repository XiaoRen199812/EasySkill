/*
日期：
功能：技能基础的逻辑  由于技能复杂 实现由子类实现
作者：小人
版本号：
*/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 public abstract class SkillLogicBase
{
    //技能释放者
    public Creature _caster;

    //技能目标 可能为空
    public Creature _target;

    //技能结束的回调
    protected Action _skillEndCallback;

    protected SkillTimeLine _timeLine = new SkillTimeLine();

    //初始化技能的时间线   即何时技能前摇动画播放 特效播放 动画播放  
    //由时间线控制技能节奏
    public abstract void InitSkillTimeLineEvent();


    public void Init(Creature caster)
    {
        _caster = caster;
        InitSkillTimeLineEvent();
    }

    public virtual void OnSkillStart(int __null)
    {
       
        //技能CD的初始化(暂时先不实现)

        //将施法者的朝向面向目标
        if (_target != null)
        {
            var realWatchPos = new Vector3(_target.transform.position.x, _caster.transform.position.y, _target.transform.position.z);
            _caster.transform.LookAt(realWatchPos);
        }
    }

    protected virtual void OnAnimStart(int animID)
    {

       
        (_caster as Role).SetAnim(animID);
    }

    protected virtual void OnAnimEnd(int endAnimID)
    {
      
        Role role = _caster as Role;
        if (role != null && role.GetAnim() != endAnimID)
        { 
        (_caster as Role).SetAnim(endAnimID);
         }
    }

    protected virtual void OnSkillEnd(int _nul)
    {
       
        _target = null;
        _skillEndCallback?.Invoke();
    }

    //开始逻辑 开始技能时间线驱动
    public void Start( Action skillEndCallback,Creature target)
    {
        _skillEndCallback = skillEndCallback;
        _target = target;
        //开启时间线
        _timeLine.Start();
    }

    //驱动时间线更新
    public void Loop()
    {
        _timeLine.Loop(Time.deltaTime);
    }
}
