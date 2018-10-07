/*
 *
 *		Title: "FlappyBirdByMVC" 项目
 *			    主题: 游戏进行中 UI界面
 *		Description:
 *				功能:
 *
 *		Date: 2018.5.9
 *		Version: 0.1
 *		Modify Recoder:
 *
 *
 */

using SimpleUIFramework;
using UnityEngine;
namespace PureMVCdemo
{
	public class GamePlayingUIForm : BaseUIForm 
	{
	    void Awake()
	    {
	        /* 窗体类型 */
	        CurrentUiType.UIForms_ShowMode = UIFormShowMode.HideOther;
	        CurrentUiType.IsClearStack = false;
	        CurrentUiType.UIForms_Type = UIFormsType.Normal;
        }
        
	}
}