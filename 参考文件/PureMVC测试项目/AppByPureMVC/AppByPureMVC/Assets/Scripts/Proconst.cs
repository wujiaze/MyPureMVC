/*
 *
 *		Title: "AppByPureMVC" 应用项目
 *			    主题: 使用 PureMVC 架构 的 APP 示例应用项目
 *		Description:
 *				功能:
 *                  定义项目中所有的常量
 *		Date: 2018.5.13
 *		Version: 0.1
 *		Modify Recoder:
 *
 *
 */
namespace AppByPureMVC
{
	public class Proconst 
	{
        /* 路径常量 */
        public const string PATH_UIPREFABS = "UIPrefabs";
	    public const string PATH_TOGGLE = "Toggle";

        /* UI对象 */
        // Toggle
        public const string TXT_USERNAME = "Txt_UserName";
	    public const string TXT_GENDER = "Txt_Gender";
	    public const string TXT_DEPARTMENT = "Txt_Department";
	    public const string TXT_TELEPHONE = "Txt_Tel";
	    public const string TXT_EMAIL = "Txt_Email";
        // 整体
	    public const string EMP_ROOT = "EmployeeInfo";
	    public const string EMP_LISTINFO = "EmpListInfo";
	    public const string EMP_FORMINFO = "EmpFormInfo";
	    public const string GRID_INFO = "GridInfo";
        // EmpListInfo
        public const string TXT_USER_COUNT = "TxtUserCount";
	    public const string BTN_NEW_USER = "BtnNewUser";
	    public const string BTN_DELETE_USER = "BtnDeleteUser";
        // EmpFormInfo
	    public const string INP_FIRSTNAME = "InpFirstname";
	    public const string INP_LASTNAME = "InpLastName";
	    public const string INP_DEPARTMENT = "InpDepartment";
	    public const string INP_TELPHONE = "InpTel";
	    public const string INP_EMAIL = "InpEmail";
	    public const string TOG_MALE = "Toggle_male";
	    public const string TOG_FEMALE = "Toggle_female";
	    public const string BTN_CONFIRM = "BtnConfirm";

       
        /* PureMVC 命令消息 */
        /* 视图层 > 控制层 */
        /* 控制层内部 */
        public const string COM_INITMEADIATOR = "Com_InitMediator";

        /* PureMVC 通知消息 */
        /* 模型层 > 视图层 */

        /* 控制层 > 视图层 低频率*/
        public const string MSG_INIT_USERLIST_MEDIATOR = "Msg_InitUserListMediator";
	    public const string MSG_INIT_USERFORM_MEDIATOR = "Msg_InitUserFormMediator";

        /* 视图层内部 */
        // FormMediator 发送给 ListMediator
        public const string MSG_ADDNEWUSERIBFO_TOUSERLIST_MEDIATOR = "Msg_AddNewUserInfoToUserListMediator";
	    public const string MSG_UPDATEUSERINFO_TOUSERLIST_MEDIATOR = "Msg_UpdateUserInfoToUserListMediator";
        // UserListMediator 发送给 UserFormMediator
	    public const string MSG_ADDNEWUSERINFO_TOUSERFORM_MEDIATOR = "Msg_AddNewUserinfoToUserFormMediator"; // 增加用户信息 
	    public const string MSG_REMOVEUSERINFO_TOUSERFORM_MEDIATOR = "Msg_RemoveUserinfoToUserFormMediator"; // 删除用户信息 
	    public const string MSG_SELECT_USERINFO_TO_USERFORM_MEDIATOR = "Msg_SelUserInfoToUserFormMediator";// 发送 用户选择的信息 到 用户窗体Mediator类   
        // UserList_Item 脚本 发送给 UserListMediator
        public const string MSG_SELECT_USERINFO_TO_USERLIST_MEDIATOR = "Msg_SendUserInfoToUserlistMediator"; // 发送 用户选择的信息 到 列表信息Mediator类
        // 自己测试
        public const string MSG_DONT_SELECT = "SSSS";

	    public const string MSG_SDDD = "ss";
	}
}