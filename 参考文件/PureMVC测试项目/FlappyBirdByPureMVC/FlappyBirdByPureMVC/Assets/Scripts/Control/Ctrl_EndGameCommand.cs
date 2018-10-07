/*
 *
 *		Title: "FlappyBirdByMVC" 项目
 *			    主题: 命令层 游戏停止
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
	public class Ctrl_EndGameCommand : SimpleCommand
    {
        public override void Execute(INotification notification)
        {

            // 脚本停止运行
            StopScriptsRunning();
            // 回到GUIDE 窗口
            closeCurrentUIForm();
            // 保存分数
            Model_GameDataProxy gameDataProxy = Facade.RetrieveProxy(Model_GameDataProxy.NAME) as Model_GameDataProxy;
            gameDataProxy.SaveHightest();
            // 重置时间
            gameDataProxy.ResetGameTime();
            gameDataProxy.ResetScore();
        }

        private void StopScriptsRunning()
        {
            // 主角停止运行
            GameObject.FindGameObjectWithTag(ProjectConst.BIRD_TAG).GetComponent<Ctrl_BirdControl>().StopGame();
            // 管道停止运行
            GameObject root = GameObject.Find(ProjectConst.ROOT);
            UnityHelper.GetTheChildNodeComopnetScripts<Ctrl_PipesMove>(root, ProjectConst.PIPES).EndGame();
            // 陆地停止运行
            UnityHelper.GetTheChildNodeComopnetScripts<Ctrl_LandMoving>(root, ProjectConst.LAND).GameEnd();
            // 获取时间脚本，停止计时
            root.GetComponent<Ctrl_GetTIme>().StopGame();
            // 金币道具停止运行
            for (int i = 1; i < 4; i++)
            {
                UnityHelper.GetTheChildNodeComopnetScripts<Ctrl_Golds>(root, ProjectConst.PIPE_TRIGGER + i ).StopGame();
            }
        }
        // 关闭当前UI窗体，显示上一个窗体
        private void closeCurrentUIForm()
        {
            UIManager.GetInstance().CloseUIForms(ProjectConst.GAME_PLAYING_UIFORM);
        }
    }
}