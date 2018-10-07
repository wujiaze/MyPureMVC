/*
 *
 *		Title: "SimpleUIFramework" UI框架项目
 *			    主题: 框架核心参数
 *		Description:
 *				功能:
 *              1、系统常量
 *              2、全局性方法
 *              3、系统枚举类型
 *              4、委托定义
 *		Date: 2018.4.25
 *		Version: 0.1
 *		Modify Recoder:
 *
 *
 */
using UnityEngine;
namespace SimpleUIFramework
{
    #region 系统枚举类型
    /// <summary>
    /// UI窗体(位置)类型
    /// </summary>
    public enum UIFormsType
    {
        Normal, // 普通窗体
        Fixed,  // 固定窗体
        PopUp   // 弹出窗体
    }
    /// <summary>
    /// UI窗体的显示类型
    /// </summary>
    public enum UIFormShowMode
    {
        Normal,         //普通
        ReverseChange,  //反向切换
        HideOther       //隐藏其他
    }
    /// <summary>
    /// UI窗体透明度类型  
    /// 功能:只对 PopUp 窗体有效，其余窗体随意都可以
    /// </summary>
    public enum UIFormLucenyType
    {
        Lucency,        // 完全透明，不能穿透
        Translucence,   // 半透明，不能穿透
        ImPenetrable,   // 低透明度，不能穿透
        Pentrate        // 完全透明，可以穿透
    }

    public enum LanguageType
    {
        CN, // 中文
        EN  // 英文
    }

    #endregion
    public class SysDefine : MonoBehaviour
    {
        /* 路径常量 */
        public const string SYS_PATH_CANVAS = "Canvas";
        public const string SYS_PATH_UIFORMS_CONFIG_INFO = "UIFormsConfigInfo";
        public const string SYS_PATN_LOG_CONFIG_INFO = "SysLogConfigInfo";
        /* 标签常量 */
        public const string SYS_TAG_CANVAS = "_TagCanvas";
        /* 节点常量 */
        public const string SYS_NORMAL_NODE = "Normal";
        public const string SYS_FIXED_NODE = "Fixed";
        public const string SYS_POPUP_NODE = "PopUp";
        public const string SYS_SCRIPTMANAGER_NODE = "_ScripMgr";
        public const string SYS_UIMASK_PANEL_NODE = "_UiMaskPanel";
        /* 遮罩管理其中，透明度常量*/
        public const float SYS_UIMASK_LUCENCY_COLOR_RGB = 255 / 255F;
        public const float SYS_UIMASK_LUCENCY_COLOR_RGB_A = 0 / 255F;
        public const float SYS_UIMASK_TRANS_LUCENCY_COLOR_RGB = 220 / 255F;
        public const float SYS_UIMASK_TRANS_LUCENCY_COLOR_RGB_A = 50 / 255F;
        public const float SYS_UIMASK_IMPENETRABLE_COLOR_RGB = 50 / 255F;
        public const float SYS_UIMASK_IMPENETRABLE_COLOR_RGB_A = 200 / 255F;
        /* 摄像机层深常量 */
        public const string SYS_UICAMERA_NODE = "UICamera";
        public const int SYS_UICAMERA_DEPTH =100;
        /* 全局性的方法 */
        /* 委托的定义 */
    }
} 