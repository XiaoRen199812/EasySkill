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

    public void Load(string ConfigPath)
    {
        Cache.Clear();

        //注意不同平台下路径不同 需根据平台去写
        string url = Application.streamingAssetsPath + ConfigPath;

        //流读取配置表
        using (StreamReader stream = new StreamReader(url))
        {
            //读第一行获取相关表格的属性信息 确保数据的类型与属性信息一一对应
          string str=  stream.ReadLine();
          string[] proInfo=  str.Split(',');
            List<PropertyInfo> infolist = new List<PropertyInfo>();
            for(int i=0; i<proInfo.Length; i++)
            {
             var info=   typeof(T).GetProperty(proInfo[i]);
                infolist.Add(info);
            }

            //正式循环读取
            string res;
            while((res=stream.ReadLine())!=null)
            {
              T data=  ReadALine(infolist, res);
                Cache.Add(data.ID, data);



            }
        }
    }

    private T ReadALine(List<PropertyInfo> infolist,string str)
    {
        T data=new T();
        string[] values=str.Split(",");

        for(int i=0;i<infolist.Count;i++)
        {
            if (infolist[i].PropertyType==typeof(int))
            {
                infolist[i].SetValue(data, Convert.ToInt32(values[i]));
            }
            else if(infolist[i].PropertyType == typeof(string))
            {
                infolist[i].SetValue(data, Convert.ToString(values[i]));
            }
            else if(infolist[i].PropertyType == typeof(bool))
            {
                infolist[i].SetValue(data, Convert.ToBoolean(values[i]));
            }
            else if (infolist[i].PropertyType == typeof(double))
            {
                infolist[i].SetValue(data, Convert.ToDouble(values[i]));
            }
            //下面解析Vector3 List 等结构时 统一使用$隔开
            //这里只写一个解析Vector3的示例
            else if(infolist[i].PropertyType==typeof(Vector3))
            {
                string[] temp= values[i].Split('$');
                if(temp.Length==3)
                {
                    Vector3 v = new Vector3(float.Parse(temp[0]), float.Parse(temp[1]), float.Parse(temp[2]));
                    infolist[i].SetValue(data, v);
                }
                else
                {
                    Debug.LogError("配置表Vector3配置错误");
                }
            }
            //List<int>
            else if(infolist[i].PropertyType == typeof(List<int>))
            {
                string[] temp = values[i].Split('$');
                List<int> res = new List<int>();
                for(int k=0;k<temp.Length;k++)
                {
                    res.Add(int.Parse(temp[k]));
                }
                infolist[i].SetValue(data, res);

            }
            else if (infolist[i].PropertyType == typeof(List<float>))
            {
                string[] temp = values[i].Split('$');
                List<float> res = new List<float>();
                for (int k= 0; k < temp.Length; k++)
                {
                    res.Add(float.Parse(temp[k]));
                }
                infolist[i].SetValue(data, res);

            }
            else if (infolist[i].PropertyType == typeof(List<string>))
            {
                string[] temp = values[i].Split('$');
                List<string> res = new List<string>();
                for (int k = 0; k < temp.Length; k++)
                {
                    res.Add(temp[k]);
                }
                infolist[i].SetValue(data, res);

            }
        }
        return data;
    }

    public Dictionary<int, T> GetDic()
    {
        return Cache;
    }

}
