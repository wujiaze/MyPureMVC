/*
 *      观察者类
 */
using System;
using System.Reflection;
using PureMVC.Interfaces;
using UnityEngine;

namespace PureMVC.Patterns
{
	public class Observer : IObserver
	{
	    /// <summary>
	    /// 消息方法的名称
	    /// </summary>
	    private string m_notifyMethod;

	    /// <summary>
	    /// 消息的上下文 一般是类型的实例
	    /// </summary>
	    private object m_notifyContext;

        // 线程锁
	    protected readonly object m_syncRoot = new object();

        // 用 virtual 是为了留给用户进行二次开发 
        /* 属性 */
	    public virtual string NotifyMethod
	    {
	        private get
	        {
	            return m_notifyMethod;
	        }
	        set
	        {
	            m_notifyMethod = value;
	        }
	    }
        /// <summary>
        /// 消息的上下文 一般是类型的实例
        /// </summary>
	    public virtual object NotifyContext
	    {
	        private get
	        {
	            return m_notifyContext;
	        }
	        set
	        {
	            m_notifyContext = value;
	        }
	    }

        /* 构造函数 */
	    public Observer(string notifyMethod, object notifyContext)
	    {
	        m_notifyMethod = notifyMethod;
	        m_notifyContext = notifyContext;
	    }

        /// <summary>
        /// 通知观察者
        /// 作用 ：对指定的对象(观察者)，调用它的指定的方法
        ///  使用SendNotification 发送的消息 ，最终都是通过本方法调用指定的方法
        ///     对消息响应的统一封装
        /// </summary>
        /// <param name="notification"></param>
        public virtual void NotifyObserver(INotification notification)
		{
			object context;
			string method;

			lock (m_syncRoot)
			{
				context = NotifyContext;
				method = NotifyMethod;
			}
            // 实例的类型
			Type t = context.GetType();
            // 获取的方法的条件 ： 实例方法 公共的 忽略大小写
			BindingFlags f = BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase;
            // 获取的方法 method :方法的名字 
            MethodInfo mi = t.GetMethod(method, f);
            // context 表示 调用方法的实例   new object[] { notification } 表示调用的方法所需的参数,尽管这里只需要一个参数
            mi.Invoke(context, new object[] { notification }); 
            
		}

		
        // 判断上下文（也可以理解为传入的对象和当前缓存的对象）是否一致
		public virtual bool CompareNotifyContext(object obj)
		{
			lock (m_syncRoot)
			{
				return NotifyContext.Equals(obj);
			}
		}


	}
}
