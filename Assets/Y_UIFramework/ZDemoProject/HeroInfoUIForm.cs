/***
 * 
 *    Title: "Y_UIFramework" UI框架项目
 *           主题： 英雄信息显示窗体
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
	public class HeroInfoUIForm : UIBasePanel {


		void Awake () 
        {
		    //窗体性质
            CurrentUIType.UIPanels_Type = UIPanelType.Fixed;  //固定在主窗体上面显示
            
        }
		
	}
}