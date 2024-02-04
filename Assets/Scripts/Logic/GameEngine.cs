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
        //UI加载 初始化
        GameObject ui = ResLoader.ResGetInstance("UI/Canvas");
        UIMgr.Instance.Init(ui);

        //人物加载 初始化
        RoleTable.Instance.Load(Config.RoleTablePath);
        var dic= RoleTable.Instance.GetDic();
        GameObject go=   ResLoader.ResGetInstance(dic[1].ModelPath);
        go.transform.position = dic[1].InitPos;
        Role role= go.AddComponent<Role>();
        RoleTableData data = new RoleTableData();
        role.Init( data);
        
        //摄像机动态创建
         ResLoader.ResGetInstance("MainCamera");

        //摇杆 技能UI创建
         UIMgr.Instance.AddUIObj("UI/Skills", UILayer.Fight);
         UIMgr.Instance.AddUIObj("UI/JoyStickRoot", UILayer.Fight);

        _fightTf = UIMgr.Instance.FindLayer(UILayer.Fight);
        Transform stickRoot = TransformHelper.FindChild(_fightTf, "JoystickRoot(Clone)");
        
        JoyStick joystick= stickRoot.gameObject.AddComponent<JoyStick>();
        //var imgDirBg = TransformHelper.FindChild(stickRoot, "imgDirBg");
        //var imgDirPoint = TransformHelper.FindChild(stickRoot, "imgDirPoint");
        //var arrowRoot = TransformHelper.FindChild(stickRoot, "ArrowRoot");
        //joystick.Init(stickRoot, imgDirBg, imgDirPoint, arrowRoot);






    }

}
