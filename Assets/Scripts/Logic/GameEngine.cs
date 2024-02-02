/*
日期：
功能：游戏逻辑管理
作者：小人
版本号：
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEngine : MonoSingleton<GameEngine>
{
    
    private void OnEnable()
    {
        GameObject ui = ResLoader.ResGetInstance("UI/Canvas");
        UIMgr.Instance.Init(ui);

        RoleTable.Instance.Load(Config.RoleTablePath);
        var dic= RoleTable.Instance.GetDic();

        GameObject go=   ResLoader.ResGetInstance(dic[1].ModelPath);
        go.transform.position = dic[1].InitPos;
        go.transform.rotation = Quaternion.Euler(0, 80, 0);
        Role role=go.AddComponent<Role>();
        RoleTableData data = new RoleTableData();
        role.Init(data);
       
    }

}
