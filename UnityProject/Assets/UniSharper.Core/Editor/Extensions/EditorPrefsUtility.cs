// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License. See LICENSE in the
// project root for license information.

using System;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;

namespace UniSharperEditor.Extensions
{
    /// <summary>
    /// This class provides some useful <see cref="UnityEditor.EditorPrefs"/> utilities.
    /// </summary>
    public static class EditorPrefsUtility
    {
        /// <summary>
        /// Returns the value corresponding to <c>key</c> in the preference file if it exists. If it doesn't exist, it will return <c>defaultValue</c>.
        /// </summary>
        /// <param name="key">The key of the preference data.</param>
        /// <param name="defaultValue">The default value will be returned if the preference data doesn't exist.</param>
        /// <returns>The preference value corresponding to the <c>key</c>.</returns>
        public static bool GetBoolean(string key, bool defaultValue = false)
        {
            if (!IsValidKeyForAccessingPreferenceData(key))
                return defaultValue;
            
            var value = GetString(key);
            bool.TryParse(value, out var result);
            return result;
        }

        /// <summary>
        /// Sets the value of the preference identified by <c>key</c>.
        /// </summary>
        /// <param name="key">The key of the preference data. </param>
        /// <param name="value">The preference value to be saved. </param>
        public static void SetBoolean(string key, bool value)
        {
            if (!IsValidKeyForStoringPreferenceData(key))
                return;
            
            SetValueTypeData(key, value);
        }

        /// <summary>
        /// Returns the value corresponding to <c>key</c> in the preference file if it exists. If it doesn't exist, it will return <c>defaultValue</c>.
        /// </summary>
        /// <param name="key">The key of the preference data.</param>
        /// <param name="defaultValue">The default value will be returned if the preference data doesn't exist.</param>
        /// <returns>The preference value corresponding to the <c>key</c>.</returns>
        public static byte GetByte(string key, byte defaultValue = 0)
        {
            if (!IsValidKeyForAccessingPreferenceData(key))
                return defaultValue;
            
            var value = GetString(key);
            byte.TryParse(value, out var result);
            return result;
        }

        /// <summary>
        /// Sets the value of the preference identified by <c>key</c>.
        /// </summary>
        /// <param name="key">The key of the preference data. </param>
        /// <param name="value">The preference value to be saved. </param>
        public static void SetByte(string key, byte value)
        {
            if (!IsValidKeyForStoringPreferenceData(key))
                return;
            
            SetValueTypeData(key, value);
        }

        /// <summary>
        /// Returns the value corresponding to <c>key</c> in the preference file if it exists. If it doesn't exist, it will return <c>defaultValue</c>.
        /// </summary>
        /// <param name="key">The key of the preference data.</param>
        /// <param name="defaultValue">The default value will be returned if the preference data doesn't exist.</param>
        /// <returns>The preference value corresponding to the <c>key</c>.</returns>
        public static sbyte GetSByte(string key, sbyte defaultValue = 0)
        {
            if (!IsValidKeyForAccessingPreferenceData(key))
                return defaultValue;
            
            var value = GetString(key);
            sbyte.TryParse(value, out var result);
            return result;
        }

        /// <summary>
        /// Sets the value of the preference identified by <c>key</c>.
        /// </summary>
        /// <param name="key">The key of the preference data. </param>
        /// <param name="value">The preference value to be saved. </param>
        public static void SetSByte(string key, sbyte value)
        {
            if (!IsValidKeyForStoringPreferenceData(key))
                return;
            
            SetValueTypeData(key, value);
        }

        /// <summary>
        /// Returns the value corresponding to <c>key</c> in the preference file if it exists. If it doesn't exist, it will return <c>defaultValue</c>.
        /// </summary>
        /// <param name="key">The key of the preference data.</param>
        /// <param name="defaultValue">The default value will be returned if the preference data doesn't exist.</param>
        /// <returns>The preference value corresponding to the <c>key</c>.</returns>
        public static char GetChar(string key, char defaultValue = '\0')
        {
            if (!IsValidKeyForAccessingPreferenceData(key))
                return defaultValue;
            
            var value = GetString(key);
            char.TryParse(value, out var result);
            return result;
        }

        /// <summary>
        /// Sets the value of the preference identified by <c>key</c>.
        /// </summary>
        /// <param name="key">The key of the preference data. </param>
        /// <param name="value">The preference value to be saved. </param>
        public static void SetChar(string key, char value)
        {
            if (!IsValidKeyForStoringPreferenceData(key))
                return;
            
            SetValueTypeData(key, value);
        }

        /// <summary>
        /// Returns the value corresponding to <c>key</c> in the preference file if it exists. If it doesn't exist, it will return <c>defaultValue</c>.
        /// </summary>
        /// <param name="key">The key of the preference data.</param>
        /// <param name="defaultValue">The default value will be returned if the preference data doesn't exist.</param>
        /// <returns>The preference value corresponding to the <c>key</c>.</returns>
        public static decimal GetDecimal(string key, decimal defaultValue = 0.0m)
        {
            if (!IsValidKeyForAccessingPreferenceData(key))
                return defaultValue;
            
            var value = GetString(key);
            decimal.TryParse(value, out var result);
            return result;
        }

        /// <summary>
        /// Sets the value of the preference identified by <c>key</c>.
        /// </summary>
        /// <param name="key">The key of the preference data. </param>
        /// <param name="value">The preference value to be saved. </param>
        public static void SetDecimal(string key, decimal value)
        {
            if (!IsValidKeyForStoringPreferenceData(key))
                return;
            
            SetValueTypeData(key, value);
        }

        /// <summary>
        /// Returns the value corresponding to <c>key</c> in the preference file if it exists. If it doesn't exist, it will return <c>defaultValue</c>.
        /// </summary>
        /// <param name="key">The key of the preference data.</param>
        /// <param name="defaultValue">The default value will be returned if the preference data doesn't exist.</param>
        /// <returns>The preference value corresponding to the <c>key</c>.</returns>
        public static double GetDouble(string key, double defaultValue = 0.0d)
        {
            if (!IsValidKeyForAccessingPreferenceData(key))
                return defaultValue;
            
            var value = GetString(key);
            double.TryParse(value, out var result);
            return result;
        }

        /// <summary>
        /// Sets the value of the preference identified by <c>key</c>.
        /// </summary>
        /// <param name="key">The key of the preference data. </param>
        /// <param name="value">The preference value to be saved. </param>
        public static void SetDouble(string key, double value)
        {
            if (!IsValidKeyForStoringPreferenceData(key))
                return;
            
            SetValueTypeData(key, value);
        }

        /// <summary>
        /// Returns the value corresponding to <c>key</c> in the preference file if it exists. If it doesn't exist, it will return <c>defaultValue</c>.
        /// </summary>
        /// <param name="key">The key of the preference data.</param>
        /// <param name="defaultValue">The default value will be returned if the preference data doesn't exist.</param>
        /// <returns>The preference value corresponding to the <c>key</c>.</returns>
        public static float GetSingle(string key, float defaultValue = 0.0f)
        {
            return string.IsNullOrEmpty(key) ? defaultValue : EditorPrefs.GetFloat(key, defaultValue);
        }

        /// <summary>
        /// Sets the value of the preference identified by <c>key</c>.
        /// </summary>
        /// <param name="key">The key of the preference data. </param>
        /// <param name="value">The preference value to be saved. </param>
        public static void SetSingle(string key, float value)
        {
            if (!IsValidKeyForStoringPreferenceData(key))
                return;
            
            EditorPrefs.SetFloat(key, value);
        }

        /// <summary>
        /// Returns the value corresponding to <c>key</c> in the preference file if it exists. If it doesn't exist, it will return <c>defaultValue</c>.
        /// </summary>
        /// <param name="key">The key of the preference data.</param>
        /// <param name="defaultValue">The default value will be returned if the preference data doesn't exist.</param>
        /// <returns>The preference value corresponding to the <c>key</c>.</returns>
        public static int GetInt32(string key, int defaultValue = 0)
        {
            return string.IsNullOrEmpty(key) ? defaultValue : EditorPrefs.GetInt(key, defaultValue);
        }

        /// <summary>
        /// Sets the value of the preference identified by <c>key</c>.
        /// </summary>
        /// <param name="key">The key of the preference data. </param>
        /// <param name="value">The preference value to be saved. </param>
        public static void SetInt32(string key, int value)
        {
            if (!IsValidKeyForStoringPreferenceData(key))
                return;
            
            EditorPrefs.SetInt(key, value);
        }

        /// <summary>
        /// Returns the value corresponding to <c>key</c> in the preference file if it exists. If it doesn't exist, it will return <c>defaultValue</c>.
        /// </summary>
        /// <param name="key">The key of the preference data.</param>
        /// <param name="defaultValue">The default value will be returned if the preference data doesn't exist.</param>
        /// <returns>The preference value corresponding to the <c>key</c>.</returns>
        public static uint GetUInt32(string key, uint defaultValue = 0)
        {
            if (!IsValidKeyForAccessingPreferenceData(key))
                return defaultValue;
            
            var value = GetString(key);
            uint.TryParse(value, out var result);
            return result;
        }

        /// <summary>
        /// Sets the value of the preference identified by <c>key</c>.
        /// </summary>
        /// <param name="key">The key of the preference data. </param>
        /// <param name="value">The preference value to be saved. </param>
        public static void SetUInt32(string key, uint value)
        {
            if (!IsValidKeyForStoringPreferenceData(key))
                return;
            
            SetValueTypeData(key, value);
        }

        /// <summary>
        /// Returns the value corresponding to <c>key</c> in the preference file if it exists. If it doesn't exist, it will return <c>defaultValue</c>.
        /// </summary>
        /// <param name="key">The key of the preference data.</param>
        /// <param name="defaultValue">The default value will be returned if the preference data doesn't exist.</param>
        /// <returns>The preference value corresponding to the <c>key</c>.</returns>
        public static long GetInt64(string key, long defaultValue = 0L)
        {
            if (!IsValidKeyForAccessingPreferenceData(key))
                return defaultValue;
            
            var value = GetString(key);
            long.TryParse(value, out var result);
            return result;
        }

        /// <summary>
        /// Sets the value of the preference identified by <c>key</c>.
        /// </summary>
        /// <param name="key">The key of the preference data. </param>
        /// <param name="value">The preference value to be saved. </param>
        public static void SetInt64(string key, long value)
        {
            if (!IsValidKeyForStoringPreferenceData(key))
                return;
            
            SetValueTypeData(key, value);
        }

        /// <summary>
        /// Returns the value corresponding to <c>key</c> in the preference file if it exists. If it doesn't exist, it will return <c>defaultValue</c>.
        /// </summary>
        /// <param name="key">The key of the preference data.</param>
        /// <param name="defaultValue">The default value will be returned if the preference data doesn't exist.</param>
        /// <returns>The preference value corresponding to the <c>key</c>.</returns>
        public static ulong GetUInt64(string key, ulong defaultValue = 0)
        {
            if (!IsValidKeyForAccessingPreferenceData(key))
                return defaultValue;
            
            var value = GetString(key);
            ulong.TryParse(value, out var result);
            return result;
        }

        /// <summary>
        /// Sets the value of the preference identified by <c>key</c>.
        /// </summary>
        /// <param name="key">The key of the preference data. </param>
        /// <param name="value">The preference value to be saved. </param>
        public static void SetUInt64(string key, ulong value)
        {
            if (!IsValidKeyForStoringPreferenceData(key))
                return;
            
            SetValueTypeData(key, value);
        }

        /// <summary>
        /// Returns the value corresponding to <c>key</c> in the preference file if it exists. If it doesn't exist, it will return <c>defaultValue</c>.
        /// </summary>
        /// <param name="key">The key of the preference data.</param>
        /// <param name="defaultValue">The default value will be returned if the preference data doesn't exist.</param>
        /// <returns>The preference value corresponding to the <c>key</c>.</returns>
        public static short GetInt16(string key, short defaultValue = 0)
        {
            if (!IsValidKeyForAccessingPreferenceData(key))
                return defaultValue;
            
            var value = GetString(key);
            short.TryParse(value, out var result);
            return result;
        }

        /// <summary>
        /// Sets the value of the preference identified by <c>key</c>.
        /// </summary>
        /// <param name="key">The key of the preference data. </param>
        /// <param name="value">The preference value to be saved. </param>
        public static void SetInt16(string key, short value)
        {
            if (!IsValidKeyForStoringPreferenceData(key))
                return;
            
            SetValueTypeData(key, value);
        }

        /// <summary>
        /// Returns the value corresponding to <c>key</c> in the preference file if it exists. If it doesn't exist, it will return <c>defaultValue</c>.
        /// </summary>
        /// <param name="key">The key of the preference data.</param>
        /// <param name="defaultValue">The default value will be returned if the preference data doesn't exist.</param>
        /// <returns>The preference value corresponding to the <c>key</c>.</returns>
        public static ushort GetUInt16(string key, ushort defaultValue = 0)
        {
            if (!IsValidKeyForAccessingPreferenceData(key))
                return defaultValue;
            
            var value = GetString(key);
            ushort.TryParse(value, out var result);
            return result;
        }

        /// <summary>
        /// Sets the value of the preference identified by <c>key</c>.
        /// </summary>
        /// <param name="key">The key of the preference data. </param>
        /// <param name="value">The preference value to be saved. </param>
        public static void SetUInt16(string key, ushort value)
        {
            if (!IsValidKeyForStoringPreferenceData(key))
                return;
            
            SetValueTypeData(key, value);
        }

        /// <summary>
        /// Returns the value corresponding to <c>key</c> in the preference file if it exists. If it doesn't exist, it will return <c>defaultValue</c>.
        /// </summary>
        /// <param name="key">The key of the preference data.</param>
        /// <param name="defaultValue">The default value will be returned if the preference data doesn't exist.</param>
        /// <returns>The preference value corresponding to the <c>key</c>.</returns>
        public static string GetString(string key, string defaultValue = "")
        {
            return string.IsNullOrEmpty(key) ? defaultValue : EditorPrefs.GetString(key, defaultValue);
        }

        /// <summary>
        /// Sets the value of the preference identified by <c>key</c>.
        /// </summary>
        /// <param name="key">The key of the preference data. </param>
        /// <param name="value">The preference value to be saved. </param>
        public static void SetString(string key, string value)
        {
            if (!IsValidKeyForStoringPreferenceData(key))
                return;
            
            EditorPrefs.SetString(key, value);
        }
        
        /// <summary>
        /// Returns the object value corresponding to <c>key</c> in the preference file if it exists. If it doesn't exist, it will return <c>defaultValue</c>.
        /// </summary>
        /// <param name="key">The key of the preference data.</param>
        /// <param name="defaultValue">The default value will be returned if the preference data doesn't exist.</param>
        /// <typeparam name="T">The type definition of object.</typeparam>
        /// <returns>The preference value corresponding to the <c>key</c>.</returns>
        public static T GetObject<T>(string key, T defaultValue = default) where T : class
        {
            if (!IsValidKeyForAccessingPreferenceData(key))
                return defaultValue;

            var value = GetString(key);
            var result = defaultValue;
            
            try
            {
                result = JsonConvert.DeserializeObject<T>(value);
            }
            catch (Exception e)
            {
                Debug.LogWarning(e.ToString());
            }
            
            return result;
        }

        /// <summary>
        /// Sets the object value of the preference identified by <c>key</c>.
        /// </summary>
        /// <param name="key">The key of the preference data. </param>
        /// <param name="value">The preference value to be saved. </param>
        /// <param name="saveImmediately"><c>true</c> write preference data into file immediately. </param>
        /// <typeparam name="T">The type definition of object. </typeparam>
        public static void SetObject<T>(string key, T value, bool saveImmediately = false) where T : class
        {
            if (!IsValidKeyForStoringPreferenceData(key))
                return;
            
            var result = string.Empty;

            try
            {
                result = JsonConvert.SerializeObject(value);
            }
            catch (Exception e)
            {
                Debug.LogWarning(e.ToString());
            }

            if (string.IsNullOrEmpty(result))
                return;
            
            SetString(key, result);
        }

        /// <summary>
        /// Returns true if <c>key</c> exists in the preferences.
        /// </summary>
        /// <param name="key">The key of the preference data.</param>
        /// <returns><c>true</c> if <c>key</c> exists in the preferences; otherwise, return <c>false</c>.</returns>
        public static bool HasPreference(string key)
        {
            return EditorPrefs.HasKey(key);
        }

        /// <summary>
        /// Removes key and its corresponding value from the preferences.
        /// </summary>
        /// <param name="key">The key of the preference data.</param>
        public static void DeletePreference(string key)
        {
            EditorPrefs.DeleteKey(key);
        }

        /// <summary>
        /// Removes all keys and values from the preferences. Use with caution.
        /// </summary>
        public static void DeleteAllPreferences()
        {
            EditorPrefs.DeleteAll();
        }

        private static bool IsValidKeyForAccessingPreferenceData(string key)
        {
            return !string.IsNullOrEmpty(key) && HasPreference(key);
        }

        private static bool IsValidKeyForStoringPreferenceData(string key)
        {
            return !string.IsNullOrEmpty(key);
        }

        private static void SetValueTypeData<T>(string key, T value) where T : struct
        {
            SetString(key, value.ToString());
        }
    }
}