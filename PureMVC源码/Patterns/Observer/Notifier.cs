/*  
 *      通知辅助类
 *      存在 SendNotification 的三种重载方法，本质是 引用Facade类中的 SendNotification 方法
 */
using PureMVC.Interfaces;

namespace PureMVC.Patterns
{
    public class Notifier : INotifier
    {
        /* 字段 */
        private IFacade m_facade = PureMVC.Patterns.Facade.Instance;
        /* 属性 */
        protected IFacade Facade { get { return m_facade; }}
        /* 方法 */
        public virtual void SendNotification(string notificationName) 
		{
			m_facade.SendNotification(notificationName);
		}

		public virtual void SendNotification(string notificationName, object body)
		{
			m_facade.SendNotification(notificationName, body);
		}

		public virtual void SendNotification(string notificationName, object body, string type)
		{
            m_facade.SendNotification(notificationName, body, type);
		}
	}
}
