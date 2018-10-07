/*
 *
 *		Title: "FlappyBirdByMVC" 项目
 *			    主题: 
 *		Description:
 *				功能:
 *
 *		Date: 2018.5.9
 *		Version: 0.1
 *		Modify Recoder:
 *
 *
 */

using PureMVC.Patterns;
using UnityEngine;
namespace PureMVCdemo
{
    [RequireComponent(typeof(BoxCollider2D))]
	public class Ctrl_PipeAndLand : MonoBehaviour 
	{	
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == ProjectConst.BIRD_TAG)
            {
                // 通过 MVC 事件机制，通知 游戏结束
                Facade.Instance.SendNotification(ProjectConst.REG_ENDGAME_COMMAND);
            }
        }


    }
}