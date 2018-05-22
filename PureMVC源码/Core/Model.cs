/*
 *      核心层： 模型类
 *      Model类中是没有处理发来的消息的定义，即不处理消息，这也就是Proxy 只能被 “方法调用” 的原因
 */
using System.Collections.Generic;
using PureMVC.Interfaces;

namespace PureMVC.Core
{ 
    public class Model : IModel
    {
        /* 字段 */
        // 本类实例  volatile ：关键字，不使用Windows优化技术，即不存入缓存，保证了 m_instance 是最新的，不发生线程冲突
        protected static volatile IModel m_instance;
        // 代理集合类
        protected IDictionary<string, IProxy> m_proxyMap;
        // 同步锁定对象  用于实例
        protected readonly object m_syncRoot = new object(); 
        // 静态同步锁定对象  用于静态
        protected static readonly object m_staticSyncRoot = new object(); 

        /* 属性 */
        // 得到 "线程安全"，"延迟加载"的单例
        public static IModel Instance
        {
            get
            {
                if (m_instance == null)
                {
                    lock (m_staticSyncRoot)
                    {
                        if (m_instance == null)
                            m_instance = new Model(); // 由于有静态构造函数，这个静态单例，只有在调用静态成员时，才加载
                    }
                }
                return m_instance;
            }
        }

        /* 静态构造函数 */
        //作用 ：实现静态成员 延迟加载的功能
        static Model() { } 

        /* 构造函数 */
        protected Model()
		{
			m_proxyMap = new Dictionary<string, IProxy>();
			InitializeModel();
		}

        // 初始化模型层 ：预留，用户自定义扩充
        protected virtual void InitializeModel() { }




        /* 方法 */
        // 注册 Proxy
        public virtual void RegisterProxy(IProxy proxy)
		{
			lock (m_syncRoot)    
			{
				m_proxyMap[proxy.ProxyName] = proxy; // 这种方法也能添加不存在的键值对，还能修改已存在的键值对
			}
			proxy.OnRegister();
		}
        // 查询 Proxy
        public virtual IProxy RetrieveProxy(string proxyName)
		{
			lock (m_syncRoot)
			{
				if (!m_proxyMap.ContainsKey(proxyName)) return null;
				return m_proxyMap[proxyName];
			}
		}
       
        // 移除 Proxy
        public virtual IProxy RemoveProxy(string proxyName)
		{
			IProxy proxy = null;

			lock (m_syncRoot)
			{
				if (m_proxyMap.ContainsKey(proxyName))
				{
					proxy = RetrieveProxy(proxyName);
					m_proxyMap.Remove(proxyName);
				}
			}
            // 已经从集合中删除了引用，，但是这个内存中的实例并没有删除，返回之后，还可以进行自定义操作（一般也不操作了，等待GC回收内存）
            if (proxy != null)
                proxy.OnRemove();
			return proxy;
		}

        // 是否存在指定的代理类 Proxy
        public virtual bool HasProxy(string proxyName)
        {
            lock (m_syncRoot)
            {
                return m_proxyMap.ContainsKey(proxyName);
            }
        }



    }
}
