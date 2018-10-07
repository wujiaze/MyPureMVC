/*
 *
 *		Title: "AppByPureMVC" 应用项目
 *			    主题: 使用 PureMVC 架构 的 APP 示例应用项目
 *			    视图层 ：用户列表信息脚本
 *		Description:
 *				功能:
 *                  显示 所有 用户信息
 *		Date: 2018.5.13
 *		Version: 0.1
 *		Modify Recoder:
 *
 *
 */

using System;
using System.Collections.Generic;
using SimpleUIFramework;
using UnityEngine;
using UnityEngine.UI;

namespace AppByPureMVC
{
   
	public class UserList :MonoBehaviour
	{	
        // 系统委托
	    private Action _newUser;      // 新用户

	    private Action _deleteUser;   // 删除用户信息

	    public Action DeleteUser {
	        set { _deleteUser = value; }
	        get { return _deleteUser; }
	    }
	    public Action NewUser
	    {
	        set { _newUser = value; }
            get { return _newUser; }
	    }

	    // 用户列表(预设)
	    private UserList_Item _userInfoPrefab;
        // 用户列表的父对象
	    private Transform _userInfoParent;
	    // 用户列表信息集合
	    private List<UserList_Item> _userInfoList = new List<UserList_Item>();
        // 文本控件
        private Text _txtUsersCount;
        // 按钮控件       
	    private Button _btn_New;
        private Button _btn_Delete;

	    void Awake()
	    {
            
            // 加载预设
	        _userInfoPrefab = Resources.Load<GameObject>(Proconst.PATH_UIPREFABS + "/" + Proconst.PATH_TOGGLE)
	            .GetComponent<UserList_Item>();
            // 初始化父对象
	        _userInfoParent = UnityHelper.FindTheChildNode(this.gameObject, Proconst.GRID_INFO);
	        _txtUsersCount = UnityHelper.FindTheChildNode(this.gameObject, Proconst.TXT_USER_COUNT).GetComponent<Text>();
	        _btn_New = UnityHelper.FindTheChildNode(this.gameObject, Proconst.BTN_NEW_USER).GetComponent<Button>();
	        _btn_Delete = UnityHelper.FindTheChildNode(this.gameObject, Proconst.BTN_DELETE_USER).GetComponent<Button>();
	        // 初始化字段
	        _txtUsersCount.text = "0";
	        
            // 按钮事件
            _btn_New.onClick.AddListener(ClickBtn_New);
	        _btn_Delete.onClick.AddListener(ClickBtn_Delete);

            // 初始化按钮的显示
	        FreezeDeleteBtn();
	    }

        /// <summary>
        /// 加载并显示用户列表信息
        /// </summary>
        /// <param name="listUserInfo">需要显示的信息集合</param>
	    public void LoadAndShowUserListInfo(IList<UserData> listUserInfo)
	    {
            // 清空列表信息
	        ClearItems();
            // 实例化与显示列表信息
	        foreach (UserData userdata in listUserInfo)
	        {
	            UserList_Item tmp = InitUserInfo(); // 克隆预设体
                tmp.DisplayUserItem(userdata);      // 预设体显示信息
                _userInfoList.Add(tmp);
	        }
	        // 统计数量
	        _txtUsersCount.text = _userInfoList.Count.ToString();
        }

	    /// <summary>
	    /// 清空列表信息
	    /// </summary>
	    private void ClearItems()
	    {
	        if (_userInfoList != null)
	        {
	            foreach (UserList_Item item in _userInfoList)
	            {
	                Destroy(item.gameObject);
	            }
	            _userInfoList.Clear();
	        }
	    }

        /// <summary>
        /// 冻结 删除 按钮
        /// </summary>
        public void FreezeDeleteBtn()
	    {
	        _btn_Delete.interactable = false;

        }
        /// <summary>
        /// 显示 删除 按钮
        /// </summary>
	    public void ShowFreezeDeleteBtn()
        {
            _btn_Delete.interactable = true;

        }

	    /// <summary>
        /// 用户点击 “ 新用户” 按钮
        /// </summary>
	    private void ClickBtn_New() 
	    {
	        if (NewUser != null) NewUser();
        }
        /// <summary>
        /// 用户点击 “ 删除 ” 按钮
        /// </summary>
	    private void ClickBtn_Delete()
	    {
	        if (DeleteUser != null) DeleteUser();
        }

      

        /// <summary>
        /// 实例化用户信息 克隆信息的载体  
        /// </summary>
        /// <returns></returns>
	    private UserList_Item InitUserInfo() 
	    {
	        UserList_Item tmp = null;
	        if (_userInfoPrefab!=null)
	        {
                // 实例化
	            tmp = GameObject.Instantiate<UserList_Item>(_userInfoPrefab,Vector3.zero,Quaternion.identity, _userInfoParent); 
                tmp.transform.localScale =Vector3.one;
                tmp.gameObject.SetActive(true);
	            tmp.GetComponent<Toggle>().group = _userInfoParent.GetComponent<ToggleGroup>();
	        }
	        return tmp;
	    }
        
	}
}