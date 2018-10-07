/*
 *
 *		Title: "PureMVC" 客户端框架项目
 *			    主题: 数据代理类
 *		Description:
 *				功能: 属于 “模型层” 数据的操作
 *
 *		Date: 2018.5.7
 *		Version: 0.1
 *		Modify Recoder:
 *
 *
 */

using PureMVC.Patterns;
namespace PureMVCDemo
{
	public class DataProxy : Proxy
	{
        // 声明本类的名称，new 关键字是 覆盖 基类的定义
	    public new const string NAME = "DataProxy";
        // 引用实体类
	    private MyData _MyData = null;
        /// <summary>
        /// 构造函数
        /// </summary>
	    public DataProxy():base(NAME) // 将NAME传递给基类
	    {
	        _MyData =new MyData();
	    }

        /// <summary>
        /// 增加数量
        /// </summary>
        /// <param name="num">增加的数量</param>
	    public void AddLevel(int num)
        {
            _MyData.Number += num;
            //把变换的数据发送给 “视图层”
            SendNotification("Msg_AddLevel", _MyData);
        }

	}
}