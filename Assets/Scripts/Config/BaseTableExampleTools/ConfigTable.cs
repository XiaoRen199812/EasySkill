/*
日期：
功能：泛型配置表 提供解析配表的功能
作者：小人
版本号：
*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;


/// <summary>
/// 泛型配置表 T为表格数据结构 K为配置表
/// </summary>
/// <typeparam name="T"></typeparam>
/// <typeparam name="K"></typeparam>
public class ConfigTable<T,K> :MonoSingleton<K>
    where T:TableData,new()
    where K:MonoSingleton<K>
{
    //关于反射性能开销较大，这里一般读取一次 把数据缓存
    private Dictionary<int,T> Cache = new Dictionary<int,T>();

    public void Load(string configPath)
    {
        Cache.Clear();

        //注意不同平台下路径不同 需根据平台去写
        string url = Application.streamingAssetsPath + configPath;

        //流读取配置表
        using (StreamReader stream = new StreamReader(url))
        {
            //读第一行获取相关表格的字段信息 确保数据的类型与字段信息一一对应
            string str =  stream.ReadLine();
          string[] fieldInfo=  str.Split(',');
            List<FieldInfo> infoList = new List<FieldInfo>();
            for(int i=0; i< fieldInfo.Length; i++)
            {
             var info=   typeof(T).GetField(fieldInfo[i]);
                if(info==null)
                {
                    Debug.LogError("当前字段程序未配置：" + fieldInfo[i]);
                    continue;
                }
                infoList.Add(info);
            }

            //正式循环读取
            string res;
            while((res=stream.ReadLine())!=null)
            {
              T data=  ReadALine(infoList, res);
                Cache.Add(data.ID, data);



            }
        }
    }

    private T ReadALine(List<FieldInfo> infoList,string str)
    {
        T data=new T();
        string[] values=str.Split(",");

       
        for (int i=0;i<infoList.Count;i++)
        {
          
            if (infoList[i].FieldType==typeof(int))
            {
               
                infoList[i].SetValue(data, Convert.ToInt32(values[i]));
            }
            else if(infoList[i].FieldType == typeof(string))
            {
                infoList[i].SetValue(data, Convert.ToString(values[i]));
            }
            else if(infoList[i].FieldType == typeof(bool))
            {
                infoList[i].SetValue(data, Convert.ToBoolean(values[i]));
            }
            else if (infoList[i].FieldType == typeof(double))
            {
                infoList[i].SetValue(data, Convert.ToDouble(values[i]));
            }
            else if (infoList[i].FieldType == typeof(float))
            {
                infoList[i].SetValue(data, float.Parse(values[i]));
            }
            //下面解析Vector3 List 等结构时 统一使用$隔开
            //这里只写一个解析Vector3的示例
            else if(infoList[i].FieldType == typeof(Vector3))
            {
                string[] temp= values[i].Split('$');
                if(temp.Length==3)
                {
                    Vector3 v = new Vector3(float.Parse(temp[0]), float.Parse(temp[1]), float.Parse(temp[2]));
                    infoList[i].SetValue(data, v);
                }
                else
                {
                    Debug.LogError("配置表Vector3配置错误");
                }
            }
            //List<int>
            else if(infoList[i].FieldType == typeof(List<int>))
            {
                string[] temp = values[i].Split('$');
                List<int> res = new List<int>();
                for(int k=0;k<temp.Length;k++)
                {
                    res.Add(int.Parse(temp[k]));
                }
                infoList[i].SetValue(data, res);

            }
            else if (infoList[i].FieldType == typeof(List<float>))
            {
                string[] temp = values[i].Split('$');
                List<float> res = new List<float>();
                for (int k= 0; k < temp.Length; k++)
                {
                    res.Add(float.Parse(temp[k]));
                }
                infoList[i].SetValue(data, res);

            }
            else if (infoList[i].FieldType == typeof(List<string>))
            {
                string[] temp = values[i].Split('$');
                List<string> res = new List<string>();
                for (int k = 0; k < temp.Length; k++)
                {
                    res.Add(temp[k]);
                }
                infoList[i].SetValue(data, res);

            }
        }
        return data;
    }

    public Dictionary<int, T> GetDic()
    {
        return Cache;
    }

    
}
