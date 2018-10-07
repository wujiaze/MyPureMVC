/*
 *
 *		Title: "FlappyBirdByMVC" 项目
 *			    主题: 游戏开始 UI界面
 *		Description:
 *				功能:
 *
 *		Date: 2018.5.9
 *		Version: 0.1
 *		Modify Recoder:
 *
 *
 */

using System;
using SimpleUIFramework;
using UnityEngine;
namespace PureMVCdemo
{
	public class StartUIForm : BaseUIForm 
	{
	    void Awake()
	    {
            /* 窗体类型 */
	        CurrentUiType.UIForms_ShowMode = UIFormShowMode.HideOther;
	        CurrentUiType.IsClearStack = false;
	        CurrentUiType.UIForms_Type = UIFormsType.Normal;
            //按钮注册
	        RigisterButtonObjectEvent(ProjectConst.GAME_BTN_GUIDE, () => { OpenUIForm(ProjectConst.GAME_GUIDE_UIFORM); });
        }

	    void Start()
	    {
	        // 启动MVC框架
	        new ApplicationFacade();
	    }
	}
}