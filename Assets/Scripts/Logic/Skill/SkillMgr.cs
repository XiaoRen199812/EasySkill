/*
日期：
功能：技能管理器 注不是单例 每个能放技能的角色身上都要有一个技能管理器控制自身技能释放
作者：小人
版本号：
*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class SkillMgr : MonoBehaviour
{
    //技能拥有者
    Creature _owner;
    //技能目标
    Creature _target;
    //技能释放器
    SkillCaster _caster;

    List<SkillObject> _skillList= new List<SkillObject>();
   

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
            //注意索引 List 从0开始 字典的键当前项目是从1开始
            SkillObject skillObj = new SkillObject();
             int id= (_owner as Role).skillIDList[i];
           var skillDic= SkillTable.Instance.GetDic();
            skillObj.skillTableData = skillDic[id];
            //创建对应的逻辑 
            //提供两种思路 1.读表根据整数-》对应的技能类型枚举-》创建对应的技能
            //2.反射：根据字符串-》创建对应的类型

            //这里就使用反射了 
            string skillType= skillDic[id].SkillType;
            skillObj.skillLogicBase = typeof(SkillLogicBase).Assembly.CreateInstance(skillType) as SkillLogicBase;

            skillObj.skillLogicBase.Init(_owner);
            _skillList.Add(skillObj);
        }



        
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

        //注意列表中的索引和字典的索引
        var skillObj = _skillList[index - 1];
        var skillLogic = _skillList[index - 1].skillLogicBase;
        //如果技能不需要目标 CastRange 为-1 不检测目标 直接检测
        if(skillObj.skillTableData.CastRange!=-1)
        {
            //需要施法目标 才施法距离判断
            if (_target == null) { Debug.LogError("该技能需要目标，但目标不存在"); return; }
            var dis = MathEX.DistanceIngoreY(this.transform.position, _target.gameObject.transform.position);
            if(dis>skillObj.skillTableData.CastRange)
            {
                Debug.LogError("目标超出该角色的施法距离");
                return;
            }
        }
        
        //开始放技能
        _caster.CastSkill(skillLogic,_target);
    }

   
    //技能时间线只驱动正在放的技能   不需要驱动全部的技能
    public void Loop()
    {
        _caster.Loop();
    }

   

}
