/*
 *     控制层
 */
using System;
using System.Collections.Generic;
using PureMVC.Interfaces;
using PureMVC.Patterns;

namespace PureMVC.Core
{
    public class Controller : IController
	{
        /* 字段 */
        // 本类单例
	    protected static volatile IController m_instance;
        // IView 的引用 ：因为消息中心 放在了View 层
        protected IView m_view;
        // 命令名称 和 命令类 的集合
	    protected IDictionary<string, Type> m_commandMap;
        // 同步锁定对象
	    protected readonly object m_syncRoot = new object();
        // 静态同步锁定对象
	    protected static readonly object m_staticSyncRoot = new object();
        /* 属性 */
	    public static IController Instance
	    {
	        get
	        {
	            if (m_instance == null)
	            {
	                lock (m_staticSyncRoot)
	                {
	                    if (m_instance == null) m_instance = new Controller();
	                }
	            }
	            return m_instance;
	        }
	    }
        /* 静态构造函数 */
	    static Controller(){}
        /* 构造函数 */
        protected Controller()
	    {
	        m_commandMap = new Dictionary<string, Type>();
	        InitializeController();
	    }
	    // 初始化本类
	    protected virtual void InitializeController()
	    {
	        m_view = View.Instance;
	    }


        // 注册命令:从结构中可知，同一个命令对应不同类型时，后一个会覆盖前一个，若前一个还未执行，则再也不会执行了。
        // 所以，一般一个命令对应一个类型（除非能准确判断何时启用这个命令）。当然一个类型可以对应多个命令
        public virtual void RegisterCommand(string notificationName, Type commandType) 
		{
			lock (m_syncRoot)
			{
				if (!m_commandMap.ContainsKey(notificationName))
				{
                    // 给消息 注册 观察者
					m_view.RegisterObserver(notificationName, new Observer("ExecuteCommand", this)); 
                }
				m_commandMap[notificationName] = commandType; 
			}
		}

        // 执行命令 
        // 在 Observer 的 NotifyObserver 方法中，通过反射执行
        public virtual void ExecuteCommand(INotification note) 
	    {
	        Type commandType = null;

	        lock (m_syncRoot)
	        {
	            if (!m_commandMap.ContainsKey(note.Name)) return;
	            commandType = m_commandMap[note.Name];
	        }
            // 从这里的局部变量可知 ： 命令的实例在执行完当前命令后，就销毁了
	        object commandInstance = Activator.CreateInstance(commandType);// 创建类的实例
            // is 运算符 用来判断 是否可以执行类型转换操作 ，避免盲目转换
	        if (commandInstance is ICommand) 
	        {
	            ((ICommand)commandInstance).Execute(note);
	        }
	    }

        // 移除命令
        public virtual void RemoveCommand(string notificationName)
	    {
	        lock (m_syncRoot)
	        {
	            if (m_commandMap.ContainsKey(notificationName))
	            {
	                m_view.RemoveObserver(notificationName, this);
	                m_commandMap.Remove(notificationName);
	            }
	        }
	    }

        // 是否存在命令
        public virtual bool HasCommand(string notificationName)
		{
			lock (m_syncRoot)
			{
				return m_commandMap.ContainsKey(notificationName);
			}
		}
		
       
	}
}
