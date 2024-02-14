
/*
日期：
功能：摇杆脚本 
作者：小人
版本号：
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class JoyStick: MonoBehaviour,IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    public Transform _root;

    //摇杆的背景图(大图）也是内球的圆心
    private Transform _imgDirBg;

    //摇杆的小球
    private Transform _imgDirPoint;

    //摇杆的箭头(默认隐藏 移动摇杆时出现 并转向相应方向）
    private Transform _arrowRoot;

    //初始点
    private Vector3 InitPos;

    //小球移动最大距离
    private float Distance=80;

    

    //计算人物移动目标点相关系数
    private float S = 1;
    public void Init(Transform root,Transform imgDirBg,Transform imgDirPoint,Transform arrowRoot)
    {
        _root = root;
        _imgDirBg = imgDirBg;
        _imgDirPoint = imgDirPoint;
        _arrowRoot = arrowRoot;
      
        
        _arrowRoot.gameObject.SetActive(false);
        InitPos = _imgDirBg.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        _arrowRoot.gameObject.SetActive(true);

    
        BallMove(eventData.position);
        ArrowRotate(eventData.position);
       
        

        Vector3 dir = new Vector3(eventData.position.x, eventData.position.y, 0) - _imgDirBg.position;
        Vector3 D = dir.normalized;
        JoyStickMoveEventArgs e = new JoyStickMoveEventArgs();
        e.target = new Vector3(D.x, 0, D.y) * S;
        EventCenter.Instance.TriggerEventAndBroadcastAll(EventName.eventJoyStickMove,this,e);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _imgDirBg.position = eventData.position;
        
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        
        _imgDirBg.position= InitPos;

        _imgDirPoint.position = InitPos;

        _arrowRoot.gameObject.SetActive(false);
    }

    private void BallMove(Vector3 pos)
    {
        //1.判断内球与圆心的连线的长度 是否小于外圈半径
        Vector3 dir = pos - _imgDirBg.position;

        float mul = dir.magnitude > Distance ? Distance : dir.magnitude;

        
        _imgDirPoint.position = _imgDirBg.position+dir.normalized*mul;
        
    }

    private void ArrowRotate(Vector3 pos)
    {
        Vector3 dir = pos - _imgDirBg.position;
        float angle=  Vector3.Angle(new Vector3(1,0,0), dir);
        //angle 角度返回值0- 180 转过180会出问题 比如 它是240 计算出来120
        //点乘     a·b=|a|·|b|cos<a,b>    Vector3.Dot (a, b)
        //叉乘      c =a x b  其中a,b,c均为向量。Vector3.Cross (a, b);

        Vector3 a1 = Vector3.Cross(new Vector3(1, 0, 0), dir.normalized);
        angle = a1.z > 0 ?angle: 360 - angle;
        (_arrowRoot.transform as RectTransform).rotation=Quaternion.Euler(0,0,angle);
    }
     
    //初始化可以在这里，一般交给外面做
    //private void Start()
    //{
    //   var imgDirBg= TransformHelper.FindChild(this.transform, "imgDirBg");
    //   var imgDirPoint = TransformHelper.FindChild(this.transform, "imgDirPoint");
    //   var arrowRoot = TransformHelper.FindChild(this.transform, "ArrowRoot");
        
    //   Init(this.transform, imgDirBg, imgDirPoint, arrowRoot);
       
        
       
    //}

    

}
