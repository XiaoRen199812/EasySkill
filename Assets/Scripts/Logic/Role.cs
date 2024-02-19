/*
日期：
功能：角色类  游戏当中可以攻击的除了主角 友方角色 敌方角色之外 还用小怪 Npc 需再抽象一个父类 暂不抽象
作者：小人
版本号：
*/

using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Role :Creature
{
    //配置表数据 只读不写
    private RoleTableData _data;

    private NavMeshAgent _agent;

   

    private Animator _animator;

    //移动目标可空  主要避免引入过多标志位
    private Vector3? _target=null;

   
    //技能测试
    SkillLogicBase _skillLogicBase;
    public Creature Target;
    public void Init(RoleTableData data)
    {
        _data = data;
        _agent = transform.AddComponent<NavMeshAgent>();


        _animator=GetComponent<Animator>();



        _agent.stoppingDistance = GameSetting.StoppingDistance;
        _agent.acceleration = GameSetting.Acceleration;
        _agent.angularSpeed = GameSetting.AngularSpeed;

        //创建火球逻辑 初始化相关事件
        _skillLogicBase = new SkillFireBall();
        _skillLogicBase.Init(this);

        Target = GameObject.Find("Sphere").GetComponent<Creature>();
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
            SetAnim(2);
            if(MathEX.DistanceIngoreY(transform.position,_target.Value)<_agent.stoppingDistance)
             {
                SetAnim(1);
                _target = null;
            }
        }
    }
    private void Update()
    {
        MoveToTarget();

        if(Input.GetKeyDown(KeyCode.Q))
        {
            _skillLogicBase.Start(null, Target);
            
        }
        _skillLogicBase.Loop();

       
    }

    public void SetAnim(int animID)
    {
        _animator.SetInteger("State", animID);
    }

    public int GetAnim()
    {
     return   _animator.GetInteger("State");
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
