/*
日期：
功能：人物配置表及人物配表数据结构
作者：小人
版本号：
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 功能：人物配置
class RoleTable : ConfigTable<RoleTableData, RoleTable>
{
   
}
//功能：人物配置表数据
public class RoleTableData : TableData
{


    public string RoleName;
    public string ModelPath;
    public Vector3 InitPos;
    public List<int> SkillList;
}
