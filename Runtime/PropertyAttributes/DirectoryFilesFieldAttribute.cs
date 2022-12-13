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
        /// The path of target directory.
        /// </summary>
        public string DirectoryPath { get; }
        
        /// <summary>
        /// The search pattern.
        /// </summary>
        public string SearchPattern { get; }
        
        /// <summary>
        /// The search option.
        /// </summary>
        public SearchOption SearchOption { get; }
        
        /// <summary>
        /// Initialize an instance of <see cref="DirectoryFilesFieldAttribute"/>.
        /// </summary>
        /// <param name="directoryPath">The path of target directory. </param>
        /// <param name="searchPattern">The search pattern. </param>
        /// <param name="searchOption">The search option. </param>
        public DirectoryFilesFieldAttribute(string directoryPath, string searchPattern, SearchOption searchOption = SearchOption.TopDirectoryOnly)
        {
            DirectoryPath = Path.Combine(Directory.GetCurrentDirectory(), directoryPath);
            SearchPattern = searchPattern;
            SearchOption = searchOption;
        }
    }
}