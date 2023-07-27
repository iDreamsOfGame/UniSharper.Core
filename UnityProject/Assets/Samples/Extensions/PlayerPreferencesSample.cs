using Newtonsoft.Json;
using UniSharper.Preferences;
using UnityEngine;

namespace UniSharper.Samples
{
    internal class PlayerPreferencesSample : MonoBehaviour
    {
        private class PreferenceSampleObject
        {
            [JsonProperty("int")]
            public int IntValue { get; set; }

            [JsonProperty("string")]
            public string StringValue { get; set; }

            public override string ToString()
            {
                return $"{{ IntValue={IntValue}, StringValue={StringValue} }}";
            }
        }
        
        private const string BooleanPrefKey = "BooleanPrefKey";

        private const string BytePrefKey = "BytePrefKey";

        private const string SBytePrefKey = "SBytePrefKey";

        private const string CharPrefKey = "CharPrefKey";

        private const string DecimalPrefKey = "DecimalPrefKey";

        private const string DoublePrefKey = "DoublePrefKey";

        private const string SinglePrefKey = "SinglePrefKey";

        private const string Int32PrefKey = "Int32PrefKey";
        
        private const string UInt32PrefKey = "UInt32PrefKey";
        
        private const string Int64PrefKey = "Int64PrefKey";
        
        private const string UInt64PrefKey = "UInt64PrefKey";
        
        private const string Int16PrefKey = "Int16PrefKey";
        
        private const string UInt16PrefKey = "UInt16PrefKey";

        private const string StringPrefKey = "StringPrefKey";

        private const string ObjectPrefKey = "ObjectPrefKey";

        private IPlayerPreferences playerPreferences;

        private void Awake()
        {
            playerPreferences = new PlayerPreferences<PlayerPreferencesSample>();
            Debug.Log($"PlayerPreferences.Namespace={playerPreferences.Namespace}");
            
            playerPreferences.DeletePreferences();
            
            BoolSample(BooleanPrefKey, true);
            ByteSample(BytePrefKey, 255);
            SByteSample(SBytePrefKey, -128);
            CharSample(CharPrefKey, 's');
            DecimalSample(DecimalPrefKey, 0.1111321321m);
            DoubleSample(DoublePrefKey, 13213.321321321d);
            SingleSample(SinglePrefKey, 3198032190.3783921f);
            Int32Sample(Int32PrefKey, -321525272);
            UInt32Sample(UInt32PrefKey, 3215252752);
            Int64Sample(Int64PrefKey, -5234324322);
            UInt64Sample(UInt64PrefKey, 523432432432);
            Int16Sample(Int16PrefKey, -456);
            UInt16Sample(UInt16PrefKey, 6536);
            StringSample(StringPrefKey, "dsaioeuwqnmcxalk");
            ObjectSample(ObjectPrefKey, new PreferenceSampleObject
            {
                IntValue = int.MaxValue,
                StringValue = "Hello World!"
            });
        }

        private void ObjectSample(string key, PreferenceSampleObject to)
        {
            var value = playerPreferences.GetObject<PreferenceSampleObject>(key);
            Debug.Log($"objectValue={value}");
            
            playerPreferences.SetObject(key, to);
            value = playerPreferences.GetObject<PreferenceSampleObject>(key);
            Debug.Log($"objectValue={value}");
        }

        private void StringSample(string key, string to)
        {
            var value = playerPreferences.GetString(key);
            Debug.Log($"stringValue={value}");
            
            playerPreferences.SetString(key, to);
            value = playerPreferences.GetString(key);
            Debug.Log($"stringValue={value}");
        }

        private void Int16Sample(string key, short to)
        {
            var value = playerPreferences.GetInt16(key);
            Debug.Log($"int16Value={value}");
            
            playerPreferences.SetInt16(key, to);
            value = playerPreferences.GetInt16(key);
            Debug.Log($"int16Value={value}");
        }

        private void UInt16Sample(string key, ushort to)
        {
            var value = playerPreferences.GetUInt16(key);
            Debug.Log($"uint16Value={value}");
            
            playerPreferences.SetUInt16(key, to);
            value = playerPreferences.GetUInt16(key);
            Debug.Log($"uint16Value={value}");
        }

        private void Int64Sample(string key, long to)
        {
            var value = playerPreferences.GetInt64(key);
            Debug.Log($"int64Value={value}");
            
            playerPreferences.SetInt64(key, to);
            value = playerPreferences.GetInt64(key);
            Debug.Log($"int64Value={value}");
        }

        private void UInt64Sample(string key, ulong to)
        {
            var value = playerPreferences.GetUInt64(key);
            Debug.Log($"uint64Value={value}");
            
            playerPreferences.SetUInt64(key, to);
            value = playerPreferences.GetUInt64(key);
            Debug.Log($"uint64Value={value}");
        }

        private void UInt32Sample(string key, uint to)
        {
            var value = playerPreferences.GetUInt32(key);
            Debug.Log($"uint32Value={value}");
            
            playerPreferences.SetUInt32(key, to);
            value = playerPreferences.GetUInt32(key);
            Debug.Log($"uint32Value={value}");
        }

        private void Int32Sample(string key, int to)
        {
            var value = playerPreferences.GetInt32(key);
            Debug.Log($"int32Value={value}");
            
            playerPreferences.SetInt32(key, to);
            value = playerPreferences.GetInt32(key);
            Debug.Log($"int32Value={value}");
        }

        private void SingleSample(string key, float to)
        {
            var value = playerPreferences.GetSingle(key);
            Debug.Log($"singleValue={value}");
            
            playerPreferences.SetSingle(key, to);
            value = playerPreferences.GetSingle(key);
            Debug.Log($"singleValue={value}");
        }

        private void DoubleSample(string key, double to)
        {
            var value = playerPreferences.GetDouble(key);
            Debug.Log($"doubleValue={value}");
            
            playerPreferences.SetDouble(key, to);
            value = playerPreferences.GetDouble(key);
            Debug.Log($"doubleValue={value}");
        }

        private void DecimalSample(string key, decimal to)
        {
            var value = playerPreferences.GetDecimal(key);
            Debug.Log($"decimalValue={value}");
            
            playerPreferences.SetDecimal(key, to);
            value = playerPreferences.GetDecimal(key);
            Debug.Log($"decimalValue={value}");
        }

        private void CharSample(string key, char to)
        {
            var value = playerPreferences.GetChar(key);
            Debug.Log($"charValue={value}");
            
            playerPreferences.SetChar(key, to);
            value = playerPreferences.GetChar(key);
            Debug.Log($"charValue={value}");
        }

        private void BoolSample(string key, bool to)
        {
            var value = playerPreferences.GetBoolean(key);
            Debug.Log($"boolValue={value}");
            
            playerPreferences.SetBoolean(key, to);
            value = playerPreferences.GetBoolean(key);
            Debug.Log($"boolValue={value}");
        }
        
        
        private void ByteSample(string key, byte to)
        {
            var value = playerPreferences.GetByte(key);
            Debug.Log($"byteValue={value}");
            
            playerPreferences.SetByte(key, to);
            value = playerPreferences.GetByte(key);
            Debug.Log($"byteValue={value}");
        }
        
        
        private void SByteSample(string key, sbyte to)
        {
            var value = playerPreferences.GetSByte(key);
            Debug.Log($"sbyteValue={value}");
            
            playerPreferences.SetSByte(key, to);
            value = playerPreferences.GetSByte(key);
            Debug.Log($"sbyteValue={value}");
        }
    }
}