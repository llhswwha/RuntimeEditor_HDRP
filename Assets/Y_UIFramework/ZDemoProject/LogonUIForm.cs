/***
 * 
 *    Title: Y_UIFramework" UI框架项目
 *           主题： 登陆窗体   
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
using UnityEngine.UI;

namespace DemoProject
{
    public class LogonUIForm : UIBasePanel
    {
        public Text TxtLogonName;                           //登陆名称
        public Text TxtLogonNameByBtn;                      //登陆名称(Button)

        public void Awake()
        {
            //定义本窗体的性质(默认数值，可以不写)
            base.CurrentUIType.UIPanels_Type = UIPanelType.Normal;
            base.CurrentUIType.UIPanels_ShowMode = UIPanelShowMode.Normal;
            base.CurrentUIType.UIPanel_LucencyType = UIPanelTransparentType.Transparent;
            /* 给按钮注册事件 */
            //RigisterButtonObjectEvent("Btn_OK", LogonSys);
            //Lamda表达式写法
            RigisterButtonObjectEvent("Btn_OK", 
                p=>OpenUIPanel(ProConst.SELECT_HERO_FORM)
                );

        }


        public void Start()
        {
            //string strDisplayInfo = LauguageMgr.GetInstance().ShowText("LogonSystem");

            if (TxtLogonName)
            {
                TxtLogonName.text = GetLocalText("LogonSystem");
            }
            if (TxtLogonNameByBtn)
            {
                TxtLogonNameByBtn.text = GetLocalText("LogonSystem");
            }
        }

    }
}