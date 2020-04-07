using System;

namespace UnityEngine.Networking
{
    public static class UnityWebRequestExtensions
    {
        private const string HeaderContentTypeName = "Content-Type";

        private const string JsonContentTypeValue = "application/json";
        
        public static void SetContentType(this UnityWebRequest request, string value)
        {
            if(string.IsNullOrEmpty(value))
                throw new ArgumentNullException(nameof(value));
            
            request.SetRequestHeader(HeaderContentTypeName, value);
        }

        public static string GetContentType(this UnityWebRequest request) =>
            request.GetRequestHeader(HeaderContentTypeName);

        public static void SetJsonContentType(this UnityWebRequest request)
        {
            request.SetContentType(JsonContentTypeValue);
        }
    }
}