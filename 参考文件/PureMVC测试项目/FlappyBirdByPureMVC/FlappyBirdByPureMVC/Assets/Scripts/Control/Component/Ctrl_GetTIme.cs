/*
 *
 *		Title: "FlappyBirdByMVC" 项目
 *			    主题: 控制层 -- 得到时间
 *		Description:
 *				功能:控制脚本，每一秒，取一次时间
 *
 *		Date: 2018.5.9
 *		Version: 0.1
 *		Modify Recoder:
 *
 *
 */

using System.Collections;
using PureMVC.Patterns;
using UnityEngine;
namespace PureMVCdemo
{
	public class Ctrl_GetTIme : MonoBehaviour 
	{	
        // 模型代理
	    public Model_GameDataProxy DataProxy = null;
        // 游戏开始标记
	    private bool _isStartGame = false;

        /// <summary>
        /// 开始游戏
        /// </summary>
	    public void StartGame()
	    {
	        //DataProxy = Facade.Instance.RetrieveProxy(Model_GameDataProxy.NAME) as Model_GameDataProxy;
            _isStartGame = true;
        }
        /// <summary>
        /// 结束游戏
        /// </summary>
	    public void StopGame()
	    {
	        _isStartGame = false;
        }
        /// <summary>
        /// 重新开始游戏
        /// </summary>
	    public void ReStartGame()
	    {
	        //DataProxy = Facade.Instance.RetrieveProxy(Model_GameDataProxy.NAME) as Model_GameDataProxy;
           
            _isStartGame = true;
        }

	    private IEnumerator ie;
        void Start()
        {
            DataProxy = Facade.Instance.RetrieveProxy(Model_GameDataProxy.NAME) as Model_GameDataProxy;
            ie = GetTime();
            StartCoroutine(ie);
	    }

	    /// <summary>
        /// 协程，得到时间
        /// </summary>
        /// <returns></returns>
	    private IEnumerator GetTime()
	    {
            yield return new WaitForEndOfFrame(); // 等待当前帧的画面渲染完成
            while (true)
	        {
	            yield return new WaitForSeconds(1f);
	            if (DataProxy!=null && _isStartGame)
	            {
	                DataProxy.AddGameTime();
                }
            }
	    }
	}
}