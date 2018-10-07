/*
 *
 *		Title: "FlappyBirdByMVC" 项目
 *			    主题: 模型层 -- 数据代理类
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
	public class Model_GameDataProxy :Proxy
	{	
        // 类的名称
	    public new const string NAME = "Model_GameDataProxy";
        // 游戏数据实体类
	    private Model_GameData _gameData;

	    public Model_GameDataProxy():base(NAME)
	    {
            _gameData =new Model_GameData();
            // 得到最高分
	        _gameData.HightestScores = PlayerPrefs.GetInt(ProjectConst.GAMEHIGHTESTSCORES); 
	    }
        // 时间增加
	    public void AddGameTime()
	    {
	        ++_gameData.GameTime;
            // 数值发送到视图层
            SendNotification(ProjectConst.MSG_DISPLAYERGAMEINFO, _gameData);
	    }
        // 重置时间
	    public void ResetGameTime()
	    {
	        _gameData.GameTime = 0;
	    }
        // 重置分数
	    public void ResetScore()
	    {
	        _gameData.Scores = 0;

	    }

	    // 增加分数
	    public void AddScores()
	    {
	        ++_gameData.Scores;
            // 更新最高分数
	        GetHightestScores();
	        // 数值发送到视图层
            SendNotification(ProjectConst.MSG_DISPLAYERGAMEINFO, _gameData);
        }
        // 获取最高分数
	    public int GetHightestScores()
	    {
	        if (_gameData.Scores>_gameData.HightestScores)
	        {
	            _gameData.HightestScores = _gameData.Scores;
	        }
	        return _gameData.HightestScores;
	    }
        // 保存最高分数
	    public void SaveHightest()
	    {
	        if (_gameData.HightestScores > PlayerPrefs.GetInt(ProjectConst.GAMEHIGHTESTSCORES))
	        {
	            PlayerPrefs.SetInt(ProjectConst.GAMEHIGHTESTSCORES, _gameData.HightestScores);
	        }
	    }
	}
}