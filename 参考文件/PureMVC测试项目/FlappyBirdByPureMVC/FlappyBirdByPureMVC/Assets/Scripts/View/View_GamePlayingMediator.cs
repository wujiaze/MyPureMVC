/*
 *
 *		Title: "FlappyBirdByMVC" 项目
 *			    主题: 视图层 -- 显示游戏进行中 UI 界面显示控制
 *		Description:
 *				功能:
 *
 *		Date: 2018.5.9
 *		Version: 0.1
 *		Modify Recoder:
 *
 *
 */

using System.Collections.Generic;
using PureMVC.Interfaces;
using PureMVC.Patterns;
using SimpleUIFramework;
using UnityEngine;
using UnityEngine.UI;

namespace PureMVCdemo
{
	public class View_GamePlayingMediator : Mediator
	{
	    public new const string NAME = "View_GamePlayingMediator";
        // 控件定义
	    private Text _txtGameTime;              // 游戏时间
	    private Text _txtShowGameTime;          // 显示游戏时间
	    private Text _txtGameScore;             // 游戏分数
	    private Text _txtShowGameScore;         // 显示游戏分数
	    private Text _txtGameHightestScore;     //游戏最高分数
	    private Text _txtShowGameHightestScore; //显示游戏最高分数

	    private LanguageManager mgr;
        public View_GamePlayingMediator() : base(NAME) 
        {
            mgr = LanguageManager.GetInstance((LanguageType)0); //中文
            //mgr = LanguageManager.GetInstance((LanguageType)1); //英文
        }
        // 初始化字段
        private void InitMediatorField()
	    {
	        // 得到层级视图根节点
	        GameObject canvas = GameObject.Find(ProjectConst.CANVAS);
	        // 文字控件
	        _txtGameTime = UnityHelper.GetTheChildNodeComopnetScripts<Text>(canvas, ProjectConst.TXT_TIME);
	        _txtGameScore = UnityHelper.GetTheChildNodeComopnetScripts<Text>(canvas, ProjectConst.TXT_SCORE);
	        _txtGameHightestScore = UnityHelper.GetTheChildNodeComopnetScripts<Text>(canvas, ProjectConst.TXT_HIGHTEST_SCORE);
	        _txtGameTime.text = mgr.ShowText(ProjectConst.TIME) ;
	        _txtGameScore.text = mgr.ShowText(ProjectConst.SCORE);
	        _txtGameHightestScore.text = mgr.ShowText(ProjectConst.HIGHTEST);
	        // 文字显示控件
	        _txtShowGameTime = UnityHelper.GetTheChildNodeComopnetScripts<Text>(canvas, ProjectConst.TXT_SHOW_TIME);
	        _txtShowGameScore = UnityHelper.GetTheChildNodeComopnetScripts<Text>(canvas, ProjectConst.TXT_SHOW_SCORE);
	        _txtShowGameHightestScore = UnityHelper.GetTheChildNodeComopnetScripts<Text>(canvas, ProjectConst.TXT_SHOW_HIGHTEST_SCORE);
        }

	    /// <summary>
        /// 允许可以接受的消息列表
        /// </summary>
        /// <returns></returns>
	    public override IList<string> ListNotificationInterests()
	    {
	        IList<string>  Result = new List<string>();
            Result.Add(ProjectConst.MSG_DISPLAYERGAMEINFO);
            Result.Add(ProjectConst.MSG_INITMEDIATOR_FIELD);
	        return Result;
	    }
        /// <summary>
        /// 处理接收到的消息列表
        /// </summary>
        /// <param name="notification"></param>
	    public override void HandleNotification(INotification notification)
        {
            Model_GameData gameData = null;
            switch (notification.Name)
            {
                case ProjectConst.MSG_INITMEDIATOR_FIELD:
                    InitMediatorField();
                    break;
                case ProjectConst.MSG_DISPLAYERGAMEINFO:
                    gameData = notification.Body as Model_GameData;
                    if (gameData!=null)
                    {
                        if (_txtShowGameTime&& _txtShowGameScore && _txtShowGameHightestScore)
                        {
                            _txtShowGameTime.text = gameData.GameTime.ToString();
                            _txtShowGameScore.text = gameData.Scores.ToString();
                            _txtShowGameHightestScore.text = gameData.HightestScores.ToString();
                        }
                    }
                    break;
                    default:
                    break;
            }
        }
	}
}