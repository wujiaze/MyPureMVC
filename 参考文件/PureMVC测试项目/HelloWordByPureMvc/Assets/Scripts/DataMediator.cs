/*
 *
 *		Title: "PureMVC" 客户端框架项目
 *			    主题: 视图层,
 *		Description:
 *				功能: 属于 “视图层” 显示玩家的UI页面
 *
 *		Date: 2018.5.7
 *		Version: 0.1
 *		Modify Recoder:
 *
 *
 */

using System.Collections.Generic;
using PureMVC.Interfaces;
using PureMVC.Patterns;
using UnityEngine;
using UnityEngine.UI;

namespace PureMVCDemo
{
    // Mediator :中介者
    public class DataMediator : Mediator
	{
        // 定义名称
        public new const string NAME = "DataMediator";
        // 定义两个显示的控件
        private Text TxtNumber;

	    private Button BtnDis;

	    public DataMediator(GameObject goRootNode)
	    {
	        TxtNumber = goRootNode.transform.Find("Txt_DisplayNum").GetComponent<Text>();
	        BtnDis = goRootNode.transform.Find("Btn_Count").GetComponent<Button>();
            // 注册按钮
	        BtnDis.onClick.AddListener(() =>
	        {
	            // 定义消息，发送给控制层
                SendNotification("Reg_StartDataCommand");
	        });
        }



        // 本视图层，允许接受的消息
	    public override IList<string> ListNotificationInterests()
	    {
	        IList<string> listResult =new System.Collections.Generic.List<string>();
            // 可以接收的消息集合
	        listResult.Add("Msg_AddLevel");
	        return listResult;
	    }

        /// <summary>
        /// 处理其他类发给本类允许处理的消息
        /// </summary>
        /// <param name="notification"></param>
	    public override void HandleNotification(INotification notification)
        {
            switch (notification.Name)
            {
                case "Msg_AddLevel":
                    // 显示数据，把模型层的数据显示给控件
                    MyData mydata = notification.Body as MyData;
                    TxtNumber.text = mydata.Number.ToString();
                    break;
            }
        }

    }
}