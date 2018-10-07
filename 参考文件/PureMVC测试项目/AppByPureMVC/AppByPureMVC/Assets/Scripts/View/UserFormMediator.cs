/*
 *
 *		Title: "AppByPureMVC" 应用项目
 *			    主题: 使用 PureMVC 架构 的 APP 示例应用项目
 *			    视图层： 用户窗体操作类
 *		Description:
 *				功能:
 *
 *		Date: 2018.5.13
 *		Version: 0.1
 *		Modify Recoder:
 *
 *
 */

using System;
using System.Collections.Generic;
using PureMVC.Interfaces;
using PureMVC.Patterns;
using UnityEngine;

namespace AppByPureMVC
{
    /// <summary>
    /// 用户类型
    /// </summary>
    public enum UserFormType
    {
        Create,
        Update,
        Remove
    }

    public class UserFormMediator :Mediator
	{
	    public new const string NAME = "UserFormMediator";
        // 操作模型
	    private UserFormType _currentUserFormType = UserFormType.Create;
	    // PureMVC框架作用 ：说明 本操作类是针对哪一个视图层
        private UserForm UserForm {  get {return  base.ViewComponent as UserForm;}}

        // 将 NAME 传入基类
        public UserFormMediator() : base(NAME){} 

        /// <summary>
        /// 设置本类需要处理的信息
        /// </summary>
        /// <returns></returns>
        public override IList<string> ListNotificationInterests()
        {
            IList<string> list = new List<string>();
            list.Add(Proconst.MSG_INIT_USERFORM_MEDIATOR);  // 最终 在 View层的 m_observerMap 中
            list.Add(Proconst.MSG_SELECT_USERINFO_TO_USERFORM_MEDIATOR);
            list.Add(Proconst.MSG_ADDNEWUSERINFO_TOUSERFORM_MEDIATOR);
            list.Add(Proconst.MSG_REMOVEUSERINFO_TOUSERFORM_MEDIATOR);
            list.Add(Proconst.MSG_SDDD);
            return list;
        }

        /// <summary>
        /// 处理发送过来的消息
        /// </summary>
        /// <param name="notification"></param>
	    public override void HandleNotification(INotification notification)
        {
            switch (notification.Name)
            {
                // 初始化 用户窗体
                case Proconst.MSG_INIT_USERFORM_MEDIATOR:
                    {
                        UserForm tmp = notification.Body as UserForm;
                        if (tmp != null) InitUserFormMediator(tmp);
                        break;
                    }
                // 显示 用户选择信息
                case Proconst.MSG_SELECT_USERINFO_TO_USERFORM_MEDIATOR:
                    {
                        _currentUserFormType = UserFormType.Update;
                        UserData tmp = notification.Body as UserData;
                        if (tmp != null) DisplayOrUpdateUserForm(_currentUserFormType, tmp);
                        break;
                    }
                //  新建 用户信息
                case Proconst.MSG_ADDNEWUSERINFO_TOUSERFORM_MEDIATOR:
                    {
                        _currentUserFormType = UserFormType.Create;
                        DisplayOrUpdateUserForm(_currentUserFormType);
                        break;
                    }
                // 清除 用户信息
                case Proconst.MSG_REMOVEUSERINFO_TOUSERFORM_MEDIATOR:
                    {
                        _currentUserFormType = UserFormType.Remove;
                        DisplayOrUpdateUserForm(_currentUserFormType); // 界面清除 内存清理在 ListMediator 中 ，这样 职责明确
                        // 重置状态
                        _currentUserFormType = UserFormType.Create;
                        break;
                    }
                case Proconst.MSG_SDDD:
                {
                    // 确认按钮 冻结
                    UserForm.FreezeConfirmBtn();
                    _currentUserFormType = UserFormType.Create;
                        // 清空当前数据 用户窗体
                        UserForm.ClearUserInfo();
                        break;
                }
                default:
                    break;
            }
        }




        /// <summary>
        /// 初始化本类参数
        /// </summary>
	    internal void InitUserFormMediator(UserForm userformObj)
	    {
	        if (userformObj!=null)
	        {
	            base.m_mediatorName = NAME;
	            base.m_viewComponent = userformObj;
                // 注册委托 ：框架外部，类与类之间的消息传递，普通的定义委托传递消息
	            userformObj.ConfirmBtnHandler += SubmitUserInfo;
            }
	    }

        /// <summary>
        /// 提交 ：用户点击 “确认” 按钮
        /// </summary>
	    private void SubmitUserInfo()
	    {
            switch (_currentUserFormType)
            {
                case UserFormType.Create: 
                    AddNewUserInfo(UserForm.UserData);
                    break;
                case UserFormType.Update:
                    UpdateUserInfo(UserForm.UserData);
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// 提交：增加新用户
        /// 功能：往 “用户列表” Mediator 发送消息，增加一条新纪录，且显示
        /// </summary>
	    private void AddNewUserInfo(UserData userdata)
	    {
	        if (userdata != null && UserForm!=null)
	        {
                // 确认按钮 冻结
	            UserForm.FreezeConfirmBtn();
                SendNotification(Proconst.MSG_ADDNEWUSERIBFO_TOUSERLIST_MEDIATOR, userdata);
                // 清空当前数据 用户窗体
	            UserForm.ClearUserInfo();
            }
        }
        /// <summary>
        /// 提交 ：更新用户信息
        /// 功能：往 “用户列表” Mediator 发送消息，更新一条新纪录，且显示
        /// </summary>
	    private void UpdateUserInfo(UserData userdata) 
	    {
	        if (userdata != null && UserForm != null)
	        {
	            // 确认按钮 冻结
	            UserForm.FreezeConfirmBtn();
	            SendNotification(Proconst.MSG_UPDATEUSERINFO_TOUSERLIST_MEDIATOR, userdata);
                // 清空当前数据 用户窗体
                UserForm.ClearUserInfo();
	        } 
        }


        /// <summary>
        /// 显示 用户选择信息 或者 新建用户 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="userData"></param>
	    private void DisplayOrUpdateUserForm(UserFormType type,UserData userData =null)
        {
            if (UserForm != null)
                UserForm.ShowUserInfo(type, userData);
        }



    }
}