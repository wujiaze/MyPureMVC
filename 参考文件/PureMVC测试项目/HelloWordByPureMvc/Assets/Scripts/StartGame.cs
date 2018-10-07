/*
 *
 *		Title: "PureMVC" 客户端框架项目
 *			    主题: 游戏开始
 *		Description:
 *				功能: 游戏项目的入口
 *
 *		Date: 2018.5.7
 *		Version: 0.1
 *		Modify Recoder:
 *
 *
 */
using UnityEngine;
namespace PureMVCDemo
{
	public class StartGame : MonoBehaviour 
	{	

		void Start ()
		{
		    new ApplicationFacade(this.gameObject);
		}
	
		void Update (){
			
		}
	}
}