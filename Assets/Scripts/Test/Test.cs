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
    public Role role;

    Timer timer;
    public void Start()
    {
        //role = FindObjectOfType<Role>();

        //TimeMgr.Instance.StartTask(1, () => { Debug.Log("Task1"); }, 3);
        //TimeMgr.Instance.StartTask(0.5f, () => { Debug.Log("Task2"); },4);
    }


    private void Update()
    {
        /*
        if(Input.GetMouseButtonDown(0))
        {
         Ray ray=   Camera.main.ScreenPointToRay(Input.mousePosition);
           
            if (Physics.Raycast(ray,out RaycastHit hit))
            {
                role.SetMoveTarget(hit.point);
            }
        }
        */


        //timer.Loop();



    }

    //测试配置表读取
    private void TestConfig()
    {
       // RoleTable.Instance.Load(Config.RoleTablePath);
       //var dic= RoleTable.Instance.GetDic();
        //Debug.Log(dic[1].ID);
        //Debug.Log(dic[1].InitPos);
        //Debug.Log(dic[1].ModelPath);
       
    }

}

 


