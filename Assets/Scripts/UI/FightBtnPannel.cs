/*
日期：
功能：管理所有的技能按钮并注册事件
作者：小人
版本号：
*/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FightBtnPannel : MonoBehaviour
{

    public Button _btnCommonAttack;
    public Button _btnAtk1;
    public Button _btnAtk2;
    public Button _btnAtk3;    
   
     void Start () 
    {
        
       var res0=  TransformHelper.FindChild(this.transform, "CommonAttack");
        _btnCommonAttack = res0.GetComponent<Button>();
        _btnCommonAttack.onClick.AddListener(() => { CommonAttack(); });
       
       var res1 = TransformHelper.FindChild(this.transform, "Attack1");
        _btnAtk1 = res1.GetComponent<Button>();
        res1.GetComponent<Button>().onClick.AddListener(() => {Attack1(); });

        var res2 = TransformHelper.FindChild(this.transform, "Attack2");
        _btnAtk2 = res2.GetComponent<Button>();
        res2.GetComponent<Button>().onClick.AddListener(() => { Attack2(); });

        var res3= TransformHelper.FindChild(this.transform, "Attack3");
        _btnAtk3 = res3.GetComponent<Button>();
        res3.GetComponent<Button>().onClick.AddListener(() => { Attack3(); });

    }

    private void Attack3()
    {
        Debug.Log("持续伤害");
    }

    private void Attack2()
    {
        Debug.Log("AOE");
    }

    private void Attack1()
    {
        Debug.Log("火球");
    }

    

    private void CommonAttack()
    {
        Debug.Log("普攻");
    }
}
