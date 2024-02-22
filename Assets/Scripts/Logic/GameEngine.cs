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
    private Transform _fightTf;
   
    private void OnEnable()
    {
        //读表
        RoleTable.Instance.Load(Config.RoleTablePath);
        var dic = RoleTable.Instance.GetDic();
        FlyObjectTable.Instance.Load(Config.FlyObjectTablePath);


        //人物加载 初始化
        GameObject go=   ResLoader.ResGetInstance(dic[1].ModelPath);
        go.transform.position = dic[1].InitPos;
        Role role= go.AddComponent<Role>();
        RoleTableData data = new RoleTableData();
        role.Init( data);
        
        //摄像机动态创建
         ResLoader.ResGetInstance("MainCamera");

        //UI加载 初始化
        GameObject ui = ResLoader.ResGetInstance("UI/Canvas");
        UIMgr.Instance.Init(ui);
        _fightTf = UIMgr.Instance.FindLayer(UILayer.Fight);

        //战斗UI
        _fightTf.gameObject.AddComponent<FightUIMgr>();

        //QuickCor Init
        QuickCor.Instance.Init();
    }

}
