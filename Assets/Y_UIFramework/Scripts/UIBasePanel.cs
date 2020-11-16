/***
 * 
 *    Title: "Y_UIFramework" UI框架项目
 *           主题: UI窗体的父类
 *    Description: 
 *           功能：定义所有UI窗体的父类。
 *           定义四个生命周期
 *           
 *           1：Display 显示状态。
 *           2：Hiding 隐藏状态
 *           3：ReDisplay 再显示状态。
 *           4：Freeze 冻结状态。
 *           
 *                  
 *    Date: 
 *    Version: 0.1版本
 *    Modify Recoder: 
 *    
 *   
 */
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;

namespace Y_UIFramework
{
	public class UIBasePanel : MonoBehaviour {
        /*字段*/
        private UIType _CurrentUIType=new UIType();

        /* 属性*/
        //当前UI窗体类型
	    public UIType CurrentUIType
	    {
	        get { return _CurrentUIType; }
	        set { _CurrentUIType = value; }
	    }

        private bool isTween = true;

        public bool IsTween { get { return isTween; } set { isTween = value; } }
        /// <summary>
        /// UI动画
        /// </summary>
        private Tween tween;


        #region  窗体的四种(生命周期)状态

        /// <summary>
        /// 显示状态
        /// </summary>
	    public virtual void Show()
	    {
            this.gameObject.SetActive(true);
            if (isTween)
            {
                TweenPlay();
            }

            //设置模态窗体调用(必须是弹出窗体)
            if (_CurrentUIType.UIPanels_Type==UIPanelType.PopUp)
            {
                UIMaskMgr.GetInstance().SetMaskWindow(this.gameObject,_CurrentUIType.UIPanel_LucencyType);
            }
        }

        /// <summary>
        /// 隐藏状态
        /// </summary>
	    public virtual void Hide()
	    {
            if (isTween)
            {
                //TweenBack();
                TweenRewind();
            }
            else
            {
                this.gameObject.SetActive(false);
            }
            //取消模态窗体调用
            if (_CurrentUIType.UIPanels_Type == UIPanelType.PopUp)
            {
                UIMaskMgr.GetInstance().CancelMaskWindow();
            }
        }

        public virtual void HideTweenBack()
        {
            if (isTween)
            {
                TweenBack();
            }
            else
            {
                this.gameObject.SetActive(false);
            }
            //取消模态窗体调用
            if (_CurrentUIType.UIPanels_Type == UIPanelType.PopUp)
            {
                UIMaskMgr.GetInstance().CancelMaskWindow();
            }
        }

        /// <summary>
        /// 重新显示状态
        /// </summary>
	    public virtual void Reshow()
	    {
            this.gameObject.SetActive(true);
            if (isTween)
            {
                TweenPlay();
            }
            //设置模态窗体调用(必须是弹出窗体)
            if (_CurrentUIType.UIPanels_Type == UIPanelType.PopUp)
            {
                UIMaskMgr.GetInstance().SetMaskWindow(this.gameObject, _CurrentUIType.UIPanel_LucencyType);
            }
        }

        /// <summary>
        /// 冻结状态
        /// </summary>
	    public virtual void Pause()
	    {
            //this.gameObject.SetActive(true);
            this.gameObject.SetActive(false);
        }


        #endregion

        #region 封装子类常用的方法

        /// <summary>
        /// 注册按钮事件
        /// </summary>
        /// <param name="buttonName">按钮节点名称</param>
        /// <param name="delHandle">委托：需要注册的方法</param>
	    protected void RigisterButtonObjectEvent(string buttonName,EventTriggerListener.VoidDelegate  delHandle)
	    {
            GameObject goButton = UnityHelper.FindTheChildNode(this.gameObject, buttonName).gameObject;
            //给按钮注册事件方法
            if (goButton != null)
            {
                EventTriggerListener.Get(goButton).onClick = delHandle;
            }	    
        }

        /// <summary>
        /// 打开UI窗体
        /// </summary>
        /// <param name="uiPanelName"></param>
	    protected void OpenUIPanel(string uiPanelName)
	    {
            UIManager.GetInstance().ShowUIPanel(uiPanelName);
        }

        /// <summary>
        /// 关闭当前UI窗体
        /// </summary>
	    protected void CloseUIPanel()
	    {
	        string strUIFromName = string.Empty;            //处理后的UIFrom 名称
	        int intPosition = -1;

            strUIFromName=GetType().ToString();             //命名空间+类名
            intPosition=strUIFromName.IndexOf('.');
            if (intPosition!=-1)
            {
                //剪切字符串中“.”之间的部分
                strUIFromName = strUIFromName.Substring(intPosition + 1);
            }

            UIManager.GetInstance().CloseUIPanels(strUIFromName);
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="msgType">消息的类型</param>
        /// <param name="msgName">消息名称</param>
        /// <param name="msgContent">消息内容</param>
	    protected void SendMsg(string msgType,string msgName,object msgContent)
	    {
            KeyValueUpdate kvs = new KeyValueUpdate(msgName,msgContent);
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

        /// <summary>
        /// 接收消息
        /// </summary>
        /// <param name="messagType">消息分类</param>
        /// <param name="handler">消息委托</param>
	    public void RegisterMsgListener(string messagType,MessageCenter.DelMessenger handler)
	    {
            MessageCenter.AddMsgListener(messagType, handler);
	    }

        /// <summary>
        /// 显示语言
        /// </summary>
        /// <param name="id"></param>
	    public string GetLocalText(string id)
        {
            string strResult = string.Empty;

            strResult = LauguageMgr.GetInstance().ShowText(id);
            return strResult;
        }

        #endregion

        #region 动画控制部分

        /// <summary>
        /// 创建动画
        /// </summary>
        protected virtual void CreateTween()
        {
            CanvasGroup canvasGroup = gameObject.GetComponent<CanvasGroup>();
            if (canvasGroup == null)
            {
                canvasGroup = gameObject.AddComponent<CanvasGroup>();
            }
            Sequence sequence = DOTween.Sequence();
            canvasGroup.alpha = 0;
            Tween tweenalpha = DOTween.To(() => canvasGroup.alpha, x => canvasGroup.alpha = x, 1, 0.3F).SetEase(Ease.InSine);
            sequence.Append(tweenalpha);

            if (CurrentUIType.UIPanels_Type == UIPanelType.PopUp)
            {
                transform.localScale = Vector3.zero;
                Tween tweenscale = transform.DOScale(Vector3.one, 0.3F).SetEase(Ease.InSine);
                sequence.Join(tweenscale);
            }

            tween = sequence;

            tween.SetAutoKill(false);
            tween.Pause();

            InitTween(true, sequence, null);
        }

        /// <summary>
        /// 初始化动画
        /// </summary>
        /// <param name="isTweenT">是否开启动画</param>
        /// <param name="tw">动画</param>
        /// <param name=""></param>
        protected void InitTween(bool isTweenT, Tween tw, Action rewindAction)
        {
            isTween = isTweenT;
            if (isTween&& tw!=null)
            {
                tween = tw;
            }
            tween.OnRewind(() =>
            {
                if (rewindAction != null)
                {
                    rewindAction();
                }
                this.gameObject.SetActive(false);//动画绑定关闭方法
            });
        }

        /// <summary>
        /// 
        /// </summary>
        protected virtual void TweenPlay()
        {
            if (tween == null) CreateTween();
            tween.PlayForward();
        }

        protected virtual void TweenBack()
        {
            if (tween == null) CreateTween();
            tween.PlayBackwards();
        }

        protected void TweenRewind()
        {
            if (tween == null) CreateTween();
            tween.Rewind();
        }

        protected void TweenRestart()
        {
            if (tween == null) CreateTween();
            tween.Restart();
        }

        #endregion

    }
}