/*
 *
 *		Title: "AppByPureMVC" 应用项目
 *			    主题: 使用 PureMVC 架构 的 APP 示例应用项目
 *		Description:
 *				功能: 模型层  数据实体类 （VO 数值对象）
 *
 *		Date: 2018.5.13
 *		Version: 0.1
 *		Modify Recoder:
 *        
 *
 */

using UnityEngine;

namespace AppByPureMVC
{
	public class UserData 
	{	
        // 字段
	    private string _firstName;  // 姓
	    private string _lastName;   // 名
	    private bool _gender;       //性别
	    private string _department; //部门
	    private string _telephone;  //联系方式
	    private string _email;      //邮箱

        public string UserName
	    {
	        get { return _firstName + _lastName; }
	    }
	    public string FirstName
	    {
	        get { return _firstName; }
	        set { _firstName = value; }
	    }

	    public string LastName
	    {
	        get { return _lastName; }
	        set { _lastName = value; }
	    }

	    public bool Gender
	    {
	        get { return _gender; }
	        set { _gender = value; }
	    }

	    public string Department
	    {
	        get { return _department; }
	        set { _department = value; }
	    }

	    public string Telephone
	    {
	        get { return _telephone; }
	        set { _telephone = value; }
	    }

	    public string Email
	    {
	        get { return _email; }
	        set { _email = value; }
	    }


        public UserData()
	    {
            
	    }

	    public UserData(string firstName,string lastName,bool gender,string department,string telephone,string email)
	    {
	        if (!string.IsNullOrEmpty(firstName)) _firstName = firstName;
	        if (!string.IsNullOrEmpty(lastName)) _lastName = lastName;
            if (!string.IsNullOrEmpty(department)) _department = department;
            if (!string.IsNullOrEmpty(telephone)) _telephone = telephone;
            if (!string.IsNullOrEmpty(email)) _email = email;
	        _gender = gender;
        }

        //信息有效性 :可以根据具体业务需求改变
        public bool IsVaild
	    {
	        get { return !string.IsNullOrEmpty(_firstName) && !string.IsNullOrEmpty(_lastName) && !string.IsNullOrEmpty(_telephone);  }
	    }


        /// <summary>
        /// 对象比较
        /// 因为对象的其他信息可以更新，就名字一般不太会更新，所以就将名字作为参考
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            bool boolResult = false;
            UserData otherData = null;
            otherData = obj as UserData;
            if (otherData != null)
            {
                if (otherData.UserName == this.UserName)
                {
                    boolResult = true;
                }
            }
            return boolResult;
        }


    }
}