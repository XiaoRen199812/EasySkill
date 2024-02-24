/*
日期：
功能：简单AOE技能逻辑
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
        //创建粒子结算物
        _timeLine.AddEvent(0.2f, 1, OnParticle);
        //伤害结算
        _timeLine.AddEvent(0.2f,1,OnSettlement);
            
        _timeLine.AddEvent(1.6f, 1, OnAnimEnd);
        
        _timeLine.AddEvent(1.7f, 0, OnSkillEnd);
    }

    

    //创建粒子特效
    private void OnParticle(int ParticleID)
    {
        //根据ID查找对应的预制体 
      var parDic=  ParticleObjectTable.Instance.GetDic();
        if(parDic.ContainsKey(ParticleID))
        {
         string resPath=   parDic[ParticleID].ResPath;
           GameObject  _particle =    ResLoader.ResGetInstance(resPath);

            _particle.transform.position = (_caster as Role)._btnPoint.position;
            //注粒子特效 暂不勾选Loop
          var particles= _particle.GetComponentsInChildren<ParticleSystem>();
            foreach(var item in particles)
            {
               var main= item.main;
                main.loop = false;
            }

            float lifeTime=  parDic[ParticleID].LifeTime;
            GameObject.Destroy(_particle, lifeTime);
        }
    }

    //伤害结算
    private void OnSettlement(int SettlementID)
    {
        //生成粒子结算物
        var temp = ResLoader.ResGetInstance("ParticleSettlement/Settlement");
        temp.transform.position = (_caster as Role)._btnPoint.position;
        AOESettlement _settlement = temp.AddComponent<AOESettlement>();
        _settlement.Init( _caster,SettlementID, OnHitSomething);
    }


    private void OnHitSomething(List<Creature> creatures,AOESettlement settlement)
    {
        Debug.Log(creatures.Count);
        Debug.Log("AOE伤害自行处理");
        GameObject.Destroy(settlement.gameObject);
    }

   
}
