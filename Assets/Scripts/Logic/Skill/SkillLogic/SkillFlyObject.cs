
/*
日期：
功能：远程普攻基础逻辑
作者：小人
版本号：
*/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 class SkillFlyObject : SkillLogicBase
{

    public override void InitSkillTimeLineEvent()
    {
        //火球预制体生成 播放火球动画  动画播放完毕切换为Idle  火球击中了物体怎么样
        //火球的销毁 速度由预制体脚本
        
        _timeLine.AddEvent(0, 0, OnSkillStart); //第0秒开始 调用OnSkillStart  参数0 ,看成方法实际未使用该参数
        _timeLine.AddEvent(0, 11, OnAnimStart); //第0秒开始 调用OnAnimStart
        _timeLine.AddEvent(0.02f, 1, CreateFlyBall);//第0.02秒开始 调用 CreateFlyBall 参数是1 
        _timeLine.AddEvent(1.6f, 11, OnAnimEnd);
        _timeLine.AddEvent(1.62f, 0, OnSkillEnd);
    }

   
    protected virtual void CreateFlyBall(int flyballId)
    {
        //根据flyballId 查找配置表  
        var flyObjDic = FlyObjectTable.Instance.GetDic();
        var res = flyObjDic[flyballId];
        
        if (res != null)
        {
           
            //生成火球

            GameObject go = ResLoader.ResGetInstance<GameObject>(res.ResPath);
            go.transform.position = _caster.transform.position + new Vector3(0, 0.8f, 0);

            go.transform.rotation = _caster.transform.rotation;
           
            FireBall fireball = go.AddComponent<FireBall>();
           
            fireball.Init(_caster, res.Radius, res.Height, res.FlySpeed, res.LifeTime, onHitSomeThing, _target);
        }
        else
        {
            Debug.LogError("未找到对应的飞行道具数据：" + flyballId);
        }
        
    }

    //击中回调
    private void onHitSomeThing(FireBall ball, Creature target)
    {
        
        if(target!=null)
        {
            Debug.Log(target.gameObject.name);
            Debug.Log("将来写伤害结算");
        }
        GameObject.Destroy(ball.gameObject);


    }
}
