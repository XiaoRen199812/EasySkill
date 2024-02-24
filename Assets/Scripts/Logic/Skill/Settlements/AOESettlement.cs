/*
日期：
功能：简单AOE粒子特效结算物
作者：小人
版本号：
*/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOESettlement : MonoBehaviour
{
    //释放者
    private Creature _caster;
    private CapsuleCollider _capsuleCollider;
    private Rigidbody _rigibody;

    private float _radius;
    private float _height;

    //击中回调
    private  Action<List<Creature>, AOESettlement> _onHitCallBack;
    //击中目标列表
    private List<Creature> _list = new List<Creature>();
    public void Init(Creature caster,int ParticleID, Action<List<Creature>,AOESettlement> OnHitCallBack)
    {
        _caster = caster;
        _onHitCallBack = OnHitCallBack;
        _rigibody =GetComponent<Rigidbody>();
        _rigibody.useGravity = false;

        _capsuleCollider= GetComponent<CapsuleCollider>();
        _capsuleCollider.isTrigger = true;

        var dic=   SettlementObjectTable.Instance.GetDic();

        if(dic.ContainsKey(ParticleID))
        {
            
            _radius = dic[ParticleID].Radius;
            _capsuleCollider.radius = _radius;
            _height = dic[ParticleID].Height;
            _capsuleCollider.height = _height;
            
        }

        _list.Clear();
        //启用碰撞器
        _capsuleCollider.enabled = true;
        //等待一物理帧更新 模拟瞬间的AOE伤害
        QuickCor.Instance.StartCor(OnHitWait());
    }


    //触发检测
    private void OnTriggerEnter(Collider other)
    {
       
        var creature=  other.gameObject.GetComponent<Creature>();
        if(creature==null)
        {
            return;
        }
        //避免自身影响
        if(creature==_caster)
        {
            return;
        }
        _list.Add(creature);
        Debug.Log(other.gameObject.name);
        Debug.Log(_list.Count);
    }

    //伤害等待
    public IEnumerator OnHitWait()
    {
        //确保等待物理帧检测结束
        yield return new WaitForFixedUpdate();
        
        OnHitEnd();
    }

    //结算结束
    public void OnHitEnd()
    {
        _capsuleCollider.enabled = false;
        _onHitCallBack?.Invoke(_list,this);
       
    }
}
