/*
日期：
功能：飞行物体结算物   用于伤害处理
作者：小人
版本号：
*/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall :MonoBehaviour
{

    protected Rigidbody _rigidbody;
    protected float Speed;
    protected float LifeTime;
    
    protected CapsuleCollider _collider;
    protected float _colliderRadius;
    protected float _colliderHeight;

    //释放者和目标
    protected Creature _caster;
    protected Creature _target;

    //击中回调
    public Action<FireBall, Creature> onHitTargetCallback;


    public void Init(Creature caster,  float radius,float height,float speed,float lifeTime ,Action<FireBall, Creature> hitcallback, Creature target )
    {

        _caster = caster;
        _target = target;
        onHitTargetCallback = hitcallback;
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.useGravity = false;

        _collider = GetComponent<CapsuleCollider>();
        _colliderRadius = radius;
        _colliderHeight = height;

        _collider.radius = _colliderRadius;
        _collider.height = _colliderHeight;
        _collider.isTrigger = true;

        Speed = speed;
        LifeTime = lifeTime;

        //刚体速度设置
        if (target==null)
        {
            _rigidbody.velocity = transform.forward * Speed;
        }
        else
        {
            _rigidbody.velocity = (target.transform.position - transform.position).normalized * Speed;
        }

        GameObject.Destroy(this.gameObject, LifeTime);
    }



    private void OnTriggerEnter(Collider other)
    {
       
        var creature = other.transform.gameObject.GetComponent<Creature>();
        if(creature!=null)
        {
            if (creature != _target)
            {
                return;
            }
            if (onHitTargetCallback != null)
            {
                onHitTargetCallback(this, creature);//受到飞行道具攻击后的回调
            }
        }

        
       
    } 


}
