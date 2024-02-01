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
using UnityEngine.UI;




public class Test : MonoBehaviour
{

   


    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            UIMgr.Instance.RemoveUIObj("Skills(Clone)", UILayer.Fight);
        }
       
        if(Input.GetMouseButtonDown(1))
        {
            UIMgr.Instance.AddUIObj("Test/Skills", UILayer.Fight);
            
        }
       
    }

    //测试配置表读取
    private void TestConfig()
    {
        RoleTable.Instance.Load(Config.RoleTablePath);
       var dic= RoleTable.Instance.GetDic();
        Debug.Log(dic[1].ID);
        Debug.Log(dic[1].InitPos);
        Debug.Log(dic[1].ModelPath);
       
    }

}

 


