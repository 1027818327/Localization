
#region 版权信息
/*
 * -----------------------------------------------------------
 *  Copyright (c) KeJun All rights reserved.
 * -----------------------------------------------------------
 *		描述: 
 *      创建者：陈伟超
 *      创建时间: #CREATIONDATE#
 *  
 */
#endregion


using System;
using System.Collections.Generic;
using UnityEngine;

namespace Framework.Unity.MultiLanguage
{
    public class LanguageManager : MonoBehaviour, IMultiLanguage
    {
        #region Fields
        /// <summary>
        /// 当前支持的语言类型
        /// </summary>
        public LanguageType[] mLanguageTypes;
        /// <summary>
        /// 对应的语言的配置文件
        /// </summary>
        public TextAsset[] mLanguageTextAssets;

        public static LanguageManager mInstance;

        /// <summary>
        /// 当前语言类型
        /// </summary>
        [SerializeField]
        private LanguageType mCurrentLanguageType = LanguageType.Chinese;

        private Dictionary<LanguageType, Dictionary<string, string>> mLanguageDic;

        private Action mSwitchAction;

        #endregion

        #region Properties
        public LanguageType CurrentLanguageType
        {
            get
            {
                return mCurrentLanguageType;
            }
        }
        #endregion

        #region Unity Messages
        void Awake()
        {
            mInstance = this;
            DontDestroyOnLoad(gameObject);
        }
        //    void OnEnable()
        //    {
        //
        //    }
        //
        void Start()
        {
            LoadLanguage(mCurrentLanguageType);
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

        #endregion

        #region Protected & Public Methods


        public void LoadLanguage(LanguageType type)
        {
            if (mLanguageDic == null)
            {
                mLanguageDic = new Dictionary<LanguageType, Dictionary<string, string>>();
            }

            if (mLanguageDic.ContainsKey(type))
            {
                return;
            }
            mLanguageDic.Add(type, new Dictionary<string, string>());

            int tempIndex = Array.IndexOf(mLanguageTypes, type);
            if (tempIndex < 0)
            {
                return;
            }

            if (mLanguageTextAssets.Length <= tempIndex)
            {
                return;
            }
            if (mLanguageTextAssets[tempIndex] == null)
            {
                Debug.LogError("你没有配置" + type.ToString() + "语言对应的文件");
                return;
            }
            string tempText = mLanguageTextAssets[tempIndex].text;

            var tempJArray = SimpleJSON.JSON.Parse(tempText).AsArray;
            for (int i = 0; i < tempJArray.Count; i++)
            {
                string tempKey = tempJArray[i]["key"];
                string tempValue = tempJArray[i]["value"];

                mLanguageDic[type][tempKey] = tempValue;
            }
        }

        public void SwitchLanguage(LanguageType type)
        {
            LoadLanguage(type);
            mCurrentLanguageType = type;
        }

        public string GetValue(LanguageType type, string key)
        {
            if (!mLanguageDic.ContainsKey(type))
            {
                return key;
            }
            if (mLanguageDic[type].ContainsKey(key) == false)
            {
                return key;
            }

            return mLanguageDic[type][key];
        }

        public string GetValue(string key)
        {
            return GetValue(mCurrentLanguageType, key);
        }

        public void RegisterSwitchCallback(Action action)
        {
            mSwitchAction += action;
        }

        public void UnRegisterSwitchCallback(Action action)
        {
            mSwitchAction -= action;
        }

        #endregion
    }
}