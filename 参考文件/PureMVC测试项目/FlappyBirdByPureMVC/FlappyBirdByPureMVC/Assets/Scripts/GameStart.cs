/*
 *
 *		Title: "FlappyBirdByMVC" 项目
 *			    主题: 游戏开始脚本
 *		Description:
 *				功能:
 *
 *		Date: 2018.5.9
 *		Version: 0.1
 *		Modify Recoder:
 *
 *
 */

using SimpleUIFramework;
using UnityEngine;
namespace PureMVCdemo
{
	public class GameStart : MonoBehaviour 
	{	

		void Start () {
            // UI框架的入口
		    UIManager.GetInstance().ShowUIForms(ProjectConst.GAME_START_UIFORM);
        }
	
		
	}
}