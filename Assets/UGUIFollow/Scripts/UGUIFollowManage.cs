using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Y_UIFramework;

/// <summary>
/// UGUI屏幕跟随3D物体，实际跟随，管理
/// </summary>
public class UGUIFollowManage : MonoBehaviour
{

    public static UGUIFollowManage Instance;

    /// <summary>
    /// 所有3D跟随标志的列表，每个parent为一组
    /// </summary>
    private Dictionary<string, GameObject> name_uiparent = new Dictionary<string, GameObject>();
    /// <summary>
    /// 所有3D跟随标志的列表
    /// </summary>
    private Dictionary<string, List<GameObject>> name_uilist = new Dictionary<string, List<GameObject>>();

    /// <summary>
    /// 容器
    /// </summary>
    public Transform content;
    /// <summary>
    /// 控制显示隐藏
    /// </summary>
    private CanvasGroup canvasGroup;
    /// <summary>
    /// 公共的跟随UI容器
    /// </summary>
    public Transform CommonFollowUIs;

    /// <summary>
    /// 脚本所属Canvas
    /// </summary>
    public Canvas FollowCanvas;

    private void Awake()
    {
        Instance = this;
    }

    // Use this for initialization
    void Start()
    {
        if (CommonFollowUIs == null)
        {
            CreateFollowUIs();
        }
        //name_uiparent = new Dictionary<string, GameObject>();
        //name_uilist = new Dictionary<string, List<GameObject>>();
    }

    // Update is called once per frame
    void Update()
    {
        Sort();
    }

    /// <summary>
    /// 创建一组跟随标志
    /// </summary>
    public void Create3DFollowGroup()
    {

    }

    /// <summary>
    /// 创建一项
    /// </summary>
    public GameObject CreateItem(GameObject prefabUI, GameObject target, Camera camT, string parentName, bool IsRayCheckCollision = false)
    {
        UGUIFollowTarget followT = GetUIbyTarget(parentName, target);
        if (followT)
        {
            followT.gameObject.SetActive(true);
            return followT.gameObject;
        }
        //offset = offset + new Vector3(0, target.GetGlobalSize().y / 2, 0);
        GameObject ui = Instantiate(prefabUI);
        ui.SetActive(true);

        //ui.transform.localPosition = target.transform.position;
        //ui.transform.localScale = Vector3.one;
        //UGUI_3DFollow ugui_3DFollow = ui.AddMissingComponent<UGUI_3DFollow>();
        //ugui_3DFollow.Init(target, camT, offset);
        //GameObject targetTagObj = UGUIFollowTarget.CreateTitleTag(target, offset);
        UGUIFollowTarget followTarget = UGUIFollowTarget.AddUGUIFollowTarget(ui, target, camT, false);
        followTarget.IsRayCheckCollision = IsRayCheckCollision;

        if (name_uiparent.ContainsKey(parentName))
        {
            ui.transform.SetParent(name_uiparent[parentName].transform);
        }
        else
        {
            CreateParent(parentName);
            ui.transform.SetParent(name_uiparent[parentName].transform);
        }

        ui.transform.localScale = Vector3.one;

        if (name_uilist.ContainsKey(parentName))
        {
            name_uilist[parentName].Add(ui);
        }
        else
        {
            CreateParent(parentName);
            name_uilist[parentName].Add(ui);
        }
        return ui;
    }

    /// <summary>
    /// 创建一组
    /// </summary>
    public void CreateFlags(GameObject prefabUI, List<GameObject> objs, string groupname, Camera camT = null)
    {
        if (camT == null)
        {
            camT = GetCamera();
        }
        foreach (GameObject o in objs)
        {
            Transform titleTag = o.transform.Find("TitleTag");
            if (titleTag != null)
            {
                CreateItem(prefabUI, titleTag.gameObject, groupname, camT);
            }
            else
            {
                CreateItem(prefabUI, o, groupname, camT);
            }
        }
    }

    public GameObject CreateItemEX(GameObject prefabUI, GameObject target, string groupName,bool changeChildActive,int layerint = -1)
    {
        return CreateItem(prefabUI, target, groupName,null, changeChildActive, false,false,true, layerint);
    }

    /// <summary>
    /// 创建一项
    /// </summary>
    /// isCreateParent:是否创建父物体
    public GameObject CreateItem(GameObject prefabUI, GameObject target, string groupName, Camera camT = null,bool changeChildActive=false ,bool isCreateParent = false, bool IsRayCheckCollision = false, bool isUseCanvasScalerT = true, int layerint = -1)
    {
        if (camT == null)
        {
            camT = GetCamera();
        }
        else
        {

        }
        UGUIFollowTarget followT = GetUIbyTarget(groupName, target);
        if (followT)
        {
            followT.gameObject.SetActive(true);
            return followT.gameObject;
        }
        GameObject ui = Instantiate(prefabUI);
        ui.SetActive(true);

        UGUIFollowTarget followTarget = UGUIFollowTarget.AddUGUIFollowTarget(ui, target, camT, isUseCanvasScalerT, layerint);
        followTarget.SetTargetChildActive = changeChildActive;
        //followTarget.IsRayCheckCollision = IsRayCheckCollision;
        followTarget.SetIsRayCheckCollision(IsRayCheckCollision);

        if (isCreateParent)
        {
            if (name_uiparent.ContainsKey(groupName))
            {
                ui.transform.SetParent(name_uiparent[groupName].transform);
            }
            else
            {
                CreateParent(groupName);
                ui.transform.SetParent(name_uiparent[groupName].transform);
            }
        }
        else
        {
            ui.transform.SetParent(CommonFollowUIs);
        }

        ui.transform.localScale = Vector3.one;
        ui.transform.localEulerAngles = Vector3.zero;

        if (name_uilist.ContainsKey(groupName))
        {
            name_uilist[groupName].Add(ui);
        }
        else
        {
            name_uilist.Add(groupName, new List<GameObject>());
            name_uilist[groupName].Add(ui);
        }


        return ui;
    }

    private Camera GetCamera()
    {
        Camera camT;
        //Canvas canvas = transform.GetComponentInParent<Canvas>();
        Canvas canvas = FollowCanvas;
        //if (canvas.renderMode == RenderMode.ScreenSpaceOverlay)
        //{
        //    camT = Camera.main;
        //}
        //else
        //{
        //    camT = canvas.worldCamera;
        //}

        camT = Camera.main;

        return camT;
    }

    /// <summary>
    /// 创建父物体
    /// </summary>
    public void CreateParent(string name)
    {
        GameObject parentT = new GameObject(name);
        parentT.transform.SetParent(content);
        parentT.gameObject.AddComponent<RectTransform>();
        parentT.transform.localPosition = Vector3.zero;
        parentT.transform.localScale = Vector3.one;
        parentT.transform.localEulerAngles = Vector3.zero;
        name_uilist.Add(name, new List<GameObject>());
        name_uiparent.Add(name, parentT);
        //return dics[name];
    }

    /// <summary>
    /// 创建父物体
    /// </summary>
    public void CreateFollowUIs()
    {
        CommonFollowUIs = new GameObject("CommonFollowUIs").transform;
        CommonFollowUIs.gameObject.AddComponent<RectTransform>();
        CommonFollowUIs.transform.SetParent(content);
        CommonFollowUIs.transform.localPosition = Vector3.zero;
        CommonFollowUIs.transform.eulerAngles = Vector3.zero;
        CommonFollowUIs.transform.localScale = Vector3.one;
        //name_uilist.Add("FollowUICommon", new List<GameObject>());
        //name_uiparent.Add(name, parentT);
        //return dics[name];
        //CommonFollowUIs.gameObject.SetActive(true);
        SetCommonFollowUIsActive(true);
    }

    /// <summary>
    /// 设置跟随UI,公共池的显示隐藏
    /// </summary>
    public void SetCommonFollowUIsActive(bool isBool)
    {
        CommonFollowUIs.gameObject.SetActive(isBool);
    }

    /// <summary>
    /// 设置跟随UI的显示或隐藏通过组名称
    /// </summary>
    public void SetGroupUIbyName(string groupname, bool b)
    {
        if (name_uiparent.ContainsKey(groupname))
        {
            name_uiparent[groupname].SetActive(b);
        }
        else
        {
            if (name_uilist.ContainsKey(groupname))
            {
                foreach (GameObject o in name_uilist[groupname])
                {
                    if (o == null) continue;
                    o.SetActive(b);
                }                
            }
        }
    }
    /// <summary>
    /// 根据组名称，获取组物体
    /// </summary>
    /// <param name="groupName"></param>
    /// <returns></returns>
    public GameObject GetGroupByName(string groupName)
    {
        if (name_uiparent.ContainsKey(groupName))
        {
            return name_uiparent[groupName];
        }
        else
        {
            return null;
        }
    }
    /// <summary>
    /// 移除跟随UI的集合，通过名称
    /// </summary>
    public void RemoveGroupUIbyName(string groupname)
    {
        if (name_uiparent.ContainsKey(groupname))
        {
            DestroyImmediate(name_uiparent[groupname].gameObject);
            name_uiparent.Remove(groupname);
            name_uilist.Remove(groupname);
        }
        else
        {
            if (name_uilist.ContainsKey(groupname))
            {
                foreach (GameObject o in name_uilist[groupname])
                {
                    DestroyImmediate(o);
                }
                name_uilist.Remove(groupname);
            }
        }
    }
    /// <summary>
    /// 通过UI标志，移除目标物体
    /// </summary>
    /// <param name="groupName"></param>
    /// <param name="target"></param>
    public void RemoveUIbyTarget(string groupName,GameObject target)
    {
        if (name_uilist.ContainsKey(groupName))
        {
            List<GameObject> uis = name_uilist[groupName];
            GameObject targetFollow = null;
            foreach (GameObject ui in uis)
            {
                if (ui == null) continue;
                UGUIFollowTarget follow = ui.GetComponent<UGUIFollowTarget>();
                if (follow.Target == target)
                {
                    targetFollow = ui;
                    break;
                }
            }
            if (targetFollow != null)
            {
                uis.Remove(targetFollow);
                DestroyImmediate(targetFollow);
            }
        }
    }
    /// <summary>
    /// 获取UI标志通过目标物体
    /// </summary>
    public UGUIFollowTarget GetUIbyTarget(string gruopname, GameObject target)
    {
        if (name_uilist.ContainsKey(gruopname))
        {
            List<GameObject> uis = name_uilist[gruopname];
            foreach (GameObject ui in uis)
            {
                if (ui == null) continue;
                UGUIFollowTarget follow = ui.GetComponent<UGUIFollowTarget>();
                if (follow.Target == target)
                {
                    return follow;
                }
            }
        }
        return null;
    }

    /// <summary>
    /// 设置UI的名称通过组名称和目标物体
    /// </summary>
    public void SetUIbyTarget(string groupname, GameObject target, bool isActive)
    {
        UGUIFollowTarget ui = GetUIbyTarget(groupname, target);
        if (ui != null)
        {
            ui.gameObject.SetActive(isActive);
        }
    }


    /// <summary>
    /// 是否有某组跟随UI，根据组名称项
    /// </summary>
    public bool IsGroupUIbyName(string name)
    {
        if (name_uilist.ContainsKey(name))
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// 排序某组UI
    /// </summary>
    /// <param name="groupname"></param>
    public void Sort(string groupname)
    {
        if (name_uiparent.ContainsKey(groupname))
        {
            UGUIFollowTarget[] uis = name_uiparent[groupname].GetComponentsInChildren<UGUIFollowTarget>();
            List<UGUIFollowTarget> uiList = new List<UGUIFollowTarget>(uis);
            uiList.Sort((left, right) =>
            {
                if (left.DisCamera >= right.DisCamera)
                    return 1;
                else
                    return -1;
            });

            for (int i = 0; i < uiList.Count; i++)
            {
                if (!uiList[i].gameObject.activeInHierarchy) continue;
                uiList[i].transform.SetAsFirstSibling();
            }
            //print("Sort");
        }
    }

    /// <summary>
    /// 排序CommonFollowUIs
    /// </summary>
    /// <param name="groupname"></param>
    public void Sort()
    {
        //UGUIFollowTarget[] uis = CommonFollowUIs.GetComponentsInChildren<UGUIFollowTarget>();
        UGUIFollowTarget[] uis = transform.GetComponentsInChildren<UGUIFollowTarget>();//获取所有的跟随UI，进行排序
        List<UGUIFollowTarget> uiList = new List<UGUIFollowTarget>(uis);
        List<UGUIFollowTarget> normalList = new List<UGUIFollowTarget>();
        List<UGUIFollowTarget> upList = new List<UGUIFollowTarget>();

        for (int i = 0; i < uiList.Count; i++)
        {
            if (uiList[i].isup)
            {
                upList.Add(uiList[i]);
            }
            else
            {
                normalList.Add(uiList[i]);
            }
        }

        upList.Sort((left, right) =>
        {
            if (left.DisCamera >= right.DisCamera)
                return 1;
            else
                return -1;
        });

        normalList.Sort((left, right) =>
        {
            if (left.DisCamera >= right.DisCamera)
                return 1;
            else
                return -1;
        });

        for (int i = 0; i < upList.Count; i++)
        {
            if (!upList[i].gameObject.activeInHierarchy) continue;
            upList[i].transform.SetAsFirstSibling();
        }

        for (int i = 0; i < normalList.Count; i++)
        {
            if (!normalList[i].gameObject.activeInHierarchy) continue;
            normalList[i].transform.SetAsFirstSibling();
        }
        //print("Sort");
    }
}
