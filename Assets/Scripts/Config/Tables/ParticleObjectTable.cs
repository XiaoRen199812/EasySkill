/*
日期：
功能：粒子特效表及粒子特效相关参数
作者：小人
版本号：
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleObjectTable : ConfigTable<ParticleObjectData, ParticleObjectTable>
{
 
   
    
}

public class ParticleObjectData:TableData
{
    public string ResPath;
    public float LifeTime;
    
}