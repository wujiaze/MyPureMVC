/*
 *
 *		Title: "AppByPureMVC" 应用项目
 *			    主题: 使用 PureMVC 架构 的 APP 示例应用项目
 *			    PureMVC 框架入口程序
 *		Description:
 *				功能:
 *
 *		Date: 2018.5.13
 *		Version: 0.1
 *		Modify Recoder:
 *
 *
 */

using PureMVC.Interfaces;
using PureMVC.Patterns;
using SimpleUIFramework;
using UnityEngine;

namespace AppByPureMVC
{
	public class ApplicationFacade :Facade
	{

	    protected ApplicationFacade():base()
	    {
            Log.Write(GetType() + "/ ApplicationFacade() ");
        }
        /// <summary>
        /// PureMVC 的单例引用
        /// 单例 且 线程安全，父类中有静态构造函数，则实现了静态成员 “延迟加载” 功能
        /// </summary>
	    public new static IFacade Instance // 为什么使用接口，而不是类名  答:便于扩展
	    {
	        get
	        {
	            if (m_instance ==null)  // 这个是来判断 是否实例化过
	            {
	                lock (m_staticSyncRoot) 
	                {
	                    if (m_instance == null) // 这个是在锁住之后，来判断是否是null ，是null就实例化，就怕锁住的时候已经有值了
	                    {
	                        m_instance =new ApplicationFacade(); // 在类的内部实例化
                        }
	                }
	            }
	            return m_instance;
	        }
	    }




        /// <summary>
        /// 注册 模型层 实例
        /// 模型层是长期存在的，除非调用删除模型方法，并没有创建实例，（这很正常，因为模型本身就应该是单个实例）
        /// </summary>
        protected override void InitializeModel()
	    {
	        base.InitializeModel();
            RegisterProxy(new UserProxy());
	    }
        /// <summary>
        /// 注册 视图层实例
        /// 视图层是长期存在的，除非调用删除视图方法，并没有创建实例，（这很正常，因为视图层本身就应该是单个实例）
        /// </summary>
        protected override void InitializeView()
	    {
	        base.InitializeView();
	        RegisterMediator(new UserListMediator());
            RegisterMediator(new UserFormMediator());
	    }
        /// <summary>
        /// 注册 控制层 实例
        /// 生命周期：命令是长期存在的，除非调用删除命令方法，但是命令每执行一次，就会创建一次命令的实例，这个实例在执行完命令就销毁了
        /// </summary>
        protected override void InitializeController()
	    {
	        base.InitializeController();
            RegisterCommand(Proconst.COM_INITMEADIATOR,typeof(StartUpApplication_Command));
        }
        
	}
}
   