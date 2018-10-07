/*
 *
 *		Title: "FlappyBirdByMVC" 项目
 *			    主题: 模型层 -- 实体类
 *		Description:
 *				功能: 定义小鸟的相关数据
 *				    1.时间
 *				    2.分数
 *				    3.最高分数
 *
 *		Date: 2018.5.9
 *		Version: 0.1
 *		Modify Recoder:
 *
 *
 */
namespace PureMVCdemo
{
	public class Model_GameData 
	{
        // 游戏时间
	    public int GameTime { get; set; }
        // 游戏分数
	    public int Scores { set; get; }
        // 游戏最高分数
	    public int HightestScores { get; set; }
	}
}