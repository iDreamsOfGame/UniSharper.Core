// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License.
// See LICENSE in the project root for license information.

using UniSharper.Extensions;

namespace UniSharper.Preferences
{
    /// <summary>
    /// This class provides methods to get or save player preferences data identified by <c>key</c> contains <c>Namespace</c> with <see cref="UnityEngine.PlayerPrefs"/>.
    /// </summary>
    public class UniPlayerPreferences<T> : PlayerPreferences<T>, IPlayerPreferences
    {
        public bool GetBoolean(string key, bool defaultValue = false)
        {
            var preferenceKey = GetPreferenceKey(key);
            return PlayerPrefsUtility.GetBoolean(preferenceKey, defaultValue);
        }

        public void SetBoolean(string key, bool value, bool saveImmediately = false)
        {
            var preferenceKey = GetPreferenceKey(key);
            PlayerPrefsUtility.SetBoolean(preferenceKey, value, saveImmediately);
            AddPreferenceKey(preferenceKey);
        }

        public byte GetByte(string key, byte defaultValue = 0)
        {
            var preferenceKey = GetPreferenceKey(key);
            return PlayerPrefsUtility.GetByte(preferenceKey, defaultValue);
        }

        public void SetByte(string key, byte value, bool saveImmediately = false)
        {
            var preferenceKey = GetPreferenceKey(key);
            PlayerPrefsUtility.SetByte(preferenceKey, value, saveImmediately);
            AddPreferenceKey(preferenceKey);
        }

        public sbyte GetSByte(string key, sbyte defaultValue = 0)
        {
            var preferenceKey = GetPreferenceKey(key);
            return PlayerPrefsUtility.GetSByte(preferenceKey, defaultValue);
        }

        public void SetSByte(string key, sbyte value, bool saveImmediately = false)
        {
            var preferenceKey = GetPreferenceKey(key);
            PlayerPrefsUtility.SetSByte(preferenceKey, value, saveImmediately);
            AddPreferenceKey(preferenceKey);
        }

        public char GetChar(string key, char defaultValue = '\0')
        {
            var preferenceKey = GetPreferenceKey(key);
            return PlayerPrefsUtility.GetChar(preferenceKey, defaultValue);
        }

        public void SetChar(string key, char value, bool saveImmediately = false)
        {
            var preferenceKey = GetPreferenceKey(key);
            PlayerPrefsUtility.SetChar(preferenceKey, value, saveImmediately);
            AddPreferenceKey(preferenceKey);
        }

        public decimal GetDecimal(string key, decimal defaultValue = 0.0m)
        {
            var preferenceKey = GetPreferenceKey(key);
            return PlayerPrefsUtility.GetDecimal(preferenceKey, defaultValue);
        }

        public void SetDecimal(string key, decimal value, bool saveImmediately = false)
        {
            var preferenceKey = GetPreferenceKey(key);
            PlayerPrefsUtility.SetDecimal(preferenceKey, value, saveImmediately);
            AddPreferenceKey(preferenceKey);
        }

        public double GetDouble(string key, double defaultValue = 0.0d)
        {
            var preferenceKey = GetPreferenceKey(key);
            return PlayerPrefsUtility.GetDouble(preferenceKey, defaultValue);
        }

        public void SetDouble(string key, double value, bool saveImmediately = false)
        {
            var preferenceKey = GetPreferenceKey(key);
            PlayerPrefsUtility.SetDouble(preferenceKey, value, saveImmediately);
            AddPreferenceKey(preferenceKey);
        }

        public float GetSingle(string key, float defaultValue = 0.0f)
        {
            var preferenceKey = GetPreferenceKey(key);
            return PlayerPrefsUtility.GetSingle(preferenceKey, defaultValue);
        }

        public void SetSingle(string key, float value, bool saveImmediately = false)
        {
            var preferenceKey = GetPreferenceKey(key);
            PlayerPrefsUtility.SetSingle(preferenceKey, value, saveImmediately);
            AddPreferenceKey(preferenceKey);
        }

        public int GetInt32(string key, int defaultValue = 0)
        {
            var preferenceKey = GetPreferenceKey(key);
            return PlayerPrefsUtility.GetInt32(preferenceKey, defaultValue);
        }

        public void SetInt32(string key, int value, bool saveImmediately = false)
        {
            var preferenceKey = GetPreferenceKey(key);
            PlayerPrefsUtility.SetInt32(preferenceKey, value, saveImmediately);
            AddPreferenceKey(preferenceKey);
        }

        public uint GetUInt32(string key, uint defaultValue = 0)
        {
            var preferenceKey = GetPreferenceKey(key);
            return PlayerPrefsUtility.GetUInt32(preferenceKey, defaultValue);
        }

        public void SetUInt32(string key, uint value, bool saveImmediately = false)
        {
            var preferenceKey = GetPreferenceKey(key);
            PlayerPrefsUtility.SetUInt32(preferenceKey, value, saveImmediately);
            AddPreferenceKey(preferenceKey);
        }

        public long GetInt64(string key, long defaultValue = 0)
        {
            var preferenceKey = GetPreferenceKey(key);
            return PlayerPrefsUtility.GetInt64(preferenceKey, defaultValue);
        }

        public void SetInt64(string key, long value, bool saveImmediately = false)
        {
            var preferenceKey = GetPreferenceKey(key);
            PlayerPrefsUtility.SetInt64(preferenceKey, value, saveImmediately);
            AddPreferenceKey(preferenceKey);
        }

        public ulong GetUInt64(string key, ulong defaultValue = 0)
        {
            var preferenceKey = GetPreferenceKey(key);
            return PlayerPrefsUtility.GetUInt64(preferenceKey, defaultValue);
        }

        public void SetUInt64(string key, ulong value, bool saveImmediately = false)
        {
            var preferenceKey = GetPreferenceKey(key);
            PlayerPrefsUtility.SetUInt64(preferenceKey, value, saveImmediately);
            AddPreferenceKey(preferenceKey);
        }

        public short GetInt16(string key, short defaultValue = 0)
        {
            var preferenceKey = GetPreferenceKey(key);
            return PlayerPrefsUtility.GetInt16(preferenceKey, defaultValue);
        }

        public void SetInt16(string key, short value, bool saveImmediately = false)
        {
            var preferenceKey = GetPreferenceKey(key);
            PlayerPrefsUtility.SetInt16(preferenceKey, value, saveImmediately);
            AddPreferenceKey(preferenceKey);
        }

        public ushort GetUInt16(string key, ushort defaultValue = 0)
        {
            var preferenceKey = GetPreferenceKey(key);
            return PlayerPrefsUtility.GetUInt16(preferenceKey, defaultValue);
        }

        public void SetUInt16(string key, ushort value, bool saveImmediately = false)
        {
            var preferenceKey = GetPreferenceKey(key);
            PlayerPrefsUtility.SetUInt16(preferenceKey, value, saveImmediately);
            AddPreferenceKey(preferenceKey);
        }

        public string GetString(string key, string defaultValue = "")
        {
            var preferenceKey = GetPreferenceKey(key);
            return PlayerPrefsUtility.GetString(preferenceKey, defaultValue);
        }

        public void SetString(string key, string value, bool saveImmediately = false)
        {
            var preferenceKey = GetPreferenceKey(key);
            PlayerPrefsUtility.SetString(preferenceKey, value, saveImmediately);
            AddPreferenceKey(preferenceKey);
        }

        public override TObject GetObject<TObject>(string key, TObject defaultValue = null) where TObject : class
        {
            var preferenceKey = GetPreferenceKey(key);
            return PlayerPrefsUtility.GetObject(preferenceKey, defaultValue);
        }

        public override void SetObject<TObject>(string key, TObject value, bool saveImmediately = false) where TObject : class
        {
            var preferenceKey = GetPreferenceKey(key);
            PlayerPrefsUtility.SetObject(preferenceKey, value, saveImmediately);
            AddPreferenceKey(preferenceKey);
        }

        public bool HasPreference(string key)
        {
            return PlayerPrefsUtility.HasPreference(GetPreferenceKey(key));
        }

        public void DeletePreference(string key)
        {
            var preferenceKey = GetPreferenceKey(key);
            PlayerPrefsUtility.DeletePreference(preferenceKey);
            RemoveKey(preferenceKey);
        }

        public void DeletePreferences()
        {
            var keyList = PreferenceKeyList;
            if (keyList == null)
                return;

            foreach (var key in keyList)
            {
                PlayerPrefsUtility.DeletePreference(key);
            }
            
            keyList.Clear();
            PreferenceKeyList = keyList;
            
            Save();
        }

        public void Save()
        {
            PlayerPrefsUtility.Flush();
        }
    }
}