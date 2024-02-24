/*
日期：
功能：
作者：小人
版本号：
*/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSimpleAOE : SkillLogicBase
{
    public override void InitSkillTimeLineEvent()
    {
        _timeLine.AddEvent(0, 0, OnSkillStart);
        _timeLine.AddEvent(0, 13, OnAnimStart);

        _timeLine.AddEvent(0.2f, 1, OnParticleCreate);

        _timeLine.AddEvent(1.6f, 1, OnAnimEnd);
        _timeLine.AddEvent(1.6f, 0, OnSkillEnd);
    }

    //创建粒子特效
    private void OnParticleCreate(int ParticleID)
    {
        
    }
}
