/*
 *
 *		Title: "SimpleUIFramework" UI框架项目
 *			    主题:  语言国际化
 *		Description:
 *				功能: 是的我们的语言根据不同国家，选择语言
 *
 *		Date: 2018.4.25
 *		Version: 0.1
 *		Modify Recoder:
 *
 *
 */

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

namespace SimpleUIFramework
{
    public class LanguageManager
    {
        private static LanguageManager _Instance;
        public LanguageType type;
        private static Dictionary<string, string> _DicLanguageCache;
        private LanguageManager()
        {
            _DicLanguageCache =new Dictionary<string, string>();
        }
        /// <summary>
        /// 得到本类的实例
        /// </summary>
        /// <returns></returns>
        public static LanguageManager GetInstance(LanguageType type)
        {
            if (_Instance == null)
            {
                _Instance = new LanguageManager();
            }
            InitLanguageCache(type);
            return _Instance;
        }

        /// <summary>
        /// 初始化语言缓存集合
        /// </summary>
        /// <param name="LanguagePath">语言json文件路径</param>
        private static void InitLanguageCache(LanguageType type)
        {
            string LanguagePath = null;
            switch (type)
            {
                case LanguageType.CN:
                    LanguagePath = "LauguageJSONConfig_CN";
                    break;
                case LanguageType.EN:
                    LanguagePath = "LauguageJSONConfig";
                    break;
            }
            IConfigManager config = new ConfigManagerByJson(LanguagePath);
            if (config != null)
            {
                _DicLanguageCache.Clear();
                _DicLanguageCache = config.AppSetting;
            }
        }

        /// <summary>
        /// 根据ID显示文本信息
        /// </summary>
        /// <param name="languageID"></param>
        /// <returns></returns>
        public string ShowText(string languageID)
        {
            string strQueryResult =String.Empty;
            if (string.IsNullOrEmpty(languageID)) return null;
            if (_DicLanguageCache!=null&& _DicLanguageCache.Count>=1)
            {

                _DicLanguageCache.TryGetValue(languageID, out strQueryResult);
                if (!string.IsNullOrEmpty(strQueryResult))
                    return strQueryResult;
            }
            return null;
        }
    }
}