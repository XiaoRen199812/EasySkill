/*
日期：
功能：游戏里脚本的数据配置  比如寻路组件的停止距离等参数  注Config 专门用来配路径
作者：小人
版本号：
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameSetting
{ 
   //角色寻路数据
    public const float StoppingDistance = 0.05f;
    public const float Acceleration = 20;
    public const float AngularSpeed = 360f;
}
