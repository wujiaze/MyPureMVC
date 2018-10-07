/*
 *
 *		Title: "PureMVC" 客户端框架项目
 *			    主题: PureMVC 项目全局控制类
 *		Description:
 *				功能: 
 *
 *		Date: 2018.5.7
 *		Version: 0.1
 *		Modify Recoder:
 *
 *
 */

using PureMVC.Patterns;
using UnityEngine;

namespace PureMVCDemo
{
	public class ApplicationFacade : Facade
	{
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="goRootNode">UI界面的根节点</param>
	    public ApplicationFacade(GameObject goRootNode)
	    {
	        /*MVC 三层的关联绑定*/
            // 控制层注册 (命令消息与控制层类的对应关系，建立绑定)
            RegisterCommand("Reg_StartDataCommand",typeof(DataCommand));
            // 视图层注册
            RegisterMediator(new DataMediator(goRootNode));
            // 模型层册数
            RegisterProxy(new DataProxy());
	    }
	}
}