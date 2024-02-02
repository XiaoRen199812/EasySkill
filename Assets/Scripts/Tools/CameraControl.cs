/*
日期：
功能：摄像机工具 编辑器下调试设置摄像机位置旋转 运行时跟随玩家
作者：小人
版本号：
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

//注 可将PosOffest 设为(1.48, 9.92, -8.48) _smooth设为10
//摄像机的 rotation(47.3,-6.38,0)
public class CameraControl : MonoBehaviour
{

    private Transform _followTarget;
    [SerializeField]
    private Vector3 PosOffest;
    [SerializeField]
    private Vector3 RotOffest;
    [SerializeField]
    private float _smooth;

    private Vector3 _lateRotation;
    //(1.48, 9.92, -8.48) Pos
    //(45.3,3.6,0.16) Rot
    private void Start()
    {
        
        _followTarget = GameObject.FindGameObjectWithTag("Player").transform;

        
        _lateRotation = transform.rotation.eulerAngles;
    }
    private void LateUpdate()
    {

#if UNITY_EDITOR
        SetRotation();
#endif
        transform.position = Vector3.Lerp(transform.position, _followTarget.position + PosOffest, _smooth*Time.deltaTime);
      
       
    }

    private void SetRotation()
    {
       
        Vector3 temp = _lateRotation + RotOffest;
        
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(temp),_smooth*Time.deltaTime);



    }
}
