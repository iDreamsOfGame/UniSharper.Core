// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License.
// See LICENSE in the project root for license information.

using System.Collections.Generic;

namespace UniSharper.Preferences
{
    /// <summary>
    /// This abstract class provides methods to get or save player preferences data identified by <c>key</c> contains <c>Namespace</c>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class PlayerPreferences<T>
    {
        private const string PreferenceKeyListKey = "PreferenceKeyListKey";
        
        public string Namespace { get; } = typeof(T).FullName;
        
        protected List<string> PreferenceKeyList
        {
            get => GetObject<List<string>>(PreferenceKeyListKey);
            set => SetObject(PreferenceKeyListKey, value);
        }

        public abstract TObject GetObject<TObject>(string key, TObject defaultValue = null) where TObject : class;

        public abstract void SetObject<TObject>(string key, TObject value, bool saveImmediately = false) where TObject : class;
        
        protected string GetPreferenceKey(string key) => string.IsNullOrEmpty(key) ? null : $"{Namespace}.{key}";
        
        protected void AddPreferenceKey(string key)
        {
            var keyList = PreferenceKeyList;
            if (keyList != null && keyList.Contains(key)) 
                return;

            keyList ??= new List<string>();
            keyList.Add(key);
            PreferenceKeyList = keyList;
        }

        protected void RemoveKey(string key)
        {
            var keyList = PreferenceKeyList;
            if (keyList != null && keyList.Remove(key))
                PreferenceKeyList = keyList;
        }
    }
}