using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRoamPoint : MonoBehaviour
{
    public string txt;
    //// Start is called before the first frame update
    //void Start()
    //{

    //}

    //// Update is called once per frame
    //void Update()
    //{

    //}

    public int CameraDevId;

    /// <summary>
    /// //打开加载后的二期主厂房设备
    /// </summary>
    public bool isLoadDevErQiZhuChangFan;

    /// <summary>
    /// 设置到该点位置速度是现有速度的倍速
    /// </summary>
    public float speedMultiple = 1;
}
