using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;
/// <summary>
/// 判断是否点击在UI上
/// </summary>
public class IsClickUGUIorNGUI : MonoBehaviour
{
    public bool isClickUI = false;
    public bool isClickUGUI = false;
    public bool isClickedUI = false;//上一次点击是否点在UI上

    public static bool SisOverUI
    {
        get
        {
            if (Instance)
            {
                return Instance.IsOverUIMultiCanvas();
            }
            return false;
        }
    }

    public bool isOverUGUI = false;
    public bool isOverUI = false;

    public bool isCheck = true;//是否开启检测

    public static IsClickUGUIorNGUI Instance;


    //public  Canvas[] CanvasGroup;
    public List<Canvas> CanvasGruop;
    // Use this for initialization
    void Start()
    {
        Instance = this;

    }

    // Update is called once per frame
    void Update()
    {

        if (isCheck)
        {
            IsPointerOverUGUI();
            IsClickUIFunction();
        }

    }

    //private void LateUpdate()
    //{
    //    IsPointerOverUGUI();
    //}

    /// <summary>
    /// 是否开启检测
    /// //不关闭UI检测，会导致人员移动时，鼠标移动在UI上，场景出现异常
    /// </summary>
    /// <param name="b"></param>
    public void SetIsCheck(bool b)
    {
        isCheck = b;
        if (!isCheck)
        {
            Recover();
        }
    }

    /// <summary>
    /// 恢复
    /// </summary>
    public void Recover()
    {
        isClickUI = false;
        isClickUGUI = false;
        isClickedUI = false;
        isOverUGUI = false;
        isOverUI = false;
    }

    /// <summary>
    /// 检测鼠标点击
    /// </summary>
    public void IsClickUIFunction()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2))
        {
            //EventSystem.current.UpdateModules();
            //EventSystem.current.currentInputModule.UpdateModule();
            //EventSystem.current.currentInputModule.Process();
            //PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
            if (EventSystem.current.IsPointerOverGameObject())
            {
                if(InnoreItems.Count>0)
                {
                    isClickUGUI=HitItems.Count>0;
                    isClickedUI = isClickUGUI;
                }
                else{
                    isClickUGUI = true;
                    //Debug.Log("当前点击在UGUI上"); 
                    isClickedUI = true;
                }
            }
            else
            {
                //isClickUGUI = false;
                //Debug.Log("当前没有点击在UGUI上");
                isClickedUI = false;
            }
        }

        if (Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1) || Input.GetMouseButtonUp(2))
        {
            Invoke("SetisClickUIfalse", 0.05f);
            //isClickUGUI = false;
        }

        if (isClickUGUI)
        {
            isClickUI = true;

        }
        else
        {
            isClickUI = false;
        }
    }
    private string BigScreenCanvas = "Canvas_BigScreen";
    private bool IsOverUIMultiCanvas()
    {
        if (CanvasGruop == null) return true;
        Vector2 MousePos = Input.mousePosition;
        bool result = IsPointerOverUIObjectS(CanvasGruop, MousePos);
        //Debug.LogError("IsPointerOverUI : " + result);
        return result;
    }
    public bool IsPointerOverUIObjectS(List<Canvas> vas, Vector2 screenPosition)
    {
        //Debug.LogError("CanvasGroup Length : "+vas.Length);
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = screenPosition;
        int Result = 0;
        if (vas == null) return false;
        for (int i = 0; i < vas.Count; i++)
        {
            if (vas[i] == null) continue;
            if (!vas[i].gameObject.activeInHierarchy || vas[i].gameObject.name == BigScreenCanvas) continue;
            GraphicRaycaster uiRaycaster = vas[i].gameObject.GetComponent<GraphicRaycaster>();
            if (uiRaycaster != null)
            {
                List<RaycastResult> results = new List<RaycastResult>();
                uiRaycaster.Raycast(eventDataCurrentPosition, results);
                Result += results.Count;
            }
        }
        return Result > 0;
    }

    public static bool IsPointerOverUIObjectS(Canvas vas, Vector2 screenPosition)
    {
        //Debug.LogError("CanvasGroup Length : "+vas.Length);
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = screenPosition;
        int Result = 0;
        if (vas == null) return false;

        GraphicRaycaster uiRaycaster = vas.gameObject.GetComponent<GraphicRaycaster>();
        if (uiRaycaster != null)
        {
            List<RaycastResult> results = new List<RaycastResult>();
            uiRaycaster.Raycast(eventDataCurrentPosition, results);
            Result += results.Count;

        }
        return Result > 0;
    }

    public bool CheckRaycast=false;

    public List<GameObject> HitItems;

    public List<string> InnoreItems=new List<string>();

    public void IsPointerOverUGUI()
    {
        if (EventSystem.current && EventSystem.current.IsPointerOverGameObject())
        {
            //Debug.Log("鼠标当前触摸在UGUI上");
            isOverUGUI = true;
            //return true;

            if(CheckRaycast && InnoreItems.Count>0){
                PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
                pointerEventData.position = Input.mousePosition;

                HitItems= new List<GameObject>();

                List<RaycastResult> result = new List<RaycastResult>();
                EventSystem.current.RaycastAll(pointerEventData, result);
                foreach(var item in result){
                    if(InnoreItems.Contains(item.gameObject.name)){

                    }
                    else{
                        HitItems.Add(item.gameObject);
                    }
                    
                }

                isOverUGUI=HitItems.Count>0;
            }
        }
        else
        {
            isOverUGUI = false;
            //Debug.Log("鼠标当前没有触摸在UGUI上");
            //return false;
        }

        if (isOverUGUI)
        {
            isOverUI = true;
        }
        else
        {
            isOverUI = false;
        }
    }

    /// <summary>
    /// 外部调用
    /// </summary>
    public void ExternalIsClickUIFunction()
    {
        IsClickUIFunction();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="b"></param>
    public void SetisClickUIfalse()
    {
        isClickUGUI = false;
    }
}
