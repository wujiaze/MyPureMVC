/*
 *
 *		Title: "AppByPureMVC" 应用项目
 *			    主题: 使用 PureMVC 架构 的 APP 示例应用项目
 *			    启动 PureMVC 框架
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
using PureMVC.Patterns;
using SimpleUIFramework;
using UnityEngine;

namespace AppByPureMVC
{
    
	public class GameStart :MonoBehaviour
	{
	    private UserEmployeeInfo userEmpInfo; 
        private void Awake()
        {
            Log.Write(GetType()+ "/ Awake() ");
            // 启动框架 注册了所有的代理类，消息
            ApplicationFacade appFacade =  ApplicationFacade.Instance as ApplicationFacade;


            // 动态添加脚本组件
            AddGameObjectScripts();
            if (appFacade != null && userEmpInfo != null)
            {
                //启动项目 控制层内部 消息传递
                appFacade.SendNotification(Proconst.COM_INITMEADIATOR, userEmpInfo);
            }
        }

	    // 动态添加脚本组件
	    private void AddGameObjectScripts()
	    {
	        GameObject EmpRoot = GameObject.Find(Proconst.EMP_ROOT);
	        userEmpInfo = EmpRoot.AddComponent<UserEmployeeInfo>();
	    }


    }
}