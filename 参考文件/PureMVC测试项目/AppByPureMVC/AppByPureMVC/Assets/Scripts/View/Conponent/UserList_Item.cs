/*
 *
 *		Title: "AppByPureMVC" 应用项目
 *			    主题: 使用 PureMVC 架构 的 APP 示例应用项目
 *			        视图层 ： 代表用户列表信息（一条记录） 纯视图层 view
 *		Description:
 *				功能: 显示用户列表的一条记录
 *
 *		Date: 2018.5.13
 *		Version: 0.1
 *		Modify Recoder:
 *
 *      脚本和类 的区别
 *      脚本：继承MonoBehaviour
 *      类：不继承MonoBehaviour
 */

using System;
using PureMVC.Patterns;
using SimpleUIFramework;
using UnityEngine;
using UnityEngine.UI;

namespace AppByPureMVC
{
    public class UserList_Item :MonoBehaviour
	{
	    private Text _texUserName;
	    private Text _texGender;
	    private Text _texDepartment;
	    private Text _texTel;
	    private Text _texEmail;
	    private Toggle _ToggleUserItem;
        // 当前对象保存的数据实体信息
	    private UserData _currentData;
	    public UserData CurrentData
	    {
	        get { return _currentData; }
	    }

	    // 获取 Text 对象
        void Awake()
	    {
	        _texUserName = UnityHelper.GetTheChildNodeComopnetScripts<Text>(gameObject,Proconst.TXT_USERNAME);
	        _texGender = UnityHelper.GetTheChildNodeComopnetScripts<Text>(gameObject, Proconst.TXT_GENDER);
	        _texDepartment = UnityHelper.GetTheChildNodeComopnetScripts<Text>(gameObject, Proconst.TXT_DEPARTMENT);
	        _texTel = UnityHelper.GetTheChildNodeComopnetScripts<Text>(gameObject, Proconst.TXT_TELEPHONE);
	        _texEmail = UnityHelper.GetTheChildNodeComopnetScripts<Text>(gameObject, Proconst.TXT_EMAIL);
	        _ToggleUserItem = this.GetComponent<Toggle>();
            // toggle 事件
            _ToggleUserItem.onValueChanged.AddListener(OnValuesChangeByToggle);
        }

        private void OnValuesChangeByToggle(bool arg0)
        {
            if (arg0)
            {
                Facade.Instance.SendNotification(Proconst.MSG_SELECT_USERINFO_TO_USERLIST_MEDIATOR, this);
            }
            else
            {
                Facade.Instance.SendNotification(Proconst.MSG_DONT_SELECT, this);
            }
        }

        /// <summary>
        /// 显示用户数据项（一条记录）
        /// </summary>
        /// <param name="user"></param>
        public void DisplayUserItem(UserData user) 
	    {
	        if (user!=null)
	        {
	            _currentData = user; // 保存数据
                // UI显示
                if (_texUserName) _texUserName.text = user.UserName;
	            if (_texDepartment) _texDepartment.text = user.Department;
	            if (_texTel) _texTel.text = user.Telephone;
	            if (_texEmail) _texEmail.text = user.Email;
	            if (_texGender)
	            {
	                if (user.Gender)
	                {
	                    _texGender.text = "男"; 
	                }
	                else
	                {
	                    _texGender.text = "女";
	                }
	            }
            }
	    }




	}
}