/*
日期：
功能：测试用配置表读取
作者：小人
版本号：
*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class TestTableReader : MonoBehaviour
{
    public Dictionary<int,TestTableData> dic=new Dictionary<int,TestTableData>();
    public void Start()
    {
        Read(Application.streamingAssetsPath + "/Role.csv.bytes");

        for (int i = 1; i <= dic.Count; i++)
        {
            Debug.Log("当前的角色是："+dic[i].RoleName);
        }
    }

    public void Read(string path)
    {
        using (StreamReader stream = new StreamReader(path))
        {
            stream.ReadLine();

            string str;
            while ((str = stream.ReadLine()) != null)
            {

                string[] array = str.Split(',');

                foreach (string item in array)
                {
                    Debug.Log(item);
                }

                TestTableData data = new TestTableData();

                var arr = typeof(TestTableData).GetProperties();
                foreach (var prop in arr)
                {
                    Debug.Log(prop.Name);

                }

                for (int i = 0; i < arr.Length; i++)
                {
                    if (arr[i].PropertyType == typeof(int))
                    {
                        arr[i].SetValue(data, Convert.ToInt32(array[i]));
                    }
                    else if (arr[i].PropertyType == typeof(string))
                    {
                        arr[i].SetValue(data, Convert.ToString(array[i]));
                    }
                }

                dic.Add(data.ID, data);

            }
        }
    }
}
