/*
日期：
功能:配置类 有关资源加载的路径的相关变量统一填写在这里 
作者：小人
版本号：
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static  class Config 
{
    //为了避免外界修改改为Const 
    public const string TablePath ="/Table.csv.bytes";
    public const string RoleTablePath ="/Role.csv.bytes";
    public const string FlyObjectTablePath ="/FlyObject.csv.bytes";
}
