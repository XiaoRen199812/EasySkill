/*
日期：
功能：角色类
作者：小人
版本号：
*/

using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Role : MonoBehaviour
{
    //配置表数据 只读不写
    private RoleTableData _data;

    private NavMeshAgent _agent;

    private CharacterAnimControl _animControl;

    private Animator _animator;

    //移动目标可空  主要避免引入过多标志位
    private Vector3? _target=null;

    public void Init(RoleTableData data)
    {
        _data = data;
        _agent = transform.AddComponent<NavMeshAgent>();

        _animControl=transform.AddComponent<CharacterAnimControl>();

        _animator=GetComponent<Animator>();
        _animControl.Init(_animator);

        _agent.stoppingDistance = 0.05f;
        _agent.acceleration = 20;
        _agent.angularSpeed = 360f;
        
    }

    //public void SetMoveTarget(Vector3 pos)
    //{
    //    _target = pos;
    //}


    public void SetMoveTarget(object sender,EventArgs args)
    {
        JoyStickMoveEventArgs e= args as JoyStickMoveEventArgs;
        
        _target = this.transform.position+e.target;
    }

    private void MoveToTarget()
    {
        if(_target!=null)
        {
            _agent.SetDestination(_target.Value);
            _animControl.PlayRun();
            if(Vector3.Distance(transform.position,new Vector3(_target.Value.x,transform.position.y,_target.Value.z))<_agent.stoppingDistance)
             {
                _animControl.PlayIdle();
                _target = null;
            }
        }
    }
    private void Update()
    {
        MoveToTarget();
    }


    //事件注册 
    private void OnEnable()
    {
        EventCenter.Instance.RegesitEvent(EventName.eventJoyStickMove, SetMoveTarget);
    }

    

    private void OnDisable()
    {
        EventCenter.Instance.RemoveEvent(EventName.eventJoyStickMove, SetMoveTarget);
    }
}
