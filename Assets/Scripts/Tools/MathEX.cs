/*
日期：
功能：扩展脚本，提供一些辅助功能 比如float数相等判断
作者：小人
版本号：
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MathEX
{

    //浮点数是否相等 误差小于0.00001认为相等
    public static bool FloatEqual(float a, float b)
    {

        return Mathf.Abs(a - b) < 0.00001f;
    }


    //3D游戏中经常有的距离判断
    public static float DistanceIngoreY(Vector3 a,Vector3 b)
    {
      return  Vector3.Distance(new Vector3(a.x, 0, a.y), new Vector3(b.x, 0, b.y));
    }

}
