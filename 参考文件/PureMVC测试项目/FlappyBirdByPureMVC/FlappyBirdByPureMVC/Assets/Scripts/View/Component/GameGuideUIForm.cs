/*
 *
 *		Title: "FlappyBirdByMVC" 项目
 *			    主题: 游戏玩法介绍 UI
 *		Description:
 *				功能:
 *
 *		Date: 2018.5.9
 *		Version: 0.1
 *		Modify Recoder:
 *
 *
 */

using PureMVC.Patterns;
using SimpleUIFramework;
using UnityEngine;
namespace PureMVCdemo
{
	public class GameGuideUIForm : BaseUIForm 
	{
	    void Awake()
	    {
	        //本窗体类型
	        CurrentUiType.UIForms_ShowMode = UIFormShowMode.HideOther;
	        CurrentUiType.IsClearStack = false;
	        CurrentUiType.UIForms_Type = UIFormsType.Normal;
            // 注册按钮事件
            RigisterButtonObjectEvent(ProjectConst.BTN_GUIDE_OK, () =>
            {
                OpenUIForm(ProjectConst.GAME_PLAYING_UIFORM);
                //启动MVC命令
                Facade.Instance.SendNotification(ProjectConst.REG_STARTGAME_COMMAND);
                Facade.Instance.SendNotification(ProjectConst.MSG_INITMEDIATOR_FIELD);
            });
        }

	    void Start () {
			
		}
	
		
	}
}