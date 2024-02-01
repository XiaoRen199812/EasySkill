/*
日期：
功能：UI分层管理 可以事件分发结合使用
作者：小人
版本号：
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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

        Transform _announcement = _root.transform.Find(" Announcement");
        LayerDic.Add(UILayer.Announcement, _announcement);
    }


    public  void AddUIObj(string resPath,UILayer uiLayer)
    {
        GameObject go=  ResLoader.ResGetInstance(resPath);
        go.transform.SetParent(LayerDic[uiLayer],false);
    }

    public  void RemoveUIObj(string uiName, UILayer uiLayer)
    {
      
        Transform tf=   TransformHelper.FindChild(LayerDic[uiLayer], uiName);
        if (tf != null)
        {
            GameObject.Destroy(tf.gameObject);
        }
    }
}
