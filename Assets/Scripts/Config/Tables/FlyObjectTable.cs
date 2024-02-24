/*
日期：
功能：飞行道具表及飞行道具数据
作者：小人
版本号：
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyObjectTable : ConfigTable<FlyObjectTableData,FlyObjectTable>
{
    
}

public class FlyObjectTableData:TableData
{
    public string Name;
    public string ResPath;
    public float FlySpeed;
    public float Radius;
    public float Height;
    public float LifeTime;
}