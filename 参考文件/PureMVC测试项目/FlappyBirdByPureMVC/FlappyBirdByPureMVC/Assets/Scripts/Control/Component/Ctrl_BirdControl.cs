/*
 *
 *		Title: "FlappyBirdByMVC" 项目
 *			    主题: 小鸟的控制脚本
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
    //特性： 脚本对象，若没有挂载 Rigidbody2D 组件，则自动添加
    [RequireComponent(typeof(Rigidbody2D),typeof(CircleCollider2D))]
	public class Ctrl_BirdControl : MonoBehaviour 
	{	
        /* 字段 */
        //升力
	    public float UpPower = ProjectConst.BIRD_UPPOWER_SPEED;
        //2D刚体
        private Rigidbody2D _rd2D;
        // 主角原始位置
	    private Vector2 _vecBirdOriginPos;
        // 游戏是否开始
	    private bool _isGameStart = false;

	    void Awake()
	    {
            // 保存原始的方位
	        _vecBirdOriginPos = transform.position;
	        // 获取2D刚体
	        _rd2D = gameObject.GetComponent<Rigidbody2D>();
            // 禁用2D刚体
	        DisableRigibody2D();
	    }

	    void Update()
	    {
            // 接收玩家输入
	        if (_isGameStart)
	        {
	            if (Input.GetButton(ProjectConst.FIRE1))
	            {
	                _rd2D.velocity = Vector2.up * UpPower;
	            }
	        }
	    }


	    // 游戏开始
	    public void StartGame()
	    {
	        _isGameStart = true;
	        EnableRigibody2D();
	        transform.position = _vecBirdOriginPos;
	    }

	    // 游戏结束
	    public void StopGame()
	    {
	        _isGameStart = false;
	        DisableRigibody2D();
	    }
        // 游戏重新开始
	    public void ReStartGame()
	    {
	        _isGameStart = true;
	        EnableRigibody2D();
	        transform.position = _vecBirdOriginPos;
        }

	    /// <summary>
        /// 禁用刚体
        /// </summary>
	    private void DisableRigibody2D()
	    {
	        _rd2D.simulated = false;
	    }
        /// <summary>
        /// 启用刚体
        /// </summary>
	    private void EnableRigibody2D()
        {
            _rd2D.simulated = true;
        }
    }
}