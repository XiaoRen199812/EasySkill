/*
日期：
功能：
作者：小人
版本号：
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
 
   
     void Start () 
    {
        Table.Instance.Load(Config.TablePath);
      var dic=  Table.Instance.GetDic();
        Debug.Log(dic[1].ID);

        RoleTable.Instance.Load(Config.RoleTablePath);
        var dic1 = RoleTable.Instance.GetDic();
        Debug.Log(dic1[2].RoleName);
    }



}
class Table : ConfigTable<TableData, Table>
{ }

class RoleTable:ConfigTable<RoleTableData,RoleTable>
{

}

