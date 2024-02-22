/*
日期：
功能：技能管理器 注不是单例 每个能放技能的角色身上都要有一个技能管理器控制自身技能释放
作者：小人
版本号：
*/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillMgr : MonoBehaviour
{
    //技能拥有者
    Creature _owner;
    //技能目标
    Creature _target;
    //技能释放器
    SkillCaster _caster;

    // List<SkillLogicBase> skillLogicList = new List<SkillLogicBase>();
    SkillFireBall skillLogic;

   public bool IsCasting { get { return _caster.IsCasting; } }
    //初始化 
    public void Init(Creature owner,Creature target)
    {
        //设置技能拥有者及目标
        _owner = owner;
        _target = target;
        //释放器初始化
        _caster = new SkillCaster();
        _caster.Init(_owner);
        //初始化技能
        for(int i=0;i<GameSetting.CreatureMaxOwnSkills;i++)
        {
            //读表配技能暂不做
        }
        skillLogic = new SkillFireBall();
        skillLogic.Init(_owner);


    }

    //尝试释放技能
    public void TryCastSkill(int index)
    {
        //根据ID 查找 配置表
        //哪些表 技能表  粒子特效表  结算物表（比如火球之类的结算物）
        //能放吗？ CD？  要目标吗？ 啥类型的技能？
        //找到技能

        
        //在放技能吗 在退出 不能连放
        if (_caster.IsCasting) { return; }
        //停止移动
        (_owner as Role).StopMove();
        //开始放技能
        _caster.CastSkill(skillLogic,_target);
    }

   
    //技能时间线只驱动正在放的技能   不需要驱动全部的技能
    public void Loop()
    {
        _caster.Loop();
    }

    public void OnSkillEnd()
    {

    }

}
