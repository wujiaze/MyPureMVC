/* 
 *  消息封装类
 *  作用： 作为一种消息的载体
*/
using PureMVC.Interfaces;

namespace PureMVC.Patterns
{
    public class Notification : INotification
	{
        /* 字段 */
	    /// <summary>
	    /// 消息的名称
	    /// </summary>
	    private string m_name;
	    /// <summary>
	    /// 消息的内容
	    /// </summary>
	    private object m_body;
        /// <summary>
        /// 消息的类型
        /// </summary>
        private string m_type;


        /* 将字段封装成属性 */
        public virtual string Name
        {
            get { return m_name; }
        }
        public virtual object Body
	    {
	        get { return m_body; }
	        set { m_body = value; }
	    }
        public virtual string Type
	    {
	        get { return m_type; }
	        set { m_type = value; }
	    }


        /* 构造函数 */
        public Notification(string name): this(name, null, null){}
        // 这种用的比较多
        public Notification(string name, object body): this(name, body, null){}
        // 类型 用的比较少
        public Notification(string name, object body, string type)
		{
			m_name = name;
			m_body = body;
			m_type = type;
		}


        /* 方法 */
        // 重写Tostring，只是为了看的更明确一些
		public override string ToString()
		{
			string msg = "Notification Name: " + Name;
			msg += "\nBody:" + ((Body == null) ? "null" : Body.ToString());
			msg += "\nType:" + ((Type == null) ? "null" : Type);
			return msg;
		}

	}
}
