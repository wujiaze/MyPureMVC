/*
 *
 *		Title: "FlappyBirdByMVC" 项目
 *			    主题: 控制层 -- > 金币(管道)触发检测
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
	public class Ctrl_Golds : MonoBehaviour 
	{
        // 模型代理
	    private Model_GameDataProxy _dataProxy;
        // 游戏开始标记
	    private bool _isGameStart = false;
	    void Awake()
	    {
	        transform.GetComponent<BoxCollider2D>().isTrigger = true;
	    }

	    void Start()
	    {
	        _dataProxy = Facade.Instance.RetrieveProxy(Model_GameDataProxy.NAME) as Model_GameDataProxy;
	    }

	    /// <summary>
	    /// 开始游戏
	    /// </summary>
	    public void StartGame()
	    {
	        
	        _isGameStart = true;
	    }
	    /// <summary>
	    /// 结束游戏
	    /// </summary>
	    public void StopGame()
	    {
	        _isGameStart = false;
	    }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(_dataProxy==null)return;
            if (collision.gameObject.tag == ProjectConst.BIRD_TAG)
            {
                _dataProxy.AddScores();
            }
        }

    }
}