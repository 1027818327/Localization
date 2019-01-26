using System;

namespace Framework.Unity.MultiLanguage
{
    interface IMultiLanguage
    {
        LanguageType CurrentLanguageType { get; }

        /// <summary>
        /// 加载指定语言
        /// </summary>
        /// <param name="type"></param>
        void LoadLanguage(LanguageType type);

        /// <summary>
        /// 切换语言
        /// </summary>
        /// <param name="type"></param>
        void SwitchLanguage(LanguageType type);

        /// <summary>
        /// 获取指定语言和键值对应的value
        /// </summary>
        /// <param name="type"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        string GetValue(LanguageType type, string key);


        /// <summary>
        /// 获取指定键值对应的value
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        string GetValue(string key);

        /// <summary>
        /// 注册切换语言回调
        /// </summary>
        /// <param name="action"></param>
        void RegisterSwitchCallback(Action action);

        /// <summary>
        /// 解注册切换语言回调
        /// </summary>
        /// <param name="action"></param>
        void UnRegisterSwitchCallback(Action action);
    }
}
