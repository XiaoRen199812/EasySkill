/*
日期：
功能：人物角色动画控制
作者：小人
版本号：
*/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//动画ID枚举   不同类型的动画分区间方便之后扩展
public enum AnimID
{
    //基础动作 1-10
    Idle = 1,
    Run = 2,
    Dead = 3,

    //技能 11-20
    Atk1 = 11,
    Atk2 = 12,
    Atk3 = 13,

    //施法动作21-30
    Spell1 = 21,
    Spell2 = 22,
    Spell3 = 23,

    //状态 30-40
    Stun = 31,
}

public class CharacterAnimControl : MonoBehaviour
{


    private Animator _animator;
    private AnimatorStateInfo _stateInfo;
    //重置动画状态机
    public bool IsReset;
    private AnimatorClipInfo[] _clipInfo;


    public void Init(Animator anim)
    {
        _animator = anim;
    }

    #region 提供外界播放动画  注 仅对ATK1 ATK2 ATK3 播放时加了重置 其他后续补
    public void PlayIdle()
    {
        if (_animator != null)
        {
            _animator.SetInteger("State", 1);
        }
    }
    public void PlayRun()
    {
        if (_animator != null)
        {
            _animator.SetInteger("State", 2);
        }
    }
    public void PlayATK1()
    {
        if (_animator != null)
        {
            _animator.SetInteger("State", 11);
            IsReset = true;
        }
    }
    public void PlayATK2()
    {
        if (_animator != null)
        {
            _animator.SetInteger("State", 12);
            IsReset = true;
        }
    }
    public void PlayATK3()
    {
        if (_animator != null)
        {
            _animator.SetInteger("State", 13);
            IsReset = true;
        }
    }
    public void PlaySpell1()
    {
        if (_animator != null)
        {
            _animator.SetInteger("State", 21);
        }
    }
    public void PlaySpell2()
    {
        if (_animator != null)
        {
            _animator.SetInteger("State", 22);
        }
    }
    public void PlaySpell3()
    {
        if (_animator != null)
        {
            _animator.SetInteger("State", 23);
        }
    }
    public void PlayStun()
    {
        if (_animator != null)
        {
            _animator.SetInteger("State", 31);
        }
    }
    #endregion

    private void Update()
    {
        Check();
    }

    //检测动画完毕切换回站立
    //可用动画事件可不必使用代码
    private void Check()
    {
        if (IsReset)
        {
            //获取动画状态信息
            _stateInfo = _animator.GetCurrentAnimatorStateInfo(0);

            //当前State ID->名字
            // string val=  GetAnimName(_animator.GetInteger("State"));
            string val = GetCurrentAnimName();
            
            if (_stateInfo.normalizedTime > 0.95f && _stateInfo.IsName(val))
            {
                
                Reset();
                IsReset = false;

            }

        }
    }
    
    //重置动画状态机
    private void Reset()
    {
        if (_animator != null)
        {
            _animator.SetInteger("State", 1);
        }
    }

    #region 获取当前播放动画的名字
    //基于State  找到对应的枚举 返回相应的字符串 
    private string GetAnimName(int val)
    {
        Enum e = (AnimID)val;
        string res = Enum.GetName(typeof(AnimID), e);
        return res;

    }

    //通过AnimaterClipInfo 找对应的名字
    private string GetCurrentAnimName()
    {
        string str = null;
        if (_animator != null)
        {
            AnimatorClipInfo[] clipInfo = _animator.GetCurrentAnimatorClipInfo(0);
            if (clipInfo.Length > 0)
            {
                AnimationClip playingClip = clipInfo[0].clip;
                str = playingClip.name;
               // Debug.Log("当前播放的名字：" + str);
            }
        }
        return str;
    }
    #endregion
}

