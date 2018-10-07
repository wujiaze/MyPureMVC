/*
 *
 *		Title: "AppByPureMVC" 应用项目
 *			    主题: 使用 PureMVC 架构 的 APP 示例应用项目
 *			    视图层：用户信息窗口
 *		Description:
 *				功能: 显示/更新 用户的单条信息
 *
 *		Date: 2018.5.13
 *		Version: 0.1
 *		Modify Recoder:
 *
 *
 */

using System;
using SimpleUIFramework;
using UnityEngine;
using UnityEngine.UI;


namespace AppByPureMVC
{
	public class UserForm:MonoBehaviour
	{
	    private Action _confirmBtnHandler; // 将按钮点击事件的处理方法，委托给其他类来处理
	    public Action ConfirmBtnHandler
	    {
	        get { return _confirmBtnHandler; }
	        set { _confirmBtnHandler = value; }
	    }
        // 窗体控件
        private InputField _inp_FirstName;
	    private InputField _inp_LastName;
	    private Toggle _tog_Male;
	    private Toggle _tog_Female;
	    private InputField _inp_Department;
	    private InputField _inp_Telphone;
	    private InputField _inp_Email;
	    private Button _btnConfirm;

	    // 用户实体类
	    private UserData _userData;
	    public UserData UserData { get { return _userData; } }

	   


	    void Awake()
	    {
	        _inp_FirstName = UnityHelper.GetTheChildNodeComopnetScripts<InputField>(gameObject,Proconst.INP_FIRSTNAME);
	        _inp_LastName = UnityHelper.GetTheChildNodeComopnetScripts<InputField>(gameObject, Proconst.INP_LASTNAME);
	        _inp_Department = UnityHelper.GetTheChildNodeComopnetScripts<InputField>(gameObject, Proconst.INP_DEPARTMENT);
	        _inp_Telphone = UnityHelper.GetTheChildNodeComopnetScripts<InputField>(gameObject, Proconst.INP_TELPHONE);
	        _inp_Email = UnityHelper.GetTheChildNodeComopnetScripts<InputField>(gameObject, Proconst.INP_EMAIL);
	        _tog_Male = UnityHelper.GetTheChildNodeComopnetScripts<Toggle>(gameObject, Proconst.TOG_MALE);
	        _tog_Female = UnityHelper.GetTheChildNodeComopnetScripts<Toggle>(gameObject, Proconst.TOG_FEMALE);
	        _btnConfirm = UnityHelper.GetTheChildNodeComopnetScripts<Button>(gameObject, Proconst.BTN_CONFIRM);
            // 按钮事件
	        _btnConfirm.onClick.AddListener(ClickConfirm);
            // 输入事件
	        _inp_FirstName.onValueChanged.AddListener(OnvalueChange);

            FreezeConfirmBtn();

        }


	    /// <summary>
        /// 显示用户窗体信息
        /// </summary>
        /// <param name="type"></param>
        /// <param name="userData"></param>
        public void ShowUserInfo(UserFormType type, UserData userData=null)
	    {
            switch (type)
            {
                case UserFormType.Create:
                    ClearUserInfo();
                    break;
                case UserFormType.Update:
                    ShowUserInfo(userData);
                    break;
                case UserFormType.Remove:
                    ClearUserInfo();
                    break;
                default:
                    break;
            }
        }
	    /// <summary>
	    /// 清空 用户窗体 信息
	    /// </summary>
	    public void ClearUserInfo()
	    {
	        _userData = null;
            _inp_FirstName.text = null;
	        _inp_LastName.text = null;
	        _inp_Department.text = null;
	        _inp_Telphone.text = null;
	        _inp_Email.text = null;
	        _tog_Male.isOn = true;
	        FreezeConfirmBtn();
	    }
        /// <summary>
        /// 显示用户窗体信息
        /// </summary>
        /// <param name="userData"></param>
        private void ShowUserInfo(UserData userData)
	    {
	        if (userData!=null)
	        {
	            _userData = userData;
                // UI显示
                _inp_FirstName.text = userData.FirstName;
	            _inp_LastName.text = userData.LastName;
	            _inp_Department.text = userData.Department;
	            _inp_Telphone.text = userData.Telephone;
	            _inp_Email.text = userData.Email;
	            if (userData.Gender == true)
	                _tog_Male.isOn = true;
	            if (userData.Gender == false)
	                _tog_Female.isOn = true;
	        }
	    }

	    /// <summary>
	    /// 确认按钮点击事件
	    /// </summary>
	    private void ClickConfirm()
	    {
	        // 窗体数据检查
	        if (!CheckUserForm()) return;
	        // 调用委托
	        if (_confirmBtnHandler != null)
	        {
                _confirmBtnHandler.Invoke();
	        }
	    }

        /// <summary>
        /// 检查参数的合法性
        /// </summary>
        /// <returns></returns>
	    private bool CheckUserForm()
	    {
	        //if (_userData == null)
	         _userData = new UserData();
            _userData.FirstName = _inp_FirstName.text;
	        _userData.LastName = _inp_LastName.text;
	        _userData.Department = _inp_Department.text;
	        _userData.Telephone = _inp_Telphone.text;
	        _userData.Email = _inp_Email.text;
	        if (_tog_Male.isOn)
	            _userData.Gender = true;
            if (_tog_Female.isOn)
	            _userData.Gender = false;
	        return _userData.IsVaild;
	    }
        /// <summary>
        /// 冻结 确认 按钮
        /// </summary>
	    public void FreezeConfirmBtn()
	    {
	        _btnConfirm.interactable = false;

	    }
        /// <summary>
        /// 显示 确认 按钮
        /// </summary>
        private void ShowFreezeConfirmBtn()
	    {
	        _btnConfirm.interactable = true;
	    }

	    private void OnvalueChange(string str)
	    {
	        if (!string.IsNullOrEmpty(str))
	        {
	            ShowFreezeConfirmBtn();
	        }
	    }
	}
}