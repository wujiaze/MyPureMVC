/*
 *
 *		Title: "FlappyBirdByMVC" 项目
 *			    主题: PureMVC 框架 全局管理类 启动项
 *		Description:
 *				功能:
 *				    注册 控制层 
 *                  编程:从这里开始
 *		Date: 2018.5.9
 *		Version: 0.1
 *		Modify Recoder:
 *
 *
 */

using PureMVC.Patterns;
using SimpleUIFramework;
using UnityEngine;
namespace PureMVCdemo
{
	public class ApplicationFacade : Facade 
	{
	    public ApplicationFacade()
	    {
            // 注册核心的 “命令”
            // 启动类 
            RegisterCommand(ProjectConst.REG_STARTGAME_COMMAND, typeof(Ctrl_StartGameCommand));
	        //RegisterCommand(ProjectConst.REG_STARTGAME_COMMAND, typeof(Ctrl_ReStartGameCommand));
         //   RegisterCommand(ProjectConst.REG_ENDGAME_COMMAND, typeof(Ctrl_EndGameCommand));
	        // 先注册 模型层
	        RegisterProxy(new Model_GameDataProxy());
	        // 再注册 视图层
	        RegisterMediator(new View_GamePlayingMediator());
            // 添加游戏对象脚本
            AddGameObjectScripts();
	    }


        /// <summary>
        /// 动态添加游戏对象脚本
        /// 作用 ：当游戏对象更换什么的，更加方便
        /// </summary>
	    private void AddGameObjectScripts()
	    {
            // 方法1：通过Tag
            // 给小鸟挂载控制脚本
            Ctrl_BirdControl tmp = GameObject.FindGameObjectWithTag(ProjectConst.BIRD_TAG)
	            .GetComponent<Ctrl_BirdControl>();
	        if (tmp == null)
	            GameObject.FindGameObjectWithTag(ProjectConst.BIRD_TAG)
	                .AddComponent<Ctrl_BirdControl>();
            
            // 方法2：通过名字 以及 封装的方法
            GameObject root = GameObject.Find(ProjectConst.ROOT);
            // 给陆地挂载移动脚本
	        UnityHelper.AddTheChildNodeComopnetScripts<Ctrl_LandMoving>(root, ProjectConst.LAND);
            // 给管道挂载移动脚本
	        UnityHelper.AddTheChildNodeComopnetScripts<Ctrl_PipesMove>(root, ProjectConst.PIPES);
	        for (int i = 1; i < 4; i++)
	        {
                // 给管道挂载碰撞脚本
	            UnityHelper.AddTheChildNodeComopnetScripts<Ctrl_PipeAndLand>(root, ProjectConst.PIPE_DOWN + i);
	            UnityHelper.AddTheChildNodeComopnetScripts<Ctrl_PipeAndLand>(root, ProjectConst.PIPE_UP + i );
                // 给地面挂载碰撞脚本
	            UnityHelper.AddTheChildNodeComopnetScripts<Ctrl_PipeAndLand>(root, ProjectConst.LANDING + i);
                // 给管道（金币）挂载触发脚本
	            UnityHelper.AddTheChildNodeComopnetScripts<Ctrl_Golds>(root, ProjectConst.PIPE_TRIGGER + i );
            }
            // 给root 挂载 计时脚本
	        root.AddComponent<Ctrl_GetTIme>();
	    }
    }
}