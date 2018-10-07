/*
 *
 *		Title: "FlappyBirdByMVC" 项目
 *			    主题: 命令层 游戏重新开始
 *		Description:
 *				功能:
 *
 *		Date: 2018.5.9
 *		Version: 0.1
 *		Modify Recoder:
 *
 *
 */

using PureMVC.Interfaces;
using PureMVC.Patterns;
using SimpleUIFramework;
using UnityEngine;
namespace PureMVCdemo
{
	public class Ctrl_ReStartGameCommand : SimpleCommand
    {
        public override void Execute(INotification notification)
        {
            // 主角重新运行
            GameObject.FindGameObjectWithTag(ProjectConst.BIRD_TAG).GetComponent<Ctrl_BirdControl>().StartGame();
            // 陆地重新运行
            GameObject root = GameObject.Find(ProjectConst.ROOT);
            UnityHelper.GetTheChildNodeComopnetScripts<Ctrl_LandMoving>(root, ProjectConst.LAND).StartGame();
            // 管道重新运行
            UnityHelper.GetTheChildNodeComopnetScripts<Ctrl_PipesMove>(root, ProjectConst.PIPES).StartGame();
            // 获取时间脚本,重新计时
            root.GetComponent<Ctrl_GetTIme>().StartGame();
            // 金币道具开始运行
            for (int i = 1; i < 4; i++)
            {
                UnityHelper.GetTheChildNodeComopnetScripts<Ctrl_Golds>(root, ProjectConst.PIPE_TRIGGER + i).StartGame();
            }
        }
    }
}