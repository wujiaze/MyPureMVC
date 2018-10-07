/*
 *
 *		Title: "AppByPureMVC" 应用项目
 *			    主题: 使用 PureMVC 架构 的 APP 示例应用项目
 *			    控制层 ： 启动应用 ，初始化用户列表
 *		Description:
 *				功能:
 *
 *		Date: 2018.5.13
 *		Version: 0.1
 *		Modify Recoder:
 *
 *
 */

using Assets.Scripts.Helper;
using PureMVC.Interfaces;
using PureMVC.Patterns;
using SimpleUIFramework;

namespace AppByPureMVC
{
	public class StartUpApplication_Command:SimpleCommand
	{
	    private UserEmployeeInfo _userRmpInfo = null;
        public override void Execute(INotification notification)
        {
            Log.Write(GetType() + "/ Execute() "); // 日志
            _userRmpInfo = notification.Body as UserEmployeeInfo;
            if (_userRmpInfo != null)
            {
                // 控制层 消息通知 视图层 （低频率）
                // 初始化 用户列表操作类 窗体
                SendNotification(Proconst.MSG_INIT_USERLIST_MEDIATOR, _userRmpInfo.UserList); 
                // 初始化 用户显示操作类 窗体
                SendNotification(Proconst.MSG_INIT_USERFORM_MEDIATOR, _userRmpInfo.UserForm);

            }
        }
    }
}