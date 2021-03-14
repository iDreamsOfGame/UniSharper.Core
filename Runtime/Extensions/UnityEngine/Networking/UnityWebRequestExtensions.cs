// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License.
// See LICENSE in the project root for license information.

using System;

namespace UnityEngine.Networking
{
    /// <summary>
    /// Extension methods collection of <see cref="UnityEngine.Networking.UnityWebRequest"/>.
    /// </summary>
    public static class UnityWebRequestExtensions
    {
        #region Fields

        private const string HeaderContentTypeName = "Content-Type";

        private const string JsonContentTypeValue = "application/json";

        #endregion Fields

        #region Methods

        /// <summary>
        /// Get the Content-Type of request header.
        /// </summary>
        /// <param name="request">The <see cref="UnityEngine.Networking.UnityWebRequest"/> object.</param>
        /// <returns>The Content-Type of request header. </returns>
        public static string GetRequestContentType(this UnityWebRequest request) =>
            request.GetRequestHeader(HeaderContentTypeName);

        /// <summary>
        /// Set the Content-Type of request header.
        /// </summary>
        /// <param name="request">The <see cref="UnityEngine.Networking.UnityWebRequest"/> object.</param>
        /// <param name="value">The value of Content-Type of request header to be set.</param>
        /// <exception cref="ArgumentNullException"><c>value</c> is <c>null</c> or <see cref="String.Empty"/>.</exception>
        public static void SetContentType(this UnityWebRequest request, string value)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException(nameof(value));

            request.SetRequestHeader(HeaderContentTypeName, value);
        }

        /// <summary>
        /// Set Content-Type as JSON format.
        /// </summary>
        /// <param name="request">The <see cref="UnityEngine.Networking.UnityWebRequest"/> object.</param>
        public static void SetJsonContentType(this UnityWebRequest request)
        {
            request.SetContentType(JsonContentTypeValue);
        }

        #endregion Methods
    }
}