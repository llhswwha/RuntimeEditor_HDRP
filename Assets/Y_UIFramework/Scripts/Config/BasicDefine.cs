/***
 * 
 *    Title: "Y_UIFramework" UI框架项目
 *           主题： 框架核心参数  
 *    Description: 
 *           功能：
 *           1： 系统常量
 *           2： 全局性方法。
 *           3： 系统枚举类型
 *           4： 委托定义
 *                          
 *    Date: 
 *    Version: 0.1版本
 *    Modify Recoder: 
 *    
 *   
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Y_UIFramework
{
    #region 系统枚举类型

    /// <summary>
    /// UI窗体（位置）类型
    /// </summary>
    public enum UIPanelType
    {
        //普通窗体
        Normal,   
        //固定窗体                              
        Fixed,
        //弹出窗体
        PopUp
    }

    /// <summary>
    /// UI窗体的显示类型
    /// </summary>
    public enum UIPanelShowMode
    {
        //普通:多个窗体同时显示
        Normal,
        //反向切换
        ReverseChange,
        //隐藏其他
        HideOther
    }

    /// <summary>
    /// UI窗体透明度类型
    /// </summary>
    public enum UIPanelTransparentType
    {
        //完全透明，不能穿透
        Transparent,
        //半透明，不能穿透
        Translucent,
        //低透明度，不能穿透
        LowTransparency,
        //可以穿透
        Pentrate    
    }

    #endregion

    public class BasicDefine : MonoBehaviour {
        /* 路径常量 */
        public const string SYS_PATH_CANVAS = "Canvas";
        public const string SYS_PATH_UICAMERA = "UICamera";
        public const string SYS_PATH_ALLFOLLOWPANEL = "UI/Basic/AllFollowPanel";
        public const string SYS_PATH_MESSAGEBOX = "UI/Basic/MessageBox";
        public const string SYS_PATH_UGUITOOLTIP = "UI/Basic/UGUITooltip";
        public const string SYS_PATH_PROGRESSBAR = "UI/Basic/ProgressBar";
        public const string SYS_PATH_PROGRESSBARBOTTOM = "UI/Basic/ProgressBarBottom";
        public const string SYS_PATH_FPS = "UI/Basic/FPS";

        public const string SYS_PATH_UIPANELS_CONFIG_INFO = "UIPanelsConfigInfo";
        public const string SYS_PATH_CONFIG_INFO = "SysConfigInfo";

        /* 标签常量 */
        public const string SYS_TAG_CANVAS = "NewCanvas";//NewCanvas//_TagCanvas
        /* 节点常量 */
        public const string SYS_NORMAL_NODE = "Normal";
        public const string SYS_FIXED_NODE = "Fixed";
        public const string SYS_POPUP_NODE = "PopUp";
        public const string SYS_TOPUI_NODE = "TopUI";
        public const string SYS_SCRIPTMANAGER_NODE = "_ScriptMgr";
        /* 遮罩管理器中，透明度常量 */
        public const float SYS_UIMASK_LUCENCY_COLOR_RGB = 255 / 255F;
        public const float SYS_UIMASK_LUCENCY_COLOR_RGB_A = 0F / 255F;

        public const float SYS_UIMASK_TRANS_LUCENCY_COLOR_RGB = 220 / 255F;
        public const float SYS_UIMASK_TRANS_LUCENCY_COLOR_RGB_A = 50F / 255F;

        public const float SYS_UIMASK_IMPENETRABLE_COLOR_RGB = 50 / 255F;
        public const float SYS_UIMASK_IMPENETRABLE_COLOR_RGB_A = 200F / 255F;

        /* 摄像机层深的常量 */

        /* 全局性的方法 */
        //Todo...

        /* 委托的定义 */
        //Todo....

    }
}