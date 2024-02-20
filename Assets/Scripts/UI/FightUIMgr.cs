/*
日期：
功能：战斗UI管理 包装小地图 摇杆  技能UI
作者：小人
版本号：
*/

using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class FightUIMgr : MonoBehaviour
{
   
    //摇杆
    public JoyStick joystick;

    //
    public FightBtnPannel fightBtnPannel;

    public void Start()
    {
        //摇杆 UI创建
        UIMgr.Instance.AddUIObj("UI/JoyStickRoot", UILayer.Fight);
        Transform stickRoot = TransformHelper.FindChild(this.transform, "JoystickRoot(Clone)");
        var imgDirBg = TransformHelper.FindChild(this.transform, "imgDirBg");
        var imgDirPoint = TransformHelper.FindChild(this.transform, "imgDirPoint");
        var arrowRoot = TransformHelper.FindChild(this.transform, "ArrowRoot");
        joystick = stickRoot.gameObject.AddComponent<JoyStick>();
        joystick. Init(stickRoot, imgDirBg, imgDirPoint, arrowRoot);

        //小地图创建
        UIMgr.Instance.AddUIObj("UI/MiniMap", UILayer.Fight);

        //技能UI创建 
      var skills=  UIMgr.Instance.AddUIObj("UI/Skills", UILayer.Fight);
        fightBtnPannel = skills.AddComponent<FightBtnPannel>();
    }

}
