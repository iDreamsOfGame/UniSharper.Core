using System;
using System.IO;
using UnityEngine;

namespace UniSharper
{
    /// <summary>
    /// Represents the file list of directory property.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class DirectoryFilesFieldAttribute : PropertyAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DirectoryFilesFieldAttribute"/> class.
        /// </summary>
        /// <param name="directoryPaths">The paths of target directories. </param>
        /// <param name="searchPattern">The search pattern. </param>
        /// <param name="searchOption">The search option. </param>
        /// <param name="withExtension">If <c>true</c>, shows filename with extension; <c>false</c> show filename without extension. </param>
        /// <param name="orderByDescending">If <c>true</c>, sorts the elements of result in descending order; <c>false</c> sorts in ascending order. </param>
        public DirectoryFilesFieldAttribute(string[] directoryPaths,
            string searchPattern,
            SearchOption searchOption = SearchOption.TopDirectoryOnly,
            bool withExtension = false,
            bool orderByDescending = false)
        {
            if (directoryPaths != null)
            {
                var length = directoryPaths.Length;
                DirectoryPaths = new string[length];
                for (var i = 0; i < length; i++)
                {
                    DirectoryPaths[i] = Path.GetFullPath(directoryPaths[i]);
                }
            }
            
            SearchPattern = searchPattern;
            SearchOption = searchOption;
            WithExtension = withExtension;
            OrderByDescending = orderByDescending;
        }

        /// <summary>
        /// The paths of target directories.
        /// </summary>
        public string[] DirectoryPaths { get; }

        /// <summary>
        /// The search pattern.
        /// </summary>
        public string SearchPattern { get; }

        /// <summary>
        /// The search option.
        /// </summary>
        public SearchOption SearchOption { get; }

        /// <summary>
        /// Whether shows filename with extension.
        /// </summary>
        public bool WithExtension { get; }

        /// <summary>
        /// Whether sorts the elements of result in descending order. 
        /// </summary>
        public bool OrderByDescending { get; }
    }
}