/*
日期：
功能：角色类
作者：小人
版本号：
*/

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

    public void SetMoveTarget(Vector3 pos)
    {
        _target = pos;
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
}
