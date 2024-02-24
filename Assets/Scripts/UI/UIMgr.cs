/*
日期：
功能：UI分层管理 可以事件分发结合使用
作者：小人
版本号：
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//UI层级
public enum UILayer
{
    Normal,
    Touch,
    Fight,
    System,
    Announcement,
}
public class UIMgr : MonoSingleton<UIMgr>
{
    private GameObject _root;

    private Dictionary<UILayer,Transform> LayerDic = new Dictionary<UILayer,Transform>();
    public void Init(GameObject go)
    {
       
        _root = go;

        LayerDic.Clear();
        Transform _normall = _root.transform.Find("Normal");
        LayerDic.Add(UILayer.Normal, _normall);

        Transform _touch = _root.transform.Find("Touch");
        LayerDic.Add(UILayer.Touch, _touch);

        Transform _fight = _root.transform.Find("Fight");
        LayerDic.Add(UILayer.Fight, _fight);

        Transform _system = _root.transform.Find("System");
        LayerDic.Add(UILayer.System, _system);

        Transform _announcement = _root.transform.Find("Announcement");
        LayerDic.Add(UILayer.Announcement, _announcement);
    }

    //生成并设置UI物体的父节点
    public GameObject AddUIObj(string resPath,UILayer uiLayer)
    {
        GameObject go=  ResLoader.ResGetInstance(resPath);
        if (LayerDic.ContainsKey(uiLayer))
        {
            go.transform.SetParent(LayerDic[uiLayer], false);
        }
        return go;
       
    }

    public GameObject AddUIObj(string resPath,UILayer uiLayer,RectTransform rectTransform)
    {
        GameObject go = ResLoader.ResGetInstance(resPath);
        if (LayerDic.ContainsKey(uiLayer))
        {
            go.transform.SetParent(LayerDic[uiLayer], false);
            RectTransform rect = go.transform as RectTransform;
            rect = rectTransform;
        }
        return go;
    }

    public GameObject AddUIObj(string resPath, UILayer uiLayer, Vector3 pos)
    {
        GameObject go = ResLoader.ResGetInstance(resPath);
        if (LayerDic.ContainsKey(uiLayer))
        {
            go.transform.SetParent(LayerDic[uiLayer], false);

            RectTransform rect = go.transform as RectTransform;
            rect.anchoredPosition3D = pos;
        }
        return go;
    }

    //移除某层UILayer 下的某个名为 uiName 的物体
    //如果有重名的只移除找的第一个
    public  void RemoveUIObj(string uiName, UILayer uiLayer)
    {
        if (LayerDic.ContainsKey(uiLayer))
        {
            Transform tf = TransformHelper.FindChild(LayerDic[uiLayer], uiName);
            if (tf != null)
            {
                GameObject.Destroy(tf.gameObject);
            }
        }
    }
    public void RemoveUIObj(GameObject go)
    {
        
                GameObject.Destroy(go);
    }

    //查找某层UI
    public Transform FindLayer(UILayer uiLayer)
    {
        if(LayerDic.ContainsKey(uiLayer))
        {
            return LayerDic[uiLayer];
        }
        return null;
    }

    //移除某层UI下所有物体
    public void RemoveUILayer(UILayer uiLayer)
    {
        if (LayerDic.ContainsKey(uiLayer))
        {
            
            if (LayerDic[uiLayer].childCount >0)
            {
                TransformHelper.DestroyAllChild(LayerDic[uiLayer]);
            }     
        }
    }

    //移除所有UI物体
    public void RemoveAllUILayer()
    {

        RemoveUILayer(UILayer.Normal);
        RemoveUILayer(UILayer.Touch);
        RemoveUILayer(UILayer.Fight);
        RemoveUILayer(UILayer.System);
        RemoveUILayer(UILayer.Announcement);

    }
}
