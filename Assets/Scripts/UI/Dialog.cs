/*
日期：
功能：对话框
作者：小人
版本号：
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Dialog : MonoBehaviour
{
    
    private UILayer uiLayer;

    public virtual void Init(UILayer _uiLayer)
    {
        uiLayer = _uiLayer;
    }


   public virtual void Close()
    {
        UIMgr.Instance.RemoveUIObj(this.gameObject);
    }
}
