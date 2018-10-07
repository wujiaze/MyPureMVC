/*
 *
 *		Title: "PureMVC" 客户端框架项目
 *			    主题: 数据控制层
 *		Description:
 *				功能: 属于“控制层” ，接受玩家的输入（或者视图层发来的输入消息），进行处理
 *
 *		Date: 2018.5.7
 *		Version: 0.1
 *		Modify Recoder:
 *
 *
 */

using PureMVC.Interfaces;
using PureMVC.Patterns;
namespace PureMVCDemo
{
	public class DataCommand :  SimpleCommand
	{
	    public override void Execute(INotification notification)
	    {
            // 调用数据层的“增加等级”的方法
	        DataProxy datapro = (DataProxy) Facade.RetrieveProxy(DataProxy.NAME); 
            datapro.AddLevel(10);
	    }
	}
}