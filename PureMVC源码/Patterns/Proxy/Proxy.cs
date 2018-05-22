/*
 *      代理类
 */
using PureMVC.Interfaces;
namespace PureMVC.Patterns
{
    public class Proxy : Notifier, IProxy
    {
        /* 字段 */
        public static string NAME = "Proxy";
        protected string m_proxyName;
        protected object m_data;
        /* 属性 */
        public virtual string ProxyName
        {
            get { return m_proxyName; }
        }
        public virtual object Data
        {
            get { return m_data; }
            set { m_data = value; }
        }
        /* 构造函数 */
        public Proxy() : this(NAME, null){}

        public Proxy(string proxyName): this(proxyName, null) {}

		public Proxy(string proxyName, object data)
		{
			m_proxyName = (proxyName != null) ? proxyName : NAME;
			if (data != null) m_data = data;
		}

        /* 方法 */
        // 下面两个是提供给用户自定义的方法，一个是在注册时调用，一个在移除时调用
		public virtual void OnRegister(){}
		public virtual void OnRemove(){}

	}
}
