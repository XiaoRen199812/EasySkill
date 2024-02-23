/*
日期：
功能：普通攻击（平A）
作者：小人
版本号：
*/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillNormalAttack : SkillLogicBase
{
    public override void InitSkillTimeLineEvent()
    {
        _timeLine.AddEvent(0, 0, OnSkillStart);
        _timeLine.AddEvent(0, 21, OnAnimStart);
        _timeLine.AddEvent(0.4f, 2, OnNormalHit);
        _timeLine.AddEvent(1.6f, 1, OnAnimEnd);
        _timeLine.AddEvent(1.6f, 0, OnSkillEnd);
    }

    private  void OnNormalHit(int _nul)
    {
        //更据ID 查找配置表 找到对应的技能所对应的伤害

        if(_target!=null)
        {
            Debug.Log("伤害处理...");
        }
        else
        {
            Debug.Log("当前平A的目标未设置");
        }
    }
}
