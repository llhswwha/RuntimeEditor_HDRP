/***
 * 
 *    Title: "Y_UIFramework" UI框架项目
 *           主题： 道具详细信息窗体 
 *    Description: 
 *           功能： 显示各种道具信息
 *                  
 *    Date: 
 *    Version: 0.1版本
 *    Modify Recoder: 
 *    
 *   
 */
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using Y_UIFramework;
using UnityEngine;
using UnityEngine.UI;

namespace DemoProject
{
	public class PropDetailUIForm : UIBasePanel
	{
	    public Text TxtName;                                //窗体显示名称

		void Awake () 
        {
		    //窗体的性质
		    CurrentUIType.UIPanels_Type = UIPanelType.PopUp;
		    CurrentUIType.UIPanels_ShowMode = UIPanelShowMode.ReverseChange;
		    CurrentUIType.UIPanel_LucencyType = UIPanelTransparentType.Translucent;

            /* 按钮的注册  */
            RigisterButtonObjectEvent("BtnClose",
                p=>CloseUIPanel()
                );

            /*  接受信息   */
            RegisterMsgListener("Props", 
                p =>
                {
                    if (TxtName)
                    {
                        string[] strArray = p.Values as string[];
                        TxtName.text = strArray[0];
                        //print("测试道具的详细信息： "+strArray[1]);
                    }
                }
           );

        }//Awake_end
		
	}
}