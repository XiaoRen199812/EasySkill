/*
日期：
功能：对话框管理类
作者：小人
版本号：
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogMgr : MonoSingleton<DialogMgr>
{

    public List<Dialog> dialogList = new List<Dialog>();
   public Dialog LoadDialog(string resPath,UILayer uiLayer)
    {
        var go=   UIMgr.Instance.AddUIObj(resPath, uiLayer);
        Dialog dialog= go.AddComponent<Dialog>();
        dialog.Init(uiLayer);
        dialogList.Add(dialog);
        return dialog;
    }

    public void CloseDialog(Dialog dialog)
    {
        if (dialog != null)
        {
            dialog.Close();
            dialogList.Remove(dialog);
        }
        
    }

    public void CloseAllDialog()
    {
        if(dialogList.Count > 0)
        {
            foreach (var dialog in dialogList)
            {
                dialog.Close();
            }
        }
      

        dialogList.Clear();
    }
   
}
