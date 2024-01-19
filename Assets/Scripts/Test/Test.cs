/*
日期：
功能：
作者：小人
版本号：
*/

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Test : MonoBehaviour
{
    CharacterAnimControl characterAnimControl;
    private Animator animator;
    void Start()
    {
        TestConfig();

        animator = GetComponent<Animator>();
        if (characterAnimControl == null)
        {
            characterAnimControl = transform.AddComponent<CharacterAnimControl>();
            characterAnimControl.Init(animator);
        }


    }

    //测试动画
    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("Q");
            characterAnimControl.PlayATK1();
            characterAnimControl.IsReset = true;
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log("W");
            characterAnimControl.PlayATK2();
            characterAnimControl.IsReset = true;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("E");
            characterAnimControl.PlayATK3();
            characterAnimControl.IsReset = true;
        }


    }

    //测试配置表读取
    private void TestConfig()
    {
        RoleTable.Instance.Load(Config.RoleTablePath);
       var dic= RoleTable.Instance.GetDic();
        Debug.Log(dic[1].RoleName);
        Debug.Log(dic[2].RoleName);
    }

}

 


