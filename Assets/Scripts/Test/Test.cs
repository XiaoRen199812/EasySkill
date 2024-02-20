/*
日期：
功能：
作者：小人
版本号：
*/

using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;




public class Test : MonoBehaviour
{
    public Role role;
    public void Start()
    {
      role=FindObjectOfType<Role>();
    
    }


    private void Update()
    {
        
        if(Input.GetMouseButtonDown(0))
        {
            role.CastSkill(1);
        }



    }

    
   

}

 


