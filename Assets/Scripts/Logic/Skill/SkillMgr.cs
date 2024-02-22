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
    Creature _owner;
    Creature _target;
    // List<SkillLogicBase> skillLogicList = new List<SkillLogicBase>();
    SkillFireBall skillLogic;

   
    //初始化 
    public void Init(Creature owner,Creature target)
    {
        _owner = owner;
        _target = target;
        //初始化技能
        skillLogic = new SkillFireBall();
        skillLogic.Init(_owner);
    }

    //尝试释放技能
    public void TryCastSkill(int index)
    {
        //根据ID 查找 配置表
        //哪些表 技能表  粒子特效表  结算物表（比如火球之类的结算物）

        //能放吗？ CD？  要目标吗？ 啥类型的技能？


        //开始放技能
        StartSkill(index);
    }

   
    //放技能
    private void StartSkill(int index)
    {
        
        skillLogic.Start(null, _target); 
    }
    public void Loop()
    {
        skillLogic.Loop();
    }

}
