using DG.Tweening;
using Mogoson.CameraExtension;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Y_UIFramework;

public class AutoFoam : MonoBehaviour
{
    public static AutoFoam Instance;
    public Camera camera;
    public float moveSpeed = 5f;
    public float angleSpeed = 45f;
    public List<int> speedList = new List<int>() {8, 4, 2, 1 };

    public bool isAutoFoam = false;//是否处于自动漫游

    Sequence mySequence;

    public bool IsPlaying;//是否播放；
    public int currentSpeedValue;//当前速度值
    public int currentSpeedIndex = 3;
    public List<Transform> points;//位置点
    private void Awake()
    {
        Instance = this;
        currentSpeedValue = 1;
    }

    [ContextMenu("Start")]
    // Start is called before the first frame update
    void Start()
    {
        GetPoints();
        CreateSequence();
        //CreateSequenceT();
    }

    private bool isStartTime;
    private float timeCount = 0;
    private void Update()
    {
        if (isStartTime)
        {
            if (timeCount <= 10f)
            {
                timeCount += Time.deltaTime;
            }
            else
            {
                isStartTime = false;
                timeCount = 0;
                CloseMonitorCamera();
                mySequence.Play();
            }

        }
    }


    [ContextMenu("GetPoints")]
    /// <summary>
    /// 获取点位
    /// </summary>
    public void GetPoints()
    {
        if (points == null)
        {
            points = new List<Transform>();
        }
        points.Clear();

        //GetPoints(transform, points);
        GetPoints_ByRender();
    }

    public void GetPoints_ByRender()
    {
        MeshRenderer[] meshRenderers = transform.GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer renderer in meshRenderers)
        {
            if (renderer == null) continue;
            if (renderer.gameObject.activeInHierarchy == false) return;
            points.Add(renderer.transform);

        }
    }

    //public void GetPoints(Transform p, List<Transform> pointsT)
    //{
    //    int childcount = p.childCount;
    //    if (childcount <= 0) return;
    //    for (int i = 0; i < childcount; i++)
    //    {
    //        Transform tran = p.GetChild(i);
    //        if (tran.gameObject.activeInHierarchy == false) return;
    //        if (tran.gameObject.tag == "FreeRoamPoint")
    //        {
    //            pointsT.Add(tran);
    //        }
    //        GetPoints(tran, pointsT);
    //    }

    //}

    /// <summary>
    /// Creating a Sequence
    /// </summary>
    [ContextMenu("ReCreateSequence")]
    public void CreateSequence()
    {
        if (mySequence != null)
        {
            mySequence.Kill();
            mySequence = null;
        }
        mySequence = DOTween.Sequence(); 

        for (int i = 0; i < points.Count; i++)
        {
            Transform p = points[i];

            AutoRoamPoint autoRoamPoint = p.gameObject.GetComponent<AutoRoamPoint>();
            float moveSpeedT = moveSpeed;
            float angleSpeedT = angleSpeed;
            if (autoRoamPoint != null&& autoRoamPoint.speedMultiple>1)
            {
                moveSpeedT = moveSpeed * autoRoamPoint.speedMultiple;
                angleSpeedT = angleSpeed * autoRoamPoint.speedMultiple;
            }

            float durationRotation;
            float durationMove;

            Vector3 targetPosT = new Vector3(p.position.x, p.position.y, p.position.z);

            if (i == 0)
            {
                durationMove = 0.1f;
                durationRotation = 0.1f;
            }
            else
            {
                durationMove = Vector3.Distance(targetPosT, points[i-1].transform.position) / moveSpeedT;
                //durationRotation = 0.8f;
                durationRotation = Vector3.Angle(p.forward, points[i - 1].forward) / angleSpeedT;
            }

            Tweener tweenerMove = camera.transform.DOMove(new Vector3(targetPosT.x, targetPosT.y + 1.7f, targetPosT.z), durationMove).SetEase(Ease.Linear);

            if (autoRoamPoint)
            {

                if (!string.IsNullOrEmpty(autoRoamPoint.txt))
                {
                    tweenerMove.OnComplete(() =>
                    {
                        Debug.Log("OnComplete！" + autoRoamPoint.name);
                        MessageCenter.SendMsg(MsgType.AutoRoamPanelMsg.TypeName, MsgType.AutoRoamPanelMsg.SetInfoTxt, autoRoamPoint.txt);
                        if (autoRoamPoint != null && autoRoamPoint.isLoadDevErQiZhuChangFan)
                        {
                            MessageCenter.SendMsg(MsgType.AutoRoamPanelMsg.TypeName, MsgType.AutoRoamPanelMsg.ShowBuildDevices, null);
                        }

                    });
                }
            }
            mySequence.Append(tweenerMove);

            Tweener tweenerR = camera.transform.DORotate(p.eulerAngles, durationRotation).SetEase(Ease.Linear);
            tweenerR.OnComplete(() =>
            {
                ////存在调取摄像头操作
                //if (autoRoamPoint != null && autoRoamPoint.CameraDevId > 0)
                //{
                //    Debug.Log("打开摄像头！");
                //    CallMonitorCamera(autoRoamPoint.CameraDevId);
                //    mySequence.Pause();
                //    isStartTime = true;
                //}

                    
            });
            mySequence.Append(tweenerR);

            //调摄像头前先停顿5秒
            if (autoRoamPoint != null && autoRoamPoint.CameraDevId > 0)
            {
                mySequence.AppendInterval(5f);
                mySequence.AppendCallback(() =>
                {
                    //存在调取摄像头操作
                    //if (autoRoamPoint != null && autoRoamPoint.CameraDevId > 0)
                    //{
                        Debug.Log("打开摄像头！");
                        CallMonitorCamera(autoRoamPoint.CameraDevId);
                        mySequence.Pause();
                        isStartTime = true;
                    //}
                });
            }

            ////存在调取摄像头关闭操作
            //if (autoRoamPoint != null && autoRoamPoint.CameraDevId > 0)
            //{
            //    Debug.Log("关闭摄像头方法！"+ autoRoamPoint.name);
            //    mySequence.AppendInterval(10F);
            //    mySequence.AppendCallback(() =>
            //    {
            //        NewMethod();
            //    });
            //}

        }

        mySequence.SetAutoKill(false);
        mySequence.Pause();
        //mySequence.OnPause(() => { Debug.Log("暂停了！"); }).OnPlay(() => { Debug.Log("暂停了！"); });
        mySequence.OnComplete(() =>
        {
            //UseMainCamera(false);
            IsPlaying = false;
        });
    }

    public void DoPaly()
    {
        if (!IsPlaying)
        {
            Paly();
        }
        else
        {
            Pause();
        }
    }


    [ContextMenu("Paly")]
    public void Paly()
    {
        //AroundAlignCamera a = camera.GetComponent<AroundAlignCamera>();
        //if (a != null)
        //{
        //    a.enabled = false;
        //}
        UseMainCamera(true);
        IsPlaying = true;
        mySequence.PlayForward();
    }

    [ContextMenu("Pause")]
    public void Pause()
    {
        IsPlaying = false;
        mySequence.Pause();
    }

    [ContextMenu("Restart")]
    public void Restart()
    {
        IsPlaying = true;
        mySequence.Restart();
    }

    /// <summary>
    /// 回到动画起点
    /// </summary>
    [ContextMenu("Rewind")]
    public void Rewind()
    {
        IsPlaying = false;
        mySequence.Rewind();
        ClearSpeed();
        UseMainCamera(false);
    }

    /// <summary>
    /// 漫游使用或不使用主摄像机
    /// </summary>
    public void UseMainCamera(bool b)
    {
        AroundAlignCamera a = camera.GetComponent<AroundAlignCamera>();
        if (a != null)
        {
            a.enabled = !b;
        }
    }
    /// <summary>
    /// Creating a Sequence
    /// </summary>
    //public void CreateSequenceT()
    //{
    //    mySequence = DOTween.Sequence();


    //    Tweener tweenerR = cube.transform.DORotate(new Vector3(0,180,0), 2f).OnComplete(() => { Debug.Log("DORotateOnComplete！"); });
    //    mySequence.Append(tweenerR);

    //    Tweener tweenerMove = cube.transform.DOMove(cube.transform.position+ new Vector3(5, 0, 0), 5).OnComplete(() => { Debug.Log("DOMoveOnComplete！"); });
    //    mySequence.Append(tweenerMove);


    //    mySequence.SetAutoKill(false);
    //    mySequence.Pause();
    //    mySequence.OnPause(() => { Debug.Log("暂停了！"); }).OnPlay(() => { Debug.Log("暂停了！"); });
    //}

    [ContextMenu("SetSequenceTimeScale2")]
    public void SetSequenceTimeScale2()
    {
        mySequence.timeScale = 2;
    }

    [ContextMenu("SetSequenceTimeScale4")]
    public void SetSequenceTimeScale4()
    {
        mySequence.timeScale = 4;
    }

    [ContextMenu("SetSequenceTimeScale8")]
    public void SetSequenceTimeScale8()
    {
        mySequence.timeScale = 8;
    }

    /// <summary>
    /// 设置加速
    /// </summary>
    public void SetSpeedupValue()
    {
        if (currentSpeedIndex == 0) return;
        currentSpeedIndex--;
        SelectSpeed(currentSpeedIndex);

        //if (currentSpeedIndex == speedList.Count - 1) return false;
        //currentSpeedIndex++;
        //if (currentSpeedIndex < speedList.Count)
        //{
        //    currentSpeedValue = speedList[currentSpeedIndex];
        //    mySequence.timeScale = currentSpeedValue;
        //    return true;
        //}
        //return false;
    }


    /// <summary>
    /// 设置减速
    /// </summary>
    public void SetSlowDownValue()
    {
        if (currentSpeedIndex > 3) return;
        currentSpeedIndex++;
        SelectSpeed(currentSpeedIndex);
        //if (currentSpeedIndex == 0) return false;
        //currentSpeedIndex--;
        //if (currentSpeedIndex >= 0)
        //{
        //    currentSpeedValue = speedList[currentSpeedIndex];
        //    mySequence.timeScale = currentSpeedValue;
        //    return true;
        //}

        //return false;

    }

    // private CameraMonitorFollowUI monitorFollowUI;

    /// <summary>
    /// 调取摄像头
    /// </summary>
    public void CallMonitorCamera(int CameraDevId)
    {
        // RoomFactory.Instance.GetDevByid(CameraDevId, (devnode) =>
        //  {
        //      devnode.Start();
        //      monitorFollowUI = devnode.FollowUI.GetComponentInChildren<CameraMonitorFollowUI>();
        //      //devnode.Start();
        //      monitorFollowUI.Show();
        //      monitorFollowUI.videoFollowUI.Max_ButClick();
        //      //devnode.Start();
        //      //FollowTargetManage.Instance.SetGroupUIbyName(devnode.ParentDepNode, true);
        //  });

        //RoomFactory.Instance.GetDevById(CameraDevId, (devnode) =>
        // {
        //     CameraMonitorFollowUI monitorFollowUI = devnode.FollowUI.GetComponentInChildren<CameraMonitorFollowUI>();
        //     monitorFollowUI.Show();
        //     monitorFollowUI.videoFollowUI.Max_ButClick();
        // });
    }

    public void CloseMonitorCamera()
    {
        // if (monitorFollowUI != null)
        // {
        //     Debug.Log("关闭摄像头！");
        //     //monitorFollowUI.videoFollowUI.Close();
        //     MessageCenter.SendMsg(MsgType.CameraVideoRtspPanelMsg.TypeName, MsgType.CameraVideoRtspPanelMsg.Close, null);
        //     monitorFollowUI.Hide();

        // }

    }

    public void ClearSpeed()
    {
        currentSpeedValue = 1;
        currentSpeedIndex = 3;
        mySequence.timeScale = 1;
    }
    /// <summary>
    /// 选择漫游速度
    /// </summary>
    /// <param name="value"></param>
    public void SelectSpeed(int value)
    {
        switch (value)
        {
            case 0:
                currentSpeedValue = 8;
                currentSpeedIndex = 0;
                break;
            case 1:
                currentSpeedValue = 4;
                currentSpeedIndex = 1;
                break;
            case 2:
                currentSpeedValue = 2;
                currentSpeedIndex = 2;
                break;
            case 3:
                currentSpeedValue = 1;
                currentSpeedIndex = 3;
                break;
        }
        mySequence.timeScale = currentSpeedValue;
    }
}
