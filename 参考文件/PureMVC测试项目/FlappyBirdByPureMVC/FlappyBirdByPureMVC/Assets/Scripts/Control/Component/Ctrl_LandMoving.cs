/*
 *
 *		Title: "FlappyBirdByMVC" 项目
 *			    主题: 控制层 ： 陆地移动
 *		Description:
 *				功能:
 *
 *		Date: 2018.5.9
 *		Version: 0.1
 *		Modify Recoder:
 *
 *
 */
using UnityEngine;
namespace PureMVCdemo
{
	public class Ctrl_LandMoving : MonoBehaviour
	{
        // 陆地移动速度
	    public float MovingSpeed = ProjectConst.LAND_MOVE_SPEED;
        // 陆地初始位置
	    private Vector2 _OriginalPositon;
        // 游戏开始标记
	    private bool _isGameStart = false;
	    void Awake()
	    {
	        _OriginalPositon = transform.position;
	    }

        private void Update()
        {
            
            if (_isGameStart)
            {
                transform.Translate(Vector2.left * Time.deltaTime * MovingSpeed);
            }
            // 陆地循环移动
            if (transform.position.x<-5f)
            {
                ResetPipesPosition();
            }
            
        }

        /// <summary>
        /// 游戏开始
        /// </summary>
        public void StartGame()
	    {
	        _isGameStart = true;
	        ResetPipesPosition();
        }
        /// <summary>
        /// 游戏结束
        /// </summary>
        public void GameEnd()
	    {
	        _isGameStart = false;
	    }
        /// <summary>
        /// 游戏重新开始
        /// </summary>
	    public void ReStartGame()
	    {
	        _isGameStart = true;
	        ResetPipesPosition();
        }

	    /// <summary>
	    /// 陆地复位
	    /// </summary>
	    private void ResetPipesPosition()
	    {
	        transform.position = _OriginalPositon;
	    }



    }
}