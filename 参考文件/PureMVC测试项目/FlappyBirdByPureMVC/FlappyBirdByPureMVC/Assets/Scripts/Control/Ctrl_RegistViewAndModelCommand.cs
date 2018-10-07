/*
 *
 *		Title: "FlappyBirdByMVC" 项目
 *			    主题: 命令层（控制层的变换）注册模型与视图层
 *		Description:
 *				功能:
 *                  编程第3 步
 *		Date: 2018.5.9
 *		Version: 0.1
 *		Modify Recoder:
 *
 *
 */

using PureMVC.Interfaces;
using PureMVC.Patterns;
using UnityEngine;
namespace PureMVCdemo
{
	public class Ctrl_RegistViewAndModelCommand : SimpleCommand 
	{
	    public override void Execute(INotification notification)
	    {
	        //// 先注册 模型层
	        //Facade.RegisterProxy(new Model_GameDataProxy());
	        //// 再注册 视图层
	        //Facade.RegisterMediator(new View_GamePlayingMediator());
        }
	}
}