
#region 版权信息
/*
 * -----------------------------------------------------------
 *  Copyright (c) KeJun All rights reserved.
 * -----------------------------------------------------------
 *		描述: 
 *      创建者：#DEVELOPERNAME#
 *      创建时间: #CREATIONDATE#
 *  
 */
#endregion


using Framework.Unity.MultiLanguage;
using UnityEngine;

namespace Assets
{
    public class Test : MonoBehaviour
    {
        #region Fields

        #endregion

        #region Properties

        #endregion

        #region Unity Messages
        //    void Awake()
        //    {
        //
        //    }
        //    void OnEnable()
        //    {
        //
        //    }
        //
        void Start()
        {

        }
        //    
        //    void Update() 
        //    {
        //    
        //    }
        //
        //    void OnDisable()
        //    {
        //
        //    }
        //
        //    void OnDestroy()
        //    {
        //
        //    }

        #endregion

        #region Private Methods
        [ContextMenu("测试获取值")]
        private void TestGetValue()
        {
            Debug.Log(LanguageManager.mInstance.GetValue("你好"));
            Debug.Log(LanguageManager.mInstance.GetValue("你几岁"));
        }

        [ContextMenu("测试获取值2")]
        private void TestGetValue2()
        {
            Debug.Log(LanguageManager.mInstance.GetValue(LanguageType.Chinese, "你好"));
            Debug.Log(LanguageManager.mInstance.GetValue(LanguageType.Chinese, "你几岁"));
        }

        [ContextMenu("测试切换语言")]

        private void TestSwitchLanguage()
        {
            LanguageManager.mInstance.SwitchLanguage(LanguageType.English);
        }
        #endregion

        #region Protected & Public Methods

        #endregion
    }
}