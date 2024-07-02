// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License. See LICENSE in the
// project root for license information.

using UnityEditor;
using UnityEngine;

namespace UniSharperEditor.Extensions
{
    internal static class MissingScriptsCleaner
    {
        [MenuItem("GameObject/Clean Missing Scripts", true)]
        private static bool CleanMissingScriptsOnActiveGameObjectValidator() => Selection.activeGameObject;

        [MenuItem("GameObject/Clean Missing Scripts", false, -2)]
        private static void CleanMissingScriptsOnActiveGameObject()
        {
            if (!Selection.activeGameObject)
                return;

            RemoveMonoBehavioursWithMissingScript(Selection.activeGameObject);
        }
        
        [MenuItem("Assets/Clean Missing Scripts", true)]
        private static bool CleanMissingScriptsValidator() => Selection.gameObjects != null && Selection.gameObjects.Length > 0;

        [MenuItem("Assets/Clean Missing Scripts")]
        private static void CleanMissingScripts()
        {
            var gameObjects = Selection.gameObjects;

            foreach (var gameObject in gameObjects)
            {
                RemoveMonoBehavioursWithMissingScript(gameObject);
            }
        }

        private static void RemoveMonoBehavioursWithMissingScript(GameObject gameObject)
        {
            GameObjectUtility.RemoveMonoBehavioursWithMissingScript(gameObject);

            if (gameObject.transform.childCount > 0)
            {
                for (var i = 0; i < gameObject.transform.childCount; i++)
                {
                    var child = gameObject.transform.GetChild(i);
                    RemoveMonoBehavioursWithMissingScript(child.gameObject);
                }
            }
        }
    }
}