/***
 * 
 *    Title: "Y_UIFramework" UI框架项目
 *           主题： UI管理器  
 *    Description: 
 *           功能： 是整个UI框架的核心，用户程序通过本脚本，来实现框架绝大多数的功能实现。
 *                  
 *    Date: 
 *    Version: 0.1版本
 *    Modify Recoder: 
 *    
 * 
 *    软件开发原则：
 *    1： “高内聚，低耦合”。
 *    2： 方法的“单一职责”
 *     
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Y_UIFramework
{
	public class UIManager : MonoBehaviour {
        /* 字段 */
	    private static UIManager _Instance = null;
        //UI窗体预设路径(参数1：窗体预设名称，2：表示窗体预设路径)
	    private Dictionary<string, string> _DicPanelsPaths; 
        //缓存所有UI窗体
	    private Dictionary<string, UIBasePanel> _DicALLUIPanels;
        //当前显示的UI窗体
	    private Dictionary<string, UIBasePanel> _DicCurrentShowUIPanels;
        //定义“栈”集合,存储显示当前所有[反向切换]的窗体类型
        private Stack<UIBasePanel> _StackCurrentUIPanels;  
        //UI根节点
	    private Transform _TranCanvas = null;
        //全屏幕显示的节点
        private Transform _TranNormal = null;
        //固定显示的节点
	    private Transform _TranFixed = null;
        //弹出节点
	    private Transform _TranPopUp = null;
        //UI管理脚本的节点
	    private Transform _TranUIScripts = null;
        ////UI摄像机
        private Camera _UICamera;

        public Camera UICamera { get => _UICamera; }
        
        public Transform RootCanvas { get => _TranCanvas; }
        /// <summary>
        /// 得到实例
        /// </summary>
        /// <returns></returns>
        public static UIManager GetInstance()
	    {
	        if (_Instance==null)
	        {
	            _Instance = new GameObject("_UIManager").AddComponent<UIManager>();
	        }
	        return _Instance;
	    }

        //初始化核心数据，加载“UI窗体路径”到集合中。
	    public void Awake()
	    {
	        //字段初始化
            _DicALLUIPanels=new Dictionary<string, UIBasePanel>();
            _DicCurrentShowUIPanels=new Dictionary<string, UIBasePanel>();
            _DicPanelsPaths=new Dictionary<string, string>();
            _StackCurrentUIPanels = new Stack<UIBasePanel>();
            //初始化加载（根UI窗体）Canvas预设
	        InitRootCanvasLoading();
	        //得到UI根节点、全屏节点、固定节点、弹出节点
            _TranCanvas = GameObject.FindGameObjectWithTag(BasicDefine.SYS_TAG_CANVAS).transform;
	        _TranNormal = UnityHelper.FindTheChildNode(_TranCanvas.gameObject, BasicDefine.SYS_NORMAL_NODE);
            _TranFixed = UnityHelper.FindTheChildNode(_TranCanvas.gameObject, BasicDefine.SYS_FIXED_NODE);
            _TranPopUp = UnityHelper.FindTheChildNode(_TranCanvas.gameObject, BasicDefine.SYS_POPUP_NODE);
            _TranUIScripts = UnityHelper.FindTheChildNode(_TranCanvas.gameObject,BasicDefine.SYS_SCRIPTMANAGER_NODE);

            //把本脚本作为“根UI窗体”的子节点。
            this.gameObject.transform.SetParent(_TranUIScripts, false);
	        //"根UI窗体"在场景转换的时候，不允许销毁
            DontDestroyOnLoad(_TranCanvas);
	        //初始化“UI窗体预设”路径数据
	        InitUIPanelsPathData();
	    }

        /// <summary>
        /// 显示（打开）UI窗体
        /// 功能：
        /// 1: 根据UI窗体的名称，加载到“所有UI窗体”缓存集合中
        /// 2: 根据不同的UI窗体的“显示模式”，分别作不同的加载处理
        /// </summary>
        /// <param name="uiPanelName">UI窗体预设的名称</param>
	    public void ShowUIPanel(string uiPanelName)
        {
            Debug.Log("ShowUIPanel:"+uiPanelName);
            UIBasePanel basePanel = null;                    //UI窗体基类

            //参数的检查
            if (string.IsNullOrEmpty(uiPanelName)) return;

            //根据UI窗体的名称，加载到“所有UI窗体”缓存集合中
            basePanel = LoadPanelsToAllUIPanelsCatch(uiPanelName);
            if (basePanel == null) return;

            //是否清空“栈集合”中得数据
            if (basePanel.CurrentUIType.IsClearStack)
            {
                ClearStackArray();
            }

            // Debug.Log("UIPanels_ShowMode:"+basePanel.CurrentUIType.UIPanels_ShowMode);
            //根据不同的UI窗体的显示模式，分别作不同的加载处理
            switch (basePanel.CurrentUIType.UIPanels_ShowMode)
            {                    
                case UIPanelShowMode.Normal:                 //“普通显示”窗口模式
                    //把当前窗体加载到“当前窗体”集合中。
                    LoadUIToCurrentCache(uiPanelName);
                    break;
                case UIPanelShowMode.ReverseChange:          //需要“反向切换”窗口模式
                    PushUIPanelToStack(uiPanelName);
                    break;
                case UIPanelShowMode.HideOther:              //“隐藏其他”窗口模式
                    EnterUIPanelsAndHideOther(uiPanelName);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 关闭（返回上一个）窗体
        /// </summary>
        /// <param name="uiFormName"></param>
        public void CloseUIPanels(string uiFormName, bool isTweenBack = false)
        {
            UIBasePanel basePanel;                          //窗体基类

            //参数检查
            if (string.IsNullOrEmpty(uiFormName)) return;
            //“所有UI窗体”集合中，如果没有记录，则直接返回
            _DicALLUIPanels.TryGetValue(uiFormName, out basePanel);
            if (basePanel == null) return;
            //根据窗体不同的显示类型，分别作不同的关闭处理
            switch (basePanel.CurrentUIType.UIPanels_ShowMode)
            {
                case UIPanelShowMode.Normal:
                    //普通窗体的关闭
                    ExitUIPanels(uiFormName, isTweenBack);
                    break;
                case UIPanelShowMode.ReverseChange:
                    //反向切换窗体的关闭
                    PopUIPanels(isTweenBack);
                    break;
                case UIPanelShowMode.HideOther:
                    //隐藏其他窗体关闭
                    ExitUIPanelsAndOpenOther(uiFormName, isTweenBack);
                    break;

                default:
                    break;
            }
        }

        #region  显示“UI管理器”内部核心数据，测试使用
        
        /// <summary>
        /// 显示"所有UI窗体"集合的数量
        /// </summary>
        /// <returns></returns>
        public int ShowALLUIPanelCount()
        {
            if (_DicALLUIPanels != null)
            {
                return _DicALLUIPanels.Count;
            }
            else {
                return 0;
            }   
        }

        /// <summary>
        /// 显示"当前窗体"集合中数量
        /// </summary>
        /// <returns></returns>
        public int ShowCurrentUIPanelsCount()
        {
            if (_DicCurrentShowUIPanels != null)
            {
                return _DicCurrentShowUIPanels.Count;
            }
            else
            {
                return 0;
            }           
        }

        /// <summary>
        /// 显示“当前栈”集合中窗体数量
        /// </summary>
        /// <returns></returns>
        public int ShowCurrentStackUIPanelsCount()
        {
            if (_StackCurrentUIPanels != null)
            {
                return _StackCurrentUIPanels.Count;
            }
            else
            {
                return 0;
            }           
        }

        #endregion

        #region 私有方法
        //初始化加载（根UI窗体）Canvas预设
	    private void InitRootCanvasLoading()
	    {
            GameObject uiCameraObj = ResourcesMgr.GetInstance().LoadAsset(BasicDefine.SYS_PATH_UICAMERA, false);
            DontDestroyOnLoad(uiCameraObj);

            GameObject CanvasObj = ResourcesMgr.GetInstance().LoadAsset(BasicDefine.SYS_PATH_CANVAS, false);
            Canvas canvas = CanvasObj.GetComponent<Canvas>();
            _UICamera= uiCameraObj.GetComponent<Camera>();
            canvas.worldCamera = _UICamera;

            //AllFollowPanel
            GameObject ui_AllFollowPanel = ResourcesMgr.GetInstance().LoadAsset(BasicDefine.SYS_PATH_ALLFOLLOWPANEL, false);
            ui_AllFollowPanel.transform.SetParent(CanvasObj.transform);
            ui_AllFollowPanel.transform.localPosition = Vector3.zero;
            ui_AllFollowPanel.transform.localEulerAngles = Vector3.zero;
            RectTransform rectFollow = ui_AllFollowPanel.GetComponent<RectTransform>();
            rectFollow.offsetMax = Vector2.zero;
            rectFollow.offsetMin = Vector2.zero;
            rectFollow.localScale = Vector3.one;
            ui_AllFollowPanel.transform.SetAsFirstSibling();//放在Canvas下第一个位置
            UGUIFollowManage uGUIFollow = ui_AllFollowPanel.GetComponent<UGUIFollowManage>();
            uGUIFollow.FollowCanvas = canvas;

            Transform TopUI = UnityHelper.FindTheChildNode(CanvasObj, BasicDefine.SYS_TOPUI_NODE);

            //MessageBox
            GameObject ui_MessageBox = ResourcesMgr.GetInstance().LoadAsset(BasicDefine.SYS_PATH_MESSAGEBOX, false);
            ui_MessageBox.transform.SetParent(TopUI.transform);
            ui_MessageBox.transform.localPosition = Vector3.zero;
            ui_MessageBox.transform.localEulerAngles = Vector3.zero;
            RectTransform rectBox = ui_MessageBox.GetComponent<RectTransform>();
            rectBox.offsetMax = Vector2.zero;
            rectBox.offsetMin = Vector2.zero;
            rectBox.localScale = Vector3.one;

            //Tooltip
            GameObject ui_Tooltip = ResourcesMgr.GetInstance().LoadAsset(BasicDefine.SYS_PATH_UGUITOOLTIP, false);
            ui_Tooltip.transform.SetParent(TopUI.transform);
            ui_Tooltip.transform.localPosition = Vector3.zero;
            ui_Tooltip.transform.localEulerAngles = Vector3.zero;
            RectTransform rectTooltip = ui_Tooltip.GetComponent<RectTransform>();
            rectTooltip.offsetMax = Vector2.zero;
            rectTooltip.offsetMin = Vector2.zero;
            rectTooltip.localScale = Vector3.one;

            //ProgressBar
            GameObject ui_ProgressBar = ResourcesMgr.GetInstance().LoadAsset(BasicDefine.SYS_PATH_PROGRESSBAR, false);
            ui_ProgressBar.transform.SetParent(TopUI.transform);
            ui_ProgressBar.transform.localPosition = Vector3.zero;
            ui_ProgressBar.transform.localEulerAngles = Vector3.zero;
            RectTransform rectProgressBar = ui_ProgressBar.GetComponent<RectTransform>();
            rectProgressBar.offsetMax = Vector2.zero;
            rectProgressBar.offsetMin = Vector2.zero;
            rectProgressBar.localScale = Vector3.one;

            //ProgressBarBottom
            GameObject ui_ProgressBarBottom = ResourcesMgr.GetInstance().LoadAsset(BasicDefine.SYS_PATH_PROGRESSBARBOTTOM, false);
            ui_ProgressBarBottom.transform.SetParent(TopUI.transform);
            ui_ProgressBarBottom.transform.localPosition = Vector3.zero;
            ui_ProgressBarBottom.transform.localEulerAngles = Vector3.zero;
            RectTransform rectProgressBarBottom = ui_ProgressBarBottom.GetComponent<RectTransform>();
            rectProgressBarBottom.offsetMax = Vector2.zero;
            rectProgressBarBottom.offsetMin = Vector2.zero;
            rectProgressBarBottom.localScale = Vector3.one;

            //fps
            GameObject ui_fps = ResourcesMgr.GetInstance().LoadAsset(BasicDefine.SYS_PATH_FPS, false);
            ui_fps.transform.SetParent(TopUI.transform);//PopUp
            ui_fps.transform.localPosition = Vector3.zero;
            ui_fps.transform.localEulerAngles = Vector3.zero;
            RectTransform rectFPS = ui_fps.GetComponent<RectTransform>();
            rectFPS.offsetMax = Vector2.zero;
            rectFPS.offsetMin = Vector2.zero;
            rectFPS.localScale = Vector3.one;
        }


        /// <summary>
        /// 根据UI窗体的名称，加载到“所有UI窗体”缓存集合中
        /// 功能： 检查“所有UI窗体”集合中，是否已经加载过，否则才加载。
        /// </summary>
        /// <param name="uiPanelsName">UI窗体（预设）的名称</param>
        /// <returns></returns>
	    private UIBasePanel LoadPanelsToAllUIPanelsCatch(string uiPanelsName)
	    {
	        UIBasePanel baseUIResult = null;                 //加载的返回UI窗体基类

	        _DicALLUIPanels.TryGetValue(uiPanelsName, out baseUIResult);
            if (baseUIResult==null)
	        {
	            //加载指定名称的“UI窗体”
                baseUIResult = LoadUIPanel(uiPanelsName);
	        }

	        return baseUIResult;
	    }

        /// <summary>
        /// 加载指定名称的“UI窗体”
        /// 功能：
        ///    1：根据“UI窗体名称”，加载预设克隆体。
        ///    2：根据不同预设克隆体中带的脚本中不同的“位置信息”，加载到“根窗体”下不同的节点。
        ///    3：隐藏刚创建的UI克隆体。
        ///    4：把克隆体，加入到“所有UI窗体”（缓存）集合中。
        /// 
        /// </summary>
        /// <param name="uiPanelName">UI窗体名称</param>
	    private UIBasePanel LoadUIPanel(string uiPanelName)
        {
            string strUIPanelPaths = null;                   //UI窗体路径
            GameObject goCloneUIPrefabs = null;             //创建的UI克隆体预设
            UIBasePanel basePanel = null;                     //窗体基类


            //根据UI窗体名称，得到对应的加载路径
            _DicPanelsPaths.TryGetValue(uiPanelName, out strUIPanelPaths);
            //根据“UI窗体名称”，加载“预设克隆体”
            if (!string.IsNullOrEmpty(strUIPanelPaths))
            {
                goCloneUIPrefabs = ResourcesMgr.GetInstance().LoadAsset(strUIPanelPaths, false);
            }
            //设置“UI克隆体”的父节点（根据克隆体中带的脚本中不同的“位置信息”）
            if (_TranCanvas != null && goCloneUIPrefabs != null)
            {
                basePanel = goCloneUIPrefabs.GetComponent<UIBasePanel>();
                if (basePanel == null)
                {
                    Debug.Log("baseUiForm==null! ,请先确认窗体预设对象上是否加载了baseUIForm的子类脚本！ 参数 uiFormName=" + uiPanelName);
                    return null;
                }
                switch (basePanel.CurrentUIType.UIPanels_Type)
                {
                    case UIPanelType.Normal:                 //普通窗体节点
                        goCloneUIPrefabs.transform.SetParent(_TranNormal, false);
                        break;
                    case UIPanelType.Fixed:                  //固定窗体节点
                        goCloneUIPrefabs.transform.SetParent(_TranFixed, false);
                        break;
                    case UIPanelType.PopUp:                  //弹出窗体节点
                        goCloneUIPrefabs.transform.SetParent(_TranPopUp, false);
                        break;
                    default:
                        break;
                }

                //设置隐藏
                goCloneUIPrefabs.SetActive(true);
                //把克隆体，加入到“所有UI窗体”（缓存）集合中。
                _DicALLUIPanels.Add(uiPanelName, basePanel);
                return basePanel;
            }
            else
            {
                Debug.Log("_TraCanvasTransfrom==null Or goCloneUIPrefabs==null!! ,Plese Check!, 参数uiFormName="+uiPanelName); 
            }

            Debug.Log("出现不可以预估的错误，请检查，参数 uiFormName="+uiPanelName);
            return null;
        }//Mehtod_end

        /// <summary>
        /// 把当前窗体加载到“当前窗体”集合中
        /// </summary>
        /// <param name="uiPanelName">窗体预设的名称</param>
	    private void LoadUIToCurrentCache(string uiPanelName)
	    {
	        UIBasePanel basePanel;                          //UI窗体基类
	        UIBasePanel baseUIPanelsAllCache;              //从“所有窗体集合”中得到的窗体

	        //如果“正在显示”的集合中，存在整个UI窗体，则直接返回
	        _DicCurrentShowUIPanels.TryGetValue(uiPanelName, out basePanel);
	        if (basePanel != null) return;
	        //把当前窗体，加载到“正在显示”集合中
	        _DicALLUIPanels.TryGetValue(uiPanelName, out baseUIPanelsAllCache);
            if (baseUIPanelsAllCache!=null)
	        {
                _DicCurrentShowUIPanels.Add(uiPanelName, baseUIPanelsAllCache);
                baseUIPanelsAllCache.Show();           //显示当前窗体
            }
	    }
 
        /// <summary>
        /// UI窗体入栈
        /// </summary>
        /// <param name="uiPanelName">窗体的名称</param>
        private void PushUIPanelToStack(string uiPanelName)
        { 
            UIBasePanel baseUIPanel;                          //UI窗体

            //判断“栈”集合中，是否有其他的窗体，有则“冻结”处理。
            if(_StackCurrentUIPanels.Count>0)
            {
                UIBasePanel topPanel = _StackCurrentUIPanels.Peek();
                //栈顶元素作冻结处理
                topPanel.Pause();
            }
            //判断“UI所有窗体”集合是否有指定的UI窗体，有则处理。
            _DicALLUIPanels.TryGetValue(uiPanelName, out baseUIPanel);
            if (baseUIPanel!=null)
            {
                //当前窗口显示状态
                baseUIPanel.Show();
                //把指定的UI窗体，入栈操作。
                _StackCurrentUIPanels.Push(baseUIPanel);
            }
            else{
                Debug.Log("baseUIForm==null,Please Check, 参数 uiFormName=" + uiPanelName);
            }
        }

        /// <summary>
        /// 退出指定UI窗体
        /// </summary>
        /// <param name="strUIPanelName"></param>
        private void ExitUIPanels(string strUIPanelName, bool isTweenBack = false)
        { 
            UIBasePanel basePanel;                          //窗体基类

            //"正在显示集合"中如果没有记录，则直接返回。
            _DicCurrentShowUIPanels.TryGetValue(strUIPanelName, out basePanel);
            if(basePanel==null) return ;
            //指定窗体，标记为“隐藏状态”，且从"正在显示集合"中移除。
            if (isTweenBack)
            {
                basePanel.HideTweenBack();
            }
            else
            {
                basePanel.Hide();
            }
            _DicCurrentShowUIPanels.Remove(strUIPanelName);
        }

        //（“反向切换”属性）窗体的出栈逻辑
        private void PopUIPanels(bool isTweenBack = false)
        { 
            if(_StackCurrentUIPanels.Count>=2)
            {
                //出栈处理
                UIBasePanel topUIPanels = _StackCurrentUIPanels.Pop();
                //做隐藏处理
                if (isTweenBack)
                {
                    topUIPanels.HideTweenBack();
                }
                else
                {
                    topUIPanels.Hide();
                }
                //出栈后，下一个窗体做“重新显示”处理。
                UIBasePanel nextUIPanels = _StackCurrentUIPanels.Peek();
                nextUIPanels.Reshow();
            }
            else if (_StackCurrentUIPanels.Count ==1)
            {
                //出栈处理
                UIBasePanel topUIPanels = _StackCurrentUIPanels.Pop();
                //做隐藏处理
                if (isTweenBack)
                {
                    topUIPanels.HideTweenBack();
                }
                else
                {
                    topUIPanels.Hide();
                }
            }
        }

        /// <summary>
        /// (“隐藏其他”属性)打开窗体，且隐藏其他窗体
        /// </summary>
        /// <param name="strUIName">打开的指定窗体名称</param>
        private void EnterUIPanelsAndHideOther(string strUIName)
        { 
            UIBasePanel baseUIPanel;                          //UI窗体基类
            UIBasePanel baseUIPanelALL;                   //从集合中得到的UI窗体基类


            //参数检查
            if (string.IsNullOrEmpty(strUIName)) return;

            _DicCurrentShowUIPanels.TryGetValue(strUIName, out baseUIPanel);
            if (baseUIPanel != null) return;

            //把“正在显示集合”与“栈集合”中所有窗体都隐藏。
            foreach (UIBasePanel baseUI in _DicCurrentShowUIPanels.Values)
            {
                baseUI.Hide();
            }
            foreach (UIBasePanel staUI in _StackCurrentUIPanels)
            {
                staUI.Hide();
            }

            //把当前窗体加入到“正在显示窗体”集合中，且做显示处理。
            _DicALLUIPanels.TryGetValue(strUIName, out baseUIPanelALL);
            if (baseUIPanelALL!=null)
            {
                _DicCurrentShowUIPanels.Add(strUIName, baseUIPanelALL);
                //窗体显示
                baseUIPanelALL.Show();
            }
        }

        /// <summary>
        /// (“隐藏其他”属性)关闭窗体，且显示其他窗体
        /// </summary>
        /// <param name="strUIName">打开的指定窗体名称</param>
        private void ExitUIPanelsAndOpenOther(string strUIName, bool isTweenBack = false)
        {
            UIBasePanel baseUIPanel;                          //UI窗体基类


            //参数检查
            if (string.IsNullOrEmpty(strUIName)) return;

            _DicCurrentShowUIPanels.TryGetValue(strUIName, out baseUIPanel);
            if (baseUIPanel == null) return;

            //当前窗体隐藏状态，且“正在显示”集合中，移除本窗体
            if (isTweenBack)
            {
                baseUIPanel.HideTweenBack();
            }
            else
            {
                baseUIPanel.Hide();
            }
            _DicCurrentShowUIPanels.Remove(strUIName);

            //把“正在显示集合”与“栈集合”中所有窗体都定义重新显示状态。
            foreach (UIBasePanel baseUI in _DicCurrentShowUIPanels.Values)
            {
                baseUI.Reshow();
            }
            foreach (UIBasePanel staUI in _StackCurrentUIPanels)
            {
                staUI.Reshow();
            }
        }

        /// <summary>
        /// 是否清空“栈集合”中得数据
        /// </summary>
        /// <returns></returns>
        private bool ClearStackArray()
        {
            if (_StackCurrentUIPanels != null && _StackCurrentUIPanels.Count>=1)
            {
                //清空栈集合
                _StackCurrentUIPanels.Clear();
                return true;
            }

            return false;
        }

        /// <summary>
        /// 初始化“UI窗体预设”路径数据
        /// </summary>
	    private void InitUIPanelsPathData()
	    {
            IConfigManager configMgr = new ConfigManagerByJson(BasicDefine.SYS_PATH_UIPANELS_CONFIG_INFO);
            if (configMgr!=null)
            {
                _DicPanelsPaths = configMgr.AppSetting;
            }
	    }

	    #endregion

    }//class_end
}