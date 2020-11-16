/***
 * 
 *    Title: "Y_UIFramework" UI框架项目
 *           主题： 测试脚本 
 *    Description: 
 *           功能： 
 *                 测试我们框架中用到的一些基础知识点。
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
	public class Test : MonoBehaviour {
        //Dictionary<string,string> _dicTest=new Dictionary<string, string>(); 
        private Stack<string> _StaArray=new Stack<string>(); 

		void Start ()
		{
		    Test2();
		}

        //演示Dictionary 中得一个方法。
	    private void Test1()
	    {
            //string strResult = string.Empty;  //输出内容

            //_dicTest.Add("zhangsan", "张三");
            //_dicTest.Add("lisi", "李四");

            ////查询
            //_dicTest.TryGetValue("zhangsan123", out strResult);
            //print("查询结果 strResult=" + strResult);	    
        
        }

        //演示Stack<T> 类的常用属性与方法
	    private void Test2()
	    {
	        //压栈
            _StaArray.Push("zhangsan");
            _StaArray.Push("lisi");
            _StaArray.Push("wangwu");

            //查询栈顶元素
            //print(_StaArray.Peek());
            //print(_StaArray.Peek());
            //print(_StaArray.Peek());

            //出栈操作,且返回结果
            //print(_StaArray.Pop());
            //print(_StaArray.Pop());
            //print(_StaArray.Pop());

            //演示使用"迭代器"输出所有内容。
	        IEnumerator<string> ie= _StaArray.GetEnumerator();
	        while (ie.MoveNext())
	        {
	            print(ie.Current);
	        }
	    }

	}
}