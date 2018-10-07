/*
 *
 *		Title: "SimpleUIFramework" UI框架项目
 *			    主题: UI管理器
 *		Description:
 *				功能:是整个UI框架的核心，用户通过本脚本，来实现框架绝大多数的功能实现
 *
 *		Date: 2018.4.25
 *		Version: 0.1
 *		Modify Recoder:
 *
 *
 */

using System.Collections.Generic;
using UnityEngine;
namespace SimpleUIFramework
{
	public class UIManager : MonoBehaviour 
	{	
        /*字段*/
	    private static UIManager _instance = null;
        // UI窗体预设路径（参数1：窗体预设名称，参数2：表示窗体预设路径）
	    private Dictionary<string, string> _dicFormsPaths;
        // 缓存所有UI窗体
	    private Dictionary<string, BaseUIForm> _dicAllUiForms;
        // 当前显示的UI窗体
	    private Dictionary<string, BaseUIForm> _dicCurrentShowUiForms;
        // 定义“栈”集合，存储显示当前所有【反向切换】的窗体类型
	    private Stack<BaseUIForm> _staCurrentUiForms;
        // UI根节点
	    private Transform _traCanvasTransform = null;
        // 全屏幕显示的节点
	    private Transform _traNormal = null;
        // 固定显示的节点
	    private Transform _traFixed = null;
        // 弹出节点
	    private Transform _traPopUp = null;
        // UI管理脚本的节点
	    private Transform _traUiScripts = null;

        /// <summary>
        /// 得到实例
        /// </summary>
        /// <returns></returns>
        public static UIManager GetInstance()
	    {
	        if (_instance==null)
	        {
	            _instance = new GameObject("_UIManager").AddComponent<UIManager>();
	        }
	        return _instance;
	    }

        // 初始化核心数据，加载"UI窗体路径"到集合中
        public void Awake()
        {
            // 字段初始化
            _dicAllUiForms =new Dictionary<string, BaseUIForm>();
            _dicCurrentShowUiForms =new Dictionary<string, BaseUIForm>();
            _dicFormsPaths = new Dictionary<string, string>();
            _staCurrentUiForms =new Stack<BaseUIForm>();
            // 初始化加载（根UI窗体）Canvas预设
            GameObject _Canvas = InitRootCanvasLoading();
            // 获取UI根节点  全屏节点 固定节点 弹出节点 
            _traCanvasTransform = _Canvas.transform;
            _traNormal = UnityHelper.FindTheChildNode(_Canvas, SysDefine.SYS_NORMAL_NODE);
            _traFixed = UnityHelper.FindTheChildNode(_Canvas, SysDefine.SYS_FIXED_NODE);
            _traPopUp = UnityHelper.FindTheChildNode(_Canvas, SysDefine.SYS_POPUP_NODE);
            _traUiScripts = UnityHelper.FindTheChildNode(_Canvas, SysDefine.SYS_SCRIPTMANAGER_NODE);
            // 将本脚本对象作为“根UI窗体”的子节点
            gameObject.transform.SetParent(_traUiScripts, false);
            // “根UI窗体”在场景转换的时候，不允许销毁
            DontDestroyOnLoad(_traCanvasTransform);
            // 初始化“UI窗体预设”路径数据
            InitUIFormPathData(SysDefine.SYS_PATH_UIFORMS_CONFIG_INFO);
        }
        /// <summary>
        /// 初始化 UI窗体预设 路径数据
        /// </summary>
        /// <param name="pathdata"></param>
	    private void InitUIFormPathData(string pathdata)
	    {
	        IConfigManager config =new ConfigManagerByJson(pathdata);
	        if (_dicFormsPaths != null)
	            _dicFormsPaths = config.AppSetting;
	    }

        private void Update()
        {
            //print("所有窗体集合中的数量" + UIManager.GetInstance().ShowALLUIFormCount());
            //print("当前窗体集合中的数量" + UIManager.GetInstance().ShowCurrentUIFormCount());
            //print("栈窗体集合中的数量" + UIManager.GetInstance().ShowCurrentStackUIFormCount());
        }

        /// <summary>
        /// 显示（打开）UI窗体
        ///  功能：
        /// 1、根据UI窗体的名称，加载到“所有UI窗体”缓存集合中
        /// 2、根据不同的UI窗体的显示模式，分别做不同的加载处理
        /// </summary>
        /// <param name="uiFormName">UI窗体预设的名称</param>
        public void ShowUIForms(string uiFormName)
        {
            BaseUIForm baseUIForms = null;
            //参数的检查
            if (string.IsNullOrEmpty(uiFormName)) return;  // 参数错误返回
            //根据UI窗体的名称，加载到“所有UI窗体”缓存集合中，并且从 “所有UI窗体”缓存集合中 获取本窗体
            baseUIForms = LoadFormsToAllFormsCache(uiFormName);
            if (baseUIForms==null)return;
            // 清空“栈集合”中的数据
            if (baseUIForms.CurrentUiType.IsClearStack)
            {
                bool ClearResult = ClearStackArray();
                if (!ClearResult)
                {
                    Debug.Log("栈中的数据没有清空，检查 uiFormName " + uiFormName);
                }
            }
            //根据不同的UI窗体的显示模式，分别做不同的加载处理
            switch (baseUIForms.CurrentUiType.UIForms_ShowMode)
            {
                case UIFormShowMode.Normal:         // 普通显示
                    // 将 窗体 加载到 _dicCurrentShowUiForms 中
                    LoadUIToCurrentCache(uiFormName);
                    break;
                case UIFormShowMode.ReverseChange:  // 反向切换
                    PushUIFormToStack(uiFormName);
                    break;
                case UIFormShowMode.HideOther:      //隐藏其他
                    EnterUIFormsAndHideOther(uiFormName);
                    break;
            }
        }

        /// <summary>
        /// 关闭指定窗体
        /// </summary>
        /// <param name="uiFormNAME">窗体名称</param>
	    public void CloseUIForms(string uiFormName)
        {
            BaseUIForm baseUiForm=null;
            //参数的检查
	        if (string.IsNullOrEmpty(uiFormName)) return;  // 参数错误返回
            // _dicAllUiForms 中，如果没有记录，则直接返回
            _dicAllUiForms.TryGetValue(uiFormName, out baseUiForm);
            if (baseUiForm==null)return;
            // 根据窗体不同的显示类型，分别做不同的关闭处理
            switch (baseUiForm.CurrentUiType.UIForms_ShowMode)
            {
                case UIFormShowMode.Normal:
                    // 普通窗体的关闭
                    ExitUIForms(uiFormName);
                    break;
                case UIFormShowMode.ReverseChange:
                    // 反向切换的关闭
                    PopUIForm();
                    break;
                case UIFormShowMode.HideOther:
                    // 隐藏其他关闭
                    ExitUIFormsAndHideOther(uiFormName);
                    break;
            }
        }



	    #region 显示“UI管理器”内部核心数据，供测试使用

        /// <summary>
        /// 显示所有UI窗体的数量  
        /// </summary>
        /// <returns></returns>
	    public int ShowALLUIFormCount()
	    {
	        if (_dicAllUiForms != null)
	        {
	            return _dicAllUiForms.Count;
	        }
	        else
	        {
	            return 0;
	        }
	    }
        /// <summary>
        /// 显示 _dicCurrentShowUiForms 当前窗体的数量
        /// </summary>
        /// <returns></returns>
	    public int ShowCurrentUIFormCount()
	    {
	        if (_dicCurrentShowUiForms != null)
	        {
	            return _dicCurrentShowUiForms.Count;

	        }
	        else
	        {
	            return 0;
	        }
        }
        /// <summary>
        /// 显示 当前栈的窗体数量
        /// </summary>
        /// <returns></returns>
	    public int ShowCurrentStackUIFormCount()
	    {
	        if (_staCurrentUiForms != null)
	        {
	            return _staCurrentUiForms.Count;

	        }
	        else
	        {
	            return 0;
	        }
        }

        #endregion


        #region 私有内部方法
        private GameObject InitRootCanvasLoading()
        {
            GameObject canvas = ResourcesMgr.GetInstance().LoadAsset(SysDefine.SYS_PATH_CANVAS, false);
            return canvas;
        }


        /// <summary>
        /// 根据UI窗体的名称，加载到“所有UI窗体”缓存集合中
        /// </summary>
        /// <param name="uiFormName">UI窗体（预设）的名称</param>
        /// <returns></returns>
        private BaseUIForm LoadFormsToAllFormsCache(string uiFormName)
        {
            BaseUIForm baseUIResult = null;
            if (!_dicAllUiForms.TryGetValue(uiFormName, out baseUIResult))
                baseUIResult = LoadUIForm(uiFormName);// 加载指定名称的“UI窗体”
            return baseUIResult;
        }

        /// <summary>
        /// 加载指定名称的“UI窗体”
        /// 功能：
        /// 1、根据“UI窗体名“加载预设克隆体
        /// 2、根据克隆体中不同位置信息，设置不同父对象
        /// 3、隐藏刚创建的UI克隆体
        /// 4、把克隆体加入到 _dicAllUiForms 中
        /// </summary>
        /// <param name="uiFormName">UI窗体的名称</param>
	    private BaseUIForm LoadUIForm(string uiFormName)
        {
            string strUIFormsPaths = null;
            GameObject goCloneUIPrefabs = null;
            BaseUIForm baseUiForm = null;
            // 根据UI窗体名称，得到对应的加载路径
            _dicFormsPaths.TryGetValue(uiFormName, out strUIFormsPaths);
            //1、根据“UI窗体名“加载预设克隆体
            if (!string.IsNullOrEmpty(strUIFormsPaths))
            {
                goCloneUIPrefabs = ResourcesMgr.GetInstance().LoadAsset(strUIFormsPaths, false);
            }
            //2、根据克隆体中不同位置信息，设置不同父对象
            if (_traCanvasTransform != null && goCloneUIPrefabs != null)
            {
                baseUiForm = goCloneUIPrefabs.GetComponent<BaseUIForm>();
                if (baseUiForm == null)
                {
                    Debug.Log(strUIFormsPaths + "  克隆的窗体上没有BaseUIForm");
                    return null;
                }
                switch (baseUiForm.CurrentUiType.UIForms_Type)
                {
                    case UIFormsType.Normal: // 普通窗体
                        goCloneUIPrefabs.transform.SetParent(_traNormal,false);
                        break;
                    case UIFormsType.Fixed: // 固定窗体
                        goCloneUIPrefabs.transform.SetParent(_traFixed, false);
                        break;
                    case UIFormsType.PopUp: //弹出窗体
                        goCloneUIPrefabs.transform.SetParent(_traPopUp, false);
                        break;
                    default:
                        break;
                }
                //3、隐藏刚创建的UI克隆体
                goCloneUIPrefabs.SetActive(false);
                //4、把克隆体加入到 _dicAllUiForms 中
                _dicAllUiForms.Add(uiFormName, baseUiForm);
                return baseUiForm;
            }
            else
            {
                Debug.Log("_traCanvasTransform = null Or  goCloneUIPrefabs = null" + uiFormName);
                return null;
            }

        }

        /// <summary>
        /// 把当前窗体加载到 _dicCurrentShowUiForms 中
        /// </summary>
        /// <param name="uiFormName"></param>
	    private void LoadUIToCurrentCache(string uiFormName)
        {
            BaseUIForm baseUiForm = null; //UI窗体的基类
            BaseUIForm baseUIFormFromAllCache;  // 从_DicAllUIForms 中得到窗体基类
            if (string.IsNullOrEmpty(uiFormName)) return;
            _dicCurrentShowUiForms.TryGetValue(uiFormName, out baseUiForm);
            if (baseUiForm != null) return; // 已经显示，返回
            // 把当前窗体，加载到   _dicCurrentShowUiForms    集合中
            _dicAllUiForms.TryGetValue(uiFormName, out baseUIFormFromAllCache);
            if (baseUIFormFromAllCache != null)
            {
                _dicCurrentShowUiForms.Add(uiFormName, baseUIFormFromAllCache);
                baseUIFormFromAllCache.Display();  //显示当前窗体
            }
            //
        }

        /// <summary>
        /// UI窗体入栈
        /// </summary>
        /// <param name="uiFormName">窗体的名称</param>
	    private void PushUIFormToStack(string uiFormName)
        {
            BaseUIForm baseUiForm = null;
            // 判断栈集合中，是否已有窗体，有则“冻结”处理
            if (_staCurrentUiForms.Count > 0)
            {
                BaseUIForm topUIForm = _staCurrentUiForms.Peek();
                topUIForm.Freeze();
            }
            // 判断 _dicAllUiForms 中是否有指定的UI窗体
            _dicAllUiForms.TryGetValue(uiFormName, out baseUiForm);
            if (baseUiForm != null)
            {
                baseUiForm.Display();
                // 把指定的UI窗体，入栈操作
                _staCurrentUiForms.Push(baseUiForm);
            }
            else
            {
                Debug.Log("BaseUIForm =NULL , uiFormName = " + uiFormName);
            }
        }

        /// <summary>
        /// 打来"隐藏其他属性"的窗体，且隐藏其他所有窗体
        /// </summary>
        /// <param name="uiFormName">需要打开的窗体名称</param>
	    private void EnterUIFormsAndHideOther(string uiFormName)
        {
            BaseUIForm baseUiForm = null;
            BaseUIForm baeUiFormFromALL = null;
            // 参数检查
            if (string.IsNullOrEmpty(uiFormName)) return;
            _dicCurrentShowUiForms.TryGetValue(uiFormName, out baseUiForm);
            if (baseUiForm != null) return; // 已经显示，返回
            _dicAllUiForms.TryGetValue(uiFormName, out baeUiFormFromALL);
            if (baeUiFormFromALL == null) return;
            // 把  _dicCurrentShowUiForms  和  _staCurrentUiForms 中所有的窗体都隐藏
            foreach (var item in _dicCurrentShowUiForms)
            {
                item.Value.Hiding();
            }
            foreach (BaseUIForm uiForm in _staCurrentUiForms)
            {
                uiForm.Hiding();
            }
            // 显示当前窗体，并且加入到  _dicCurrentShowUiForms
            baeUiFormFromALL.Display();
            _dicCurrentShowUiForms.Add(uiFormName, baeUiFormFromALL);
        }


        /// <summary>
        /// 退出指定UI窗体
        /// </summary>
        /// <param name="uiFormName">指定的窗体名称</param>
        private void ExitUIForms(string uiFormName)
        {
            BaseUIForm baseUiForm = null;
            if (string.IsNullOrEmpty(uiFormName)) return;
            // _dicCurrentShowUiForms 中不存在 uiFormName，直接返回
            _dicCurrentShowUiForms.TryGetValue(uiFormName, out baseUiForm);
            if (baseUiForm == null) return;
            // 指定窗体，标记为“隐藏窗体”，并从 _dicCurrentShowUiForms 移除
            baseUiForm.Hiding();
            _dicCurrentShowUiForms.Remove(uiFormName);
        }

        /// <summary>
        ///  “反向切换”属性窗体的出栈
        /// </summary>
	    private void PopUIForm()
        {
            if (_staCurrentUiForms.Count >= 2)
            {
                // 出栈处理
                BaseUIForm topUiForm = _staCurrentUiForms.Pop();
                // 隐藏处理
                topUiForm.Hiding();
                // 新的栈顶重新显示
                BaseUIForm nextUiForm = _staCurrentUiForms.Peek();
                nextUiForm.ReDisplay();
            }
            else if (_staCurrentUiForms.Count == 1)
            {
                // 出栈处理
                BaseUIForm topUiForm = _staCurrentUiForms.Pop();
                // 隐藏处理
                topUiForm.Hiding();
            }
            else
            {
                return;
            }
        }

        /// <summary>
        /// 关闭"隐藏其他属性"的窗体，且显示其他所有窗体
        /// </summary>
        /// <param name="uiFormName">需要关闭的窗体名称</param>
        private void ExitUIFormsAndHideOther(string uiFormName)
        {
            BaseUIForm baseUiForm = null;
            // 参数检查
            if (string.IsNullOrEmpty(uiFormName)) return;
            _dicCurrentShowUiForms.TryGetValue(uiFormName, out baseUiForm);
            if (baseUiForm == null) return;
            // 隐藏当前窗体，并且在 _dicCurrentShowUiForms 中移除
            baseUiForm.Hiding();
            _dicCurrentShowUiForms.Remove(uiFormName);
            // 把  _dicCurrentShowUiForms  和  _staCurrentUiForms 中所有的窗体都显示
            foreach (var item in _dicCurrentShowUiForms)
            {
                item.Value.ReDisplay();
            }
            foreach (BaseUIForm uiForm in _staCurrentUiForms)
            {
                uiForm.ReDisplay();
            }
        }

        /// <summary>
        /// 清空栈集合中的数据 
        /// </summary>
        /// <returns></returns>
	    private bool ClearStackArray()
        {
            if (_staCurrentUiForms != null && _staCurrentUiForms.Count >= 1)
            {
                // 全部隐藏
                foreach (BaseUIForm item in _staCurrentUiForms)
                {
                    item.Hiding();
                }
                // 清空栈集合
                _staCurrentUiForms.Clear();
                return true;
            }
            return false;
        }


        #endregion


    }
}