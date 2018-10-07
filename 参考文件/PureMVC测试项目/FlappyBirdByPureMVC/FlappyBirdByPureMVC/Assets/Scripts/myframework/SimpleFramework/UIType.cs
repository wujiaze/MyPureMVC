/*
 *
 *		Title: "SimpleUIFramework" UI框架项目
 *			    主题: 窗体类型
 *		Description:
 *				功能: 
 *
 *		Date: 2018.4.25
 *		Version: 0.1
 *		Modify Recoder:
 *
 *
 */
using UnityEngine;
namespace SimpleUIFramework
{
    public class UIType
    {
        /// <summary>
        /// UI窗体位置类型
        /// </summary>
        public UIFormsType UIForms_Type = UIFormsType.Normal;
        /// <summary>
        /// UI窗体显示类型
        /// </summary>
        public UIFormShowMode UIForms_ShowMode = UIFormShowMode.Normal;
        /// <summary>
        /// UI窗体透明度类型
        /// 一般是弹出窗体设置，非弹出窗体，使用默认的就可以
        /// </summary>
        public UIFormLucenyType UIForms_LucencyType = UIFormLucenyType.Lucency;
        /// <summary>
        /// 是否清空 “栈集合”
        /// </summary>
        public bool IsClearStack = false;
    }
}