/***
 * 
 *    Title: "Y_UIFramework" UI框架项目
 *           主题： Json 解析异常
 *    Description: 
 *           功能：专门负责对于JSon 由于路径错误，或者Json 格式错误造成的异常，进行捕获。
 *                  
 *    Date: 
 *    Version: 0.1版本
 *    Modify Recoder: 
 *    
 *   
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Y_UIFramework
{
	public class JsonAnlysisException : Exception {
	    public JsonAnlysisException() : base(){}
	    public JsonAnlysisException(string exceptionMessage) : base(exceptionMessage){}
	}
}