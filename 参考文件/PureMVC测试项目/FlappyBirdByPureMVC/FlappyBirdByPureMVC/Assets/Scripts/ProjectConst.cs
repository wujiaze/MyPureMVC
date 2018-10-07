/*
 *
 *		Title: "FlappyBirdByMVC" 项目
 *			    主题: 本项目所有常量定义
 *		Description:
 *				功能:
 *
 *		Date: 2018.5.9
 *		Version: 0.1
 *		Modify Recoder:
 *
 *
 */
namespace PureMVCdemo
{
	public class ProjectConst 
	{
        /* PureMVC 通信常量 */

        // 命令通知
	    public const string REG_STARTGAME_COMMAND = "Reg_StartGameCommand";
	    public const string REG_ENDGAME_COMMAND = "Reg_EndGameCommand";

        // 消息通知
	    public const string MSG_DISPLAYERGAMEINFO = "Msg_DisplayGameInfo";// 消息 模型层 --> 视图层

	    public const string MSG_INITMEDIATOR_FIELD = "Msg_InitMediatorField"; //框架外部 发送消息

        /* UI 窗体预设  */
        // 项目 UI控件名称
        public const string CANVAS = "Canvas(Clone)";
	    public const string TXT_TIME = "Txttime";
	    public const string TXT_SCORE = "TxtScores";
	    public const string TXT_HIGHTEST_SCORE = "TxtHighScores";
	    public const string TXT_SHOW_TIME = "TxtTimeShow";
	    public const string TXT_SHOW_SCORE = "TxtScoresShow";
	    public const string TXT_SHOW_HIGHTEST_SCORE = "TxtHighScoreShow";
	    public const string PIPE_DOWN = "Pipe_Down";
	    public const string PIPE_TRIGGER = "Pipe_Trigger";
	    public const string PIPE_UP = "Pipe_Up";
	    public const string LANDING = "Landing";
        // UI框架
        /// <summary>
        /// StartUIForm
        /// </summary>
        public const string GAME_START_UIFORM = "StartUIForm";
	    /// <summary>
	    /// GameGuideUIForm
	    /// </summary>
	    public const string GAME_GUIDE_UIFORM = "GameGuideUIForm";
	    /// <summary>
	    /// GamePlayingUIForm
	    /// </summary>
	    public const string GAME_PLAYING_UIFORM = "GamePlayingUIForm";
	    /// <summary>
	    /// ForwardGround
	    /// </summary>
	    public const string GAME_BTN_GUIDE = "ForwardGround";
	    /// <summary>
	    /// BtnGuidEOK
	    /// </summary>
	    public const string BTN_GUIDE_OK = "BtnGuidEOK";
        /* 项目常量 */

        /* 字段 */
        /// <summary>
        /// 鸟的上升速度
        /// </summary>
        public const float BIRD_UPPOWER_SPEED = 3;
        /// <summary>
        /// 陆地/管道移动速度
        /// </summary>
	    public const float LAND_MOVE_SPEED = 1;
        /// <summary>
        /// 对象根节点
        /// </summary>
	    public const string ROOT = "MainGameScene";
        /// <summary>
        /// 陆地对象名
        /// </summary>
        public const string LAND = "GameLanding";
        /// <summary>
        /// 管道组对象
        /// </summary>
	    public const string PIPES = "PipesGroup";
        /// <summary>
        /// BirdTag
        /// </summary>
	    public const string BIRD_TAG = "Player";
        /* 按键常量 */
	    public const string FIRE1 = "Fire1";
        /* 项目 PlayerPrefs 持久化常量 */
	    public const string GAMEHIGHTESTSCORES = "GameHighsetScores";
        /* 语言 */
	    public const string TIME = "Time";
	    public const string SCORE = "Score";
	    public const string HIGHTEST = "HightestScore";
	}
}