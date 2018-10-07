/*
 *
 *		Title: "FlappyBirdByMVC" 项目
 *			    主题: 命令层：控制层的变换 ==》 开始游戏
 *		Description:
 *				功能:
 *                  编程第2步
 *		Date: 2018.5.9
 *		Version: 0.1
 *		Modify Recoder:
 *
 *
 */

using System;
using PureMVC.Interfaces;
using PureMVC.Patterns;
using SimpleUIFramework;
using UnityEngine;
namespace PureMVCdemo
{
	public class Ctrl_StartGameCommand : MacroCommand
    {
        protected override void InitializeMacroCommand()
        {
            //// 注册模型与视图Command
            AddSubCommand(typeof(Ctrl_RegistViewAndModelCommand));
            // 注册重新开始Command
            AddSubCommand(typeof(Ctrl_ReStartGameCommand));
        }
    }
}