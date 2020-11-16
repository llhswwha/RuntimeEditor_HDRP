/***
 * 
 *    Title: "Y_UIFramework" UI框架项目
 *           主题： PRG 游戏“选择角色”窗体 
 *    Description: 
 *           功能： 选择英雄窗体
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
    public class SelectHeroUIForm : UIBasePanel
    {

        public void Awake()
        {
            YLog.SyncLogCatchToFile();
            //窗体的性质
            CurrentUIType.UIPanels_ShowMode = UIPanelShowMode.HideOther;

            //注册进入主城的事件
            RigisterButtonObjectEvent("BtnConfirm",
                p =>
                {
                    OpenUIPanel(ProConst.MAIN_CITY_UIFORM);
                    OpenUIPanel(ProConst.HERO_INFO_UIFORM);
                }

                );

            //注册返回上一个页面
            RigisterButtonObjectEvent("BtnClose",
                m=>CloseUIPanel()
                );
        }
    }
}