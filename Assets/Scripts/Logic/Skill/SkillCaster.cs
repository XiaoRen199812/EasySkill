/*
日期：
功能：技能释放器 记录当前释放的技能 避免连点 放技能时人物不会移动 不响应其他点击事件
作者：小人
版本号：
*/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillCaster 
{
    //拥有者
    Creature _owner;

    //是否在释放技能
    public bool IsCasting { get { return _castingSkill != null; } }

    public void Init(Creature owner)
    {
        _owner = owner;
    }

    //当前正在释放的技能
    SkillLogicBase _castingSkill;

    //释放技能
    internal void CastSkill(SkillLogicBase skillLogic,Creature Target)
    {
        _castingSkill=skillLogic;
        skillLogic.Start(OnSkillLogicEnd, Target);
    }


    //驱动正在释放的技能
    internal void Loop()
    {
        if(_castingSkill != null)
        {
            _castingSkill.Loop();
        }
    }

    public void OnSkillLogicEnd()
    {
        _castingSkill = null;
    }
}
