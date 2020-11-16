/***
 * 
 *    Title: "Y_UIFramework" UI框架项目
 *           主题： 主城窗体
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
using Y_UIFramework;
using UnityEngine;

namespace DemoProject
{
    public class MainCityUIForm : UIBasePanel
    {
		public void Awake () 
        {
	        //窗体性质
		    CurrentUIType.UIPanels_ShowMode = UIPanelShowMode.HideOther;

		    //事件注册
            RigisterButtonObjectEvent("BtnMarket",
                p => OpenUIPanel(ProConst.MARKET_UIFORM)           
                );

        }
		
	}
}