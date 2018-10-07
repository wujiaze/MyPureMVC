/*
 *
 *		Title: "AppByPureMVC" 应用项目
 *			    主题: 使用 PureMVC 架构 的 APP 示例应用项目
 *			         数据代理类
 *		Description:
 *				功能: 针对数据实体类，进行各种操作。
 *
 *		Date: 2018.5.13
 *		Version: 0.1
 *		Modify Recoder:
 *
 *
 */

using System.Collections.Generic;
using PureMVC.Patterns;
using SimpleUIFramework;
using UnityEngine;

namespace AppByPureMVC
{
	public class UserProxy :Proxy
	{	
        // 类名称 -- MVC 框架定义
	    public new const string NAME = "UserProxy";
        // 只读属性
	    public IList<UserData> ListUsers
	    {
	        get { return base.Data as IList<UserData>; }
	    }

	    public UserProxy():base(NAME,new List<UserData>()) 
        {
            Log.Write(GetType() + "/ UserProxy 构造 ");
	        AddUserItem(new UserData("吴","佳泽",true,"技术部","15888084993","1051277749@qq.com")); 
            AddUserItem(new UserData("吴", "佳ling", false, "技术部", "15888084993", "1051277749@qq.com")); 
        }


        //判断逻辑在控制层做 模型层 不做逻辑判断
        /// <summary>
        /// 增加用户数据  
        /// </summary>
        /// <param name="user"></param>
	    public void AddUserItem(UserData user) 
	    {
	        if (user != null)
	        {
	            ListUsers.Add(user);
            }
	    }


        /// <summary>
        /// 更新用户数据
        /// </summary>
        /// <param name="user"></param>
        public void UpdateUserItem(UserData user)
	    {
            Debug.Log(user.UserName);
	        for (int i = 0; i < ListUsers.Count; i++)
	        {
	            Debug.Log(ListUsers[i].UserName);
                if (ListUsers[i].Equals(user))
	            {
	                ListUsers[i] = user;
                    break;
	            }
            }
	    }

        /// <summary>
        /// 删除用户数据
        /// </summary>
        /// <param name="user"></param>
        public void DeleteUserItem(UserData user)
	    {
	        for (int i = 0; i < ListUsers.Count; i++)
	        {
	            if (ListUsers[i].Equals(user))
	            {
	                ListUsers.RemoveAt(i);
                    break;
	            }
	        }
        }

	}
}