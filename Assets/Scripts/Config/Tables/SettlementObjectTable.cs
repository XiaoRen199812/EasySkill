/*
日期：
功能：结算物表及相关数据
作者：小人
版本号：
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettlementObjectTable : ConfigTable<SettlementData,SettlementObjectTable>
{
 
   
   
}

public class SettlementData:TableData
{
    public float Radius;
    public float Height;
}