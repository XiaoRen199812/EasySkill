/*
日期：
功能：角色类  游戏当中可以攻击的除了主角 友方角色 敌方角色之外 还用小怪 Npc 需再抽象一个父类 暂不抽象
作者：小人
版本号：
*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
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

    private SkillMgr _skillMgr;

    //角色ID
   public int RoleID;
    //技能ID列表  为了在控制台能直观地看到技能ID方便后续查找
    public List<int> skillIDList=new
        List<int>();
    //技能释放的相关点位
    public Transform _midPoint;
    public Transform _btnPoint;
    public void Init(RoleTableData data)
    {
       
        _agent = this.gameObject.AddComponent<NavMeshAgent>();
        _animator=GetComponent<Animator>();
        _agent.stoppingDistance = GameSetting.StoppingDistance;
        _agent.acceleration = GameSetting.Acceleration;
        _agent.angularSpeed = GameSetting.AngularSpeed;

        

        //模拟人物技能数据的初始化
        _data = data;
        RoleID = _data.ID;
        skillIDList = _data.SkillList;
        //注意初始化的先后顺序
        _skillMgr = gameObject.AddComponent<SkillMgr>();
        _skillMgr.Init(this, null); ;

        SetPoint();
    }

    //public void SetMoveTarget(Vector3 pos)
    //{
    //    _target = pos;
    //}
    public void SetPoint()
    {
        var res1= TransformHelper.FindChild(this.gameObject.transform, "MidPoint");
        if (res1!=null)
        {
            _midPoint = res1;
        }
        else
        {
            Debug.LogError("技能相关释放点当前模型未设置");
        }
        var res2 = TransformHelper.FindChild(this.gameObject.transform, "ButtonPoint");
        if (res2 != null)
        {
            _btnPoint = res2;
        }
        else
        {
            Debug.LogError("技能相关释放点当前模型未设置");
        }
    }

    public void SetMoveTarget(object sender,EventArgs args)
    {
        JoyStickMoveEventArgs e= args as JoyStickMoveEventArgs;
        
        _target = this.transform.position+e.target;
    }

    private void MoveToTarget()
    {
        //放技能时不响应移动事件
        if(_skillMgr.IsCasting)
        {
            return;
        }
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
    //技能释放 UI按钮按下时调用该方法 内部由技能管理器负责释放
    public void CastSkill(int index)
    {
        _skillMgr.TryCastSkill(index);
    }
    private void Update()
    {
        MoveToTarget();

        Loop();



    }

    public void SetAnim(int animID)
    {
        _animator.SetInteger("State", animID);
    }

    public int GetAnim()
    {
     return   _animator.GetInteger("State");
    }

    // 停止 
    public void StopMove()
    {
       
        _target = null;
       
        SetAnim(1);
    }
    //驱动技能时间线的更新
    public void Loop()
    {
        _skillMgr.Loop();
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
