/***
 * 
 *    Title: "Y_UIFramework" UI框架项目
 *           主题： 窗体类型   
 *    Description: 
 *           功能： 
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
	public class UIType {
        //是否清空“栈集合”
	    public bool IsClearStack = false;
        //UI窗体（位置）类型
	    public UIPanelType UIPanels_Type = UIPanelType.Normal;
        //UI窗体显示类型
	    public UIPanelShowMode UIPanels_ShowMode = UIPanelShowMode.Normal;
        //UI窗体透明度类型
	    public UIPanelTransparentType UIPanel_LucencyType = UIPanelTransparentType.Transparent;

	}
}