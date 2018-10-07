/*  
 *     核心类： 视图层
 */
using System.Collections.Generic;
using PureMVC.Interfaces;
using PureMVC.Patterns;

namespace PureMVC.Core
{
    public class View : IView
    {
        /* 字段 */
        // 缓存每一消息的所有观察者
        protected IDictionary<string, IList<IObserver>> m_observerMap;
        // 缓存 IMediator 实例集合
        protected IDictionary<string, IMediator> m_mediatorMap;
        // 本类实例  volatile ：关键字，不使用Windows优化技术，即不存入缓存，保证了 m_instance 是最新的，不发生线程冲突
        protected static volatile IView m_instance;
        // 同步锁定对象  用于实例
        protected readonly object m_syncRoot = new object();
        // 静态同步锁定对象  用于静态
        protected static readonly object m_staticSyncRoot = new object();
        /* 属性 */
        // 得到 "线程安全"，"延迟加载"的单例
        public static IView Instance
        {
            get
            {
                if (m_instance == null)
                {
                    lock (m_staticSyncRoot)
                    {
                        if (m_instance == null) m_instance = new View();
                    }
                }
                return m_instance;
            }
        }
        /* 静态构造函数 */
        //作用 ：实现静态成员 延迟加载的功能
        static View(){}
        /* 构造函数 */
        protected View()
        {
            m_mediatorMap = new Dictionary<string, IMediator>();
            m_observerMap = new Dictionary<string, IList<IObserver>>();
            InitializeView();
        }
        // 初始化视图层 ：预留，用户自定义扩充
        protected virtual void InitializeView(){}


        #region Mediator核心视图层方法
        /// <summary>
        /// 注册 Mediator
        /// </summary>
        /// <param name="mediator"></param>
        public virtual void RegisterMediator(IMediator mediator)
        {
            lock (m_syncRoot)
            {
                // 不允许 Mediator 实现类，反复注册
                if (m_mediatorMap.ContainsKey(mediator.MediatorName)) return;
                //等价于 m_mediatorMap.Add(mediator.MediatorName, mediator);
                m_mediatorMap[mediator.MediatorName] = mediator;
                // 提取特定 Mediator 实现类中的 "消息集合"
                IList<string> interests = mediator.ListNotificationInterests();
                if (interests.Count > 0)
                {
                    // 封装 “执行体”
                    IObserver observer = new Observer("HandleNotification", mediator);

                    for (int i = 0; i < interests.Count; i++)
                    {
                        // 为每一条消息，注册一个执行体
                        RegisterObserver(interests[i].ToString(), observer);
                    }
                }
            }
            // 为用户自定义 保留方法
            mediator.OnRegister();
        }

        /// <summary>
        /// 查询 Mediator
        /// </summary>
        /// <param name="mediatorName"></param>
        /// <returns></returns>
        public virtual IMediator RetrieveMediator(string mediatorName)
        {
            lock (m_syncRoot)
            {
                if (!m_mediatorMap.ContainsKey(mediatorName)) return null;
                return m_mediatorMap[mediatorName];
            }
        }
        /// <summary>
        /// 移除 Mediator
        /// </summary>
        /// <param name="mediatorName"></param>
        /// <returns></returns>
        public virtual IMediator RemoveMediator(string mediatorName)
        {
            IMediator mediator = null;

            lock (m_syncRoot)
            {
                if (!m_mediatorMap.ContainsKey(mediatorName)) return null;
                mediator = (IMediator)m_mediatorMap[mediatorName];

                IList<string> interests = mediator.ListNotificationInterests();

                for (int i = 0; i < interests.Count; i++)
                {
                    RemoveObserver(interests[i], mediator);
                }

                m_mediatorMap.Remove(mediatorName);
            }
            // 即已经从集合中删除了引用，但是这个内存中的实例并没有删除，返回之后，还可以进行自定义操作（一般也不操作了，等待GC回收内存）
            if (mediator != null) mediator.OnRemove();
            return mediator;
        }
        /// <summary>
        /// 判断是否存在 Mediator
        /// </summary>
        /// <param name="mediatorName"></param>
        /// <returns></returns>
        public virtual bool HasMediator(string mediatorName)
        {
            lock (m_syncRoot)
            {
                return m_mediatorMap.ContainsKey(mediatorName);
            }
        }

        #endregion


        #region Observer消息中心  这个消息中心 是否可以单独提取出来，不要放在View层
        // 消息注册（存储消息）
        // 同一个消息，同一个观察者可以多次注册（程序未设置分辨，就相当于一个消息有多个观察者，只不过这个观察者是同一个对象），这样执行消息通知时，同一个观察者会执行多次
        // 同理，删除也要删除多次，这就提醒我们不要多注册相同的情况
        public virtual void RegisterObserver(string notificationName, IObserver observer)
 		{
			lock (m_syncRoot)
			{
				if (!m_observerMap.ContainsKey(notificationName))
				{
					m_observerMap[notificationName] = new List<IObserver>();
				}
				m_observerMap[notificationName].Add(observer);
			}
		}
        // 执行消息
        // 命令消息和通知消息都是同一个方法 发送过来的
		public virtual void NotifyObservers(INotification notification) 
		{
			IList<IObserver> observers = null;
			lock (m_syncRoot)
			{
				if (m_observerMap.ContainsKey(notification.Name))
				{
					IList<IObserver> observers_ref = m_observerMap[notification.Name];
                    //目的：当有多个线程或快速执行时，新开辟一块内存，各自处理，提高效率（开辟区域比循环操作快）
                    observers = new List<IObserver>(observers_ref); 
				}
			}

			if (observers != null)
			{
				for (int i = 0; i < observers.Count; i++)
				{
					IObserver observer = observers[i];
					observer.NotifyObserver(notification);
				}
			}
		}
        // 删除消息
		public virtual void RemoveObserver(string notificationName, object notifyContext)
		{
			lock (m_syncRoot)
			{
				if (m_observerMap.ContainsKey(notificationName))
				{
					IList<IObserver> observers = m_observerMap[notificationName];
					for (int i = 0; i < observers.Count; i++)
					{
						if (observers[i].CompareNotifyContext(notifyContext))
						{
							observers.RemoveAt(i);
							break;
						}
					}
                    
					if (observers.Count == 0)
					{
						m_observerMap.Remove(notificationName);
					}
				}
			}
		}
        #endregion






	}
}
