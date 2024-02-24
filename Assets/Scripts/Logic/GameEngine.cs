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
        var RoleDic = RoleTable.Instance.GetDic();
        FlyObjectTable.Instance.Load(Config.FlyObjectTablePath);
        SkillTable.Instance.Load(Config.SkillTablePath);
        ParticleObjectTable.Instance.Load(Config.ParticleObjectPath);
        SettlementObjectTable.Instance.Load(Config.SettlementObjectPath);
        //人物加载 初始化
        
        GameObject go=ResLoader.ResGetInstance(RoleDic[1].ModelPath);
        go.transform.position = RoleDic[1].InitPos;
        Role role= go.AddComponent<Role>();
        RoleTableData data = RoleDic[1];
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
