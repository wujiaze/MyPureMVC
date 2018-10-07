/*
 *
 *		Title: "AppByPureMVC" 应用项目
 *			    主题: 使用 PureMVC 架构 的 APP 示例应用项目
 *			        
 *			        视图层 ：用户列表信息操作类
 *		Description:
 *				功能:
 *
 *		Date: 2018.5.13
 *		Version: 0.1
 *		Modify Recoder:
 *
 *
 */

using System.Collections.Generic;
using PureMVC.Interfaces;
using PureMVC.Patterns;
using SimpleUIFramework;
using UnityEngine;
using UnityEngine.UI;

namespace AppByPureMVC
{
    public class UserListMediator : Mediator
    {
        // 字段 MVC 框架注册需要
        public new const string NAME = "UserListMediator";
        // 模型层，用户信息代理类
        private UserProxy _userProxy;
        // 当前用户选择的用户信息
        private UserList_Item _currentUserItem = null;
        // 只读属性
        private UserList UserList 
        {
            get { return base.ViewComponent as UserList; } 

        }

        public UserListMediator():base(NAME)
        {
            Log.Write(GetType() + "/ UserListMediator() ");
        }
        
        /// <summary>
        /// 本类实例在注册的时候触发
        /// </summary>
        public override void OnRegister()
        {
            // 得到用户代理的引用  // 低频率 视图层调用模型层
            _userProxy = Facade.RetrieveProxy(UserProxy.NAME) as UserProxy;
        }

        /// <summary>
        /// 列举所有关注的 "消息"
        /// </summary>
        /// <returns></returns>
        public override IList<string> ListNotificationInterests()
        {
            IList<string> list = new List<string>();
            list.Add(Proconst.MSG_INIT_USERLIST_MEDIATOR); // 初始化本类实例
            list.Add(Proconst.MSG_SELECT_USERINFO_TO_USERLIST_MEDIATOR); // 本类下级 控制脚本发来的选择信息
            list.Add(Proconst.MSG_ADDNEWUSERIBFO_TOUSERLIST_MEDIATOR); // 从 UserForm 发来的消息：增加新用户
            list.Add(Proconst.MSG_UPDATEUSERINFO_TOUSERLIST_MEDIATOR); // 从 UserForm 发来的消息：更新用户信息
            list.Add(Proconst.MSG_DONT_SELECT);
            return list;
        }
        /// <summary>
        /// 处理所有关注的 “消息”
        /// </summary>
        /// <param name="notification"></param>
        public override void HandleNotification(INotification notification)
        {
            switch (notification.Name)
            {
                // 调用初始化方法
                case Proconst.MSG_INIT_USERLIST_MEDIATOR:
                {
                    UserList tmp = notification.Body as UserList;
                    InitUserListMediator(tmp);
                    break;
                }
                // 处理用户选择 “用户记录”
                case Proconst.MSG_SELECT_USERINFO_TO_USERLIST_MEDIATOR:
                {
                    UserList_Item tmp = notification.Body as UserList_Item;
                    HandlerSelectUserInfo(tmp);
                    break;
                }
                // 处理 从 UserForm 发来的消息：增加新用户
                case Proconst.MSG_ADDNEWUSERIBFO_TOUSERLIST_MEDIATOR:
                {
                    UserData tmp = notification.Body as UserData;
                        SubmitNewUserInfo(tmp);
                    UserList.FreezeDeleteBtn();
                        break;
                }
                // 处理 从 UserForm 发来的消息：更新用户信息
                case Proconst.MSG_UPDATEUSERINFO_TOUSERLIST_MEDIATOR:
                {
                    UserData tmp = notification.Body as UserData;
                    SubmitUpdateUserInfo(tmp);
                    UserList.FreezeDeleteBtn();
                        break;
                }
                case Proconst.MSG_DONT_SELECT:
                {
                    UserList.FreezeDeleteBtn();
                        SendNotification(Proconst.MSG_SDDD);
                        break;
                }
                default:
                    break;
            }
        }
        
        /// <summary>
        /// 初始化用户列表Mediator
        /// </summary>
        /// <param name="userList"></param>
        private void InitUserListMediator(UserList userListObj)
        {
            if (userListObj != null)
            {
                Log.Write(GetType() + "/ InitUserListMediator() ");
                base.ViewComponent = userListObj;
                userListObj.NewUser += NewUserHandle;
                userListObj.DeleteUser += DeleteHandle;
            }
            Log.Write(GetType() + "/ InitUserListMediator() count:"+ _userProxy.ListUsers.Count);
            // 加载且显示初始视图内容
            userListObj.LoadAndShowUserListInfo(_userProxy.ListUsers);
        }

        /* 发往视图层 */
        /// <summary>
        /// 按钮 新用户 处理
        /// </summary>
        private void NewUserHandle()
        {
            if (_currentUserItem != null)
            {
                _currentUserItem.GetComponent<Toggle>().isOn = false;
            }
            // 发送消息，给 “用户表单” 视图
            SendNotification(Proconst.MSG_ADDNEWUSERINFO_TOUSERFORM_MEDIATOR);
        }
        /// <summary>
        /// 按钮 删除 处理
        /// </summary>
        private void DeleteHandle()
        {
            if (_userProxy!=null&& _currentUserItem != null)
            {
                _userProxy.DeleteUserItem(_currentUserItem.CurrentData);
                UserList.LoadAndShowUserListInfo(_userProxy.ListUsers);
                // 发送信息
                SendNotification(Proconst.MSG_REMOVEUSERINFO_TOUSERFORM_MEDIATOR);
                UserList.FreezeDeleteBtn();
            }
        }

        /// <summary>
        /// 处理 “选择用户操作”
        /// </summary>
        private void HandlerSelectUserInfo(UserList_Item userItem)
        {

            if (userItem == null)return;
            // 保存当前选择的用户信息
            _currentUserItem = userItem;
            // 显示窗体 “删除” 按钮 
            UserList.ShowFreezeDeleteBtn();
            // 发送消息到显示 “用户窗体” 的类  这里相当于一个中转站
            if (_currentUserItem.CurrentData==null) return;
            // 消息和执行体是绑定注册的，所以发送没有注册的消息，是没有任何作用的
            SendNotification(Proconst.MSG_SELECT_USERINFO_TO_USERFORM_MEDIATOR, _currentUserItem.CurrentData); 
        }

        /* 接收视图层消息的处理 */
        private void SubmitNewUserInfo(UserData userData)
        {  
            if(userData==null || _userProxy == null) return;
            _userProxy.AddUserItem(userData);
            // 刷新 窗体显示
            UserList.LoadAndShowUserListInfo(_userProxy.ListUsers);
        }

        private void SubmitUpdateUserInfo(UserData userData)
        {
            if (userData == null || _userProxy == null) return;
            Debug.Log(userData.UserName);
            _userProxy.UpdateUserItem(userData);
            // 刷新 窗体显示
            UserList.LoadAndShowUserListInfo(_userProxy.ListUsers);
        }
    }
}