using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Y_UIFramework;

public class AutoRoamPanel : UIBasePanel
{
    public Button playBtn;//暂停按钮
    public Button playBtnChild;//暂停按钮子物体
    public Button restartBtn;//重播按钮
    public Button backBtn;//返回按钮
    public Button speedupBtn;//加速按钮
    public Button slowdownBtn;//减速按钮
    public Dropdown SpeedDropdown;//速度选择下拉框 

    public Text infoTxt;//文字描述Text
    public Text currentSpeedUpText;//文字描述Text

    // public BuildingController BuildingController;

    void Awake()
    {
        //窗体性质
        CurrentUIType.UIPanels_Type = UIPanelType.Normal;  //弹出窗体
        //CurrentUIType.UIPanel_LucencyType = UIPanelTransparentType.Pentrate;
        CurrentUIType.UIPanels_ShowMode = UIPanelShowMode.HideOther;

        /*  接受信息   */
        RegisterMsgListener(MsgType.AutoRoamPanelMsg.TypeName,
            obj =>
            {
                if (obj.Key == MsgType.AutoRoamPanelMsg.SetInfoTxt)
                {
                    SetInfoTxt(obj.Values.ToString());
                }
                else if (obj.Key == MsgType.AutoRoamPanelMsg.ShowBuildDevices)
                {
                    ShowBuildDevices();
                }
            }
       );
        SetCurrentSpeedUpText();
    }

    private void SetCurrentSpeedUpText()
    {
        currentSpeedUpText.text = AutoFoam.Instance.currentSpeedValue + "x";
    }

    // Start is called before the first frame update
    void Start()
    {
        playBtn.onClick.AddListener(PlayBtn_OnClick);
        playBtnChild.onClick.AddListener(PlayBtn_OnClick);
        restartBtn.onClick.AddListener(RestartBtn_OnClick);
        backBtn.onClick.AddListener(BackBtn_OnClick);
        speedupBtn.onClick.AddListener(SpeedupBtn_OnClick);
        slowdownBtn.onClick.AddListener(SlowdownBtn_OnClick);
        SpeedDropdown.onValueChanged.AddListener(AutoFoam.Instance.SelectSpeed);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void Show()
    {
        base.Show();
        AutoFoam.Instance.isAutoFoam = true;
        SetRoamFollowUI(true);
        //SetPlayTxt();
        SetCurrentSpeedUpText();
        //SetBuildDeviceActive("二期主厂房和集控楼", true);
        // LightManage.Instance.SetAutoFoamLight(true);
        // FactoryDepManager.Instance.BackToMainFactory(()=>
        // {
        //     SetBuildDeviceActive("二期主厂房和集控楼", true);
        // });
    }


    public override void Hide()
    {
        base.Hide();
        AutoFoam.Instance.isAutoFoam = false;
        SetRoamFollowUI(false);
        SetBuildDeviceActive("二期主厂房和集控楼", false);
        // LightManage.Instance.SetAutoFoamLight(false);


    }

    /// <summary>
    /// 暂停按钮触发
    /// </summary>
    public void PlayBtn_OnClick()
    {
        //AutoFoam.Instance.Paly();

        AutoFoam.Instance.DoPaly();
        ChangePlayBtn(AutoFoam.Instance.IsPlaying);
        //SetPlayTxt();
        Camera.main.farClipPlane = 450f;
    }

    /// <summary>
    /// 重播按钮触发
    /// </summary>
    public void RestartBtn_OnClick()
    {
        ChangePlayBtn(true);
        AutoFoam.Instance.Restart();
        SpeedDropdown.value = 3;
        //HideBuildDevices();
    }

    /// <summary>
    /// 返回按钮触发
    /// </summary>
    public void BackBtn_OnClick()
    {

        AutoFoam.Instance.Rewind();
        CloseUIPanel();
        // TrainSubBar.Instance.SetAutoRoamToggle(false);
        //AutoFoam.Instance.UseMainCamera(false);
        Camera.main.farClipPlane = 5000f;
    }

    /// <summary>
    /// 加速按钮触发
    /// </summary>
    public void SpeedupBtn_OnClick()
    {
        AutoFoam.Instance.SetSpeedupValue();
        SpeedDropdown.value = AutoFoam.Instance.currentSpeedIndex;
        //SetCurrentSpeedUpText();
    }

    /// <summary>
    /// 减速按钮触发
    /// </summary>
    public void SlowdownBtn_OnClick()
    {
        AutoFoam.Instance.SetSlowDownValue();
        SpeedDropdown.value = AutoFoam.Instance.currentSpeedIndex;
        //SetCurrentSpeedUpText();
    }

    /// <summary>
    /// 设置信息
    /// </summary>
    public void SetInfoTxt(string str)
    {
        infoTxt.text = str;
    }

    /// <summary>
    /// 设置自动漫游的漂浮UI
    /// </summary>
    /// <param name="isShow"></param>
    public void SetRoamFollowUI(bool isShow)
    {
        // if (isShow)
        // {
        //     if (FollowTargetManage.Instance) FollowTargetManage.Instance.ShowFollowCameraOnAutoRoam(true);
        // }
        // else
        // {
        //     if (FollowTargetManage.Instance) FollowTargetManage.Instance.ShowFollowCameraOnAutoRoam(false);
        // }
    }
    /// <summary>
    /// 设置漫游速度
    /// </summary>
    public void SetSpeed()
    {


    }

    /// <summary>
    /// 播放/暂停修改
    /// </summary>
    public void SetPlayTxt()
    {
        Text txt = playBtn.GetComponentInChildren<Text>();
        if (txt != null)
        {
            if (AutoFoam.Instance.IsPlaying)
            {
                txt.text = "暂停";
            }
            else
            {
                txt.text = "播放";
            }
        }
    }
    /// <summary>
    /// 播放暂停切换
    /// </summary>
    public void ChangePlayBtn(bool bo)
    {
        // if (!bo)
        // {
        //     playBtnChild.gameObject.GetComponent<Image>().color = new Color(0, 0, 0, 0);
        //     playBtnChild.gameObject.GetComponent<Image>().raycastTarget = false;
        //     playBtnChild.interactable = false;
        //     playBtn.GetComponent<ControlMenuToolTip>().TipContent = "播放";
        //     playBtn.gameObject.GetComponent<Image>().color = new Color(255, 255, 255, 255);
        //     playBtn.interactable = true;
        //     playBtn.gameObject.GetComponent<Image>().raycastTarget = true;
        // }
        // else
        // {
        //     playBtn.GetComponent<ControlMenuToolTip>().TipContent = "暂停";
        //     playBtn.gameObject.GetComponent<Image>().color = new Color(0, 0, 0, 0);
        //     playBtn.interactable = false;
        //     playBtn.gameObject.GetComponent<Image>().raycastTarget = false;
        //     playBtnChild.gameObject.GetComponent<Image>().raycastTarget = true;
        //     playBtnChild.gameObject.GetComponent<Image>().color = new Color(255, 255, 255, 255);
        //     playBtnChild.interactable = true;
        // }
    }
    public void OnDisable()
    {
        ChangePlayBtn(false);//按钮状态重置
        SpeedDropdown.value = 3;
    }


    private void SetBuildDeviceActive(string BuildDepName, bool isActive)
    {
        // BuildingController depNode = FactoryDepManager.Instance.FindBuildDepByName(BuildDepName);
        // BuildingController = depNode;
        // if (depNode)
        // {
        //     if (isActive)
        //     {
        //         StartCoroutine(depNode.inBuildingObjs.LoadInBuildingObjs(() =>
        //         {
        //             if (!depNode.IsDevCreate)
        //             {
        //                 depNode.IsDevCreate = true;
        //                 //RoomFactory.Instance.CreateDepDev(doorBuilding, true);//进入建筑，创建设备。
        //                 depNode.LoadDevices(() =>
        //                 {
        //                     RoomFactory.Instance.CreateDepDev(depNode, true, () =>
        //                     {
        //                         //加载完先关闭
        //                         //HideBuildDevices();
        //                         depNode.inBuildingObjs.SetActive_InBuildingObjs(true);
        //                         depNode.ShowBuildingDev(true);
        //                     });

        //                 });
        //             }
        //             else
        //             {
        //                 //depNode.inBuildingObjs.SetActive_InBuildingObjs(false);
        //                 //depNode.ShowBuildingDev(false);
        //                 depNode.inBuildingObjs.SetActive_InBuildingObjs(true);
        //                 depNode.ShowBuildingDev(true);
        //             }
        //         }));
        //     }
        //     else
        //     {
        //         depNode.inBuildingObjs.SetActive_InBuildingObjs(false);
        //         depNode.ShowBuildingDev(false);
        //     }
        // }

    }

    /// <summary>
    /// 显示二期主厂房建筑设备
    /// </summary>
    public void ShowBuildDevices()
    {
        // //SetBuildDeviceActive("二期主厂房和集控楼", true);
        // if (BuildingController)
        // {
        //     BuildingController.inBuildingObjs.SetActive_InBuildingObjs(true);
        //     BuildingController.ShowBuildingDev(true);
        // }
    }

    /// <summary>
    /// 隐藏二期主厂房建筑设备
    /// </summary>
    public void HideBuildDevices()
    {
        SetBuildDeviceActive("二期主厂房和集控楼", false);
    }
}
