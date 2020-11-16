/***
 * 
 *    Title: "Y_UIFramework" UI框架项目
 *           主题： xxx    
 *    Description: 
 *           功能： yyy
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
	public class StartProject : MonoBehaviour {

		void Start () {
            YLog.Write(GetType()+"/Start()/");
            //加载登陆窗体
            UIManager.GetInstance().ShowUIPanel(ProConst.LOGON_FROMS);         			
		}
		
	}
}