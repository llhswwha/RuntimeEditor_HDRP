/***
 * 
 *    Title: "Y_UIFramework" UI框架项目
 *           主题： “商城窗体”   
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
    public class MarketUIFrom : UIBasePanel
    {
		void Awake ()
        {
		    //窗体性质
		    CurrentUIType.UIPanels_Type = UIPanelType.PopUp;  //弹出窗体
		    CurrentUIType.UIPanel_LucencyType = UIPanelTransparentType.Translucent;
		    CurrentUIType.UIPanels_ShowMode = UIPanelShowMode.ReverseChange;

            //注册按钮事件：退出
            RigisterButtonObjectEvent("Btn_Close",
                P=> CloseUIPanel()                
                );
            //注册道具事件：神杖 
            RigisterButtonObjectEvent("BtnTicket",
                P =>
                {
                    //打开子窗体
                    OpenUIPanel(ProConst.PRO_DETAIL_UIFORM);
                    //传递数据
                    string[] strArray = new string[] { "神杖详情", "神杖详细介绍。。。" };
                    SendMsg("Props", "ticket", strArray);
                }
                );

            //注册道具事件：战靴 
            RigisterButtonObjectEvent("BtnShoe",
                P =>
                {
                    //打开子窗体
                    OpenUIPanel(ProConst.PRO_DETAIL_UIFORM);
                    //传递数据
                    string[] strArray = new string[] { "战靴详情", "战靴详细介绍。。。" };
                    SendMsg("Props", "shoes", strArray);
                }
                );

            //注册道具事件：盔甲 
            RigisterButtonObjectEvent("BtnCloth",
                P =>
                {
                    //打开子窗体
                    OpenUIPanel(ProConst.PRO_DETAIL_UIFORM);
                    //传递数据
                    string[] strArray = new string[] { "盔甲详情", "盔甲详细介绍。。。" };
                    SendMsg("Props", "cloth", strArray);
                }
                );
        }
		
	}
}