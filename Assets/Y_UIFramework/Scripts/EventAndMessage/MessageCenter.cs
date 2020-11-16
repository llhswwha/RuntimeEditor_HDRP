/***
 * 
 *    Title: "Y_UIFramework" UI框架项目
 *           主题： 消息（传递）中心
 *    Description: 
 *           功能： 负责UI框架中，所有UI窗体中间的数据传值。
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
	public class MessageCenter {
        //委托：消息传递
	    public delegate void DelMessenger(KeyValueUpdate kv);

        //消息中心缓存集合
        //<string : 数据大的分类，DelMessageDelivery 数据执行委托>
	    public static Dictionary<string, DelMessenger> _dicMessages = new Dictionary<string, DelMessenger>();

        /// <summary>
        /// 增加消息的监听。
        /// </summary>
        /// <param name="messageType">消息分类</param>
        /// <param name="handler">消息委托</param>
	    public static void AddMsgListener(string messageType,DelMessenger handler)
	    {
            if (!_dicMessages.ContainsKey(messageType))
	        {
                _dicMessages.Add(messageType,null);
            }
	        _dicMessages[messageType] += handler;
	    }

        /// <summary>
        /// 取消消息的监听
        /// </summary>
        /// <param name="messageType">消息分类</param>
        /// <param name="handele">消息委托</param>
	    public static void RemoveMsgListener(string messageType,DelMessenger handele)
	    {
            if (_dicMessages.ContainsKey(messageType))
            {
                _dicMessages[messageType] -= handele;
            }

	    }

        /// <summary>
        /// 取消所有指定消息的监听
        /// </summary>
	    public static void ClearALLMsgListener()
	    {
	        if (_dicMessages!=null)
	        {
	            _dicMessages.Clear();
            }
	    }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="messageType">消息的分类</param>
        /// <param name="kv">键值对(对象)</param>
	    public static void SendMsg(string messageType,KeyValueUpdate kv)
	    {
	        DelMessenger del;                         //委托

	        if (_dicMessages.TryGetValue(messageType,out del))
	        {
	            if (del!=null)
	            {
                    //调用委托
	                del(kv);
	            }
	        }
	    }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="msgType">消息的类型</param>
        /// <param name="msgName">消息名称</param>
        /// <param name="msgContent">消息内容</param>
        public static void SendMsg(string msgType, string msgName, object msgContent)
        {
            KeyValueUpdate kvs = new KeyValueUpdate(msgName, msgContent);
            MessageCenter.SendMsg(msgType, kvs);
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="msgType">消息的类型</param>
        /// <param name="msgName">消息名称</param>
        /// <param name="paras">可变多参数，消息内容</param>
        public static void SendMsgParams(string msgType, string msgName, params object[] paras)
        {
            KeyValueUpdate kvs = new KeyValueUpdate(msgName, paras);
            MessageCenter.SendMsg(msgType, kvs);
        }
    }

    /// <summary>
    /// 键值更新对
    /// 功能： 配合委托，实现委托数据传递
    /// </summary>
    public class KeyValueUpdate
    {   //键
        private string _Key;
        //值
        private object _Values;

        /*  只读属性  */

        public string Key
        {
            get { return _Key; }
        }
        public object Values
        {
            get { return _Values; }
        }

        public KeyValueUpdate(string key, object valueObj)
        {
            _Key = key;
            _Values = valueObj;
        }
    }


}