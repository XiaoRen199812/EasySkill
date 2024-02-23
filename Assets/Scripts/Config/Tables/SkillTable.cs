/*
日期：
功能：
作者：小人
版本号：
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTable : ConfigTable<SkillTableData,SkillTable>
{
    
}
   
    
public class SkillTableData:TableData
{
    public string SkillName;
    public string SkilDes;

    public float Damage;

    public float CastRange;
    public float PreTime;
    public float CD;
    public float DamageRange;

    public float DuringTime;

    public float Cost;

    public string SkillType;
}
