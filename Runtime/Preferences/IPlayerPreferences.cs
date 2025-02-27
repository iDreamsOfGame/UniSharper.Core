// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace UniSharper.Preferences
{
    /// <summary>
    /// This interface provides methods to get or save player preferences data identified by <c>key</c> contains <c>Namespace</c>.
    /// </summary>
    public interface IPlayerPreferences
    {
        string Namespace { get; }

        bool GetBoolean(string key, bool defaultValue = false);

        void SetBoolean(string key, bool value, bool saveImmediately = false);

        byte GetByte(string key, byte defaultValue = 0);

        void SetByte(string key, byte value, bool saveImmediately = false);

        sbyte GetSByte(string key, sbyte defaultValue = 0);

        void SetSByte(string key, sbyte value, bool saveImmediately = false);

        char GetChar(string key, char defaultValue = '\0');

        void SetChar(string key, char value, bool saveImmediately = false);

        decimal GetDecimal(string key, decimal defaultValue = 0.0m);

        void SetDecimal(string key, decimal value, bool saveImmediately = false);

        double GetDouble(string key, double defaultValue = 0.0d);

        void SetDouble(string key, double value, bool saveImmediately = false);

        float GetSingle(string key, float defaultValue = 0.0f);

        void SetSingle(string key, float value, bool saveImmediately = false);

        int GetInt32(string key, int defaultValue = 0);

        void SetInt32(string key, int value, bool saveImmediately = false);

        uint GetUInt32(string key, uint defaultValue = 0);

        void SetUInt32(string key, uint value, bool saveImmediately = false);

        long GetInt64(string key, long defaultValue = 0L);

        void SetInt64(string key, long value, bool saveImmediately = false);

        ulong GetUInt64(string key, ulong defaultValue = 0);

        void SetUInt64(string key, ulong value, bool saveImmediately = false);

        short GetInt16(string key, short defaultValue = 0);

        void SetInt16(string key, short value, bool saveImmediately = false);

        ushort GetUInt16(string key, ushort defaultValue = 0);

        void SetUInt16(string key, ushort value, bool saveImmediately = false);

        string GetString(string key, string defaultValue = "");

        void SetString(string key, string value, bool saveImmediately = false);

        TObject GetObject<TObject>(string key, TObject defaultValue = default) where TObject : class;

        void SetObject<TObject>(string key, TObject value, bool saveImmediately = false) where TObject : class;

        bool HasPreference(string key);

        void DeletePreference(string key);

        void DeletePreferences();

        void Save();
    }
}