using System.IO;
using UnityEditor;
using UnityEngine;
using DependencyInjection.Core;
using DependencyInjection.Configuration;
using DependencyInjection.Editor.DebuggingWindow;
using UnityEditor.SceneManagement;

namespace DependencyInjection.Editor
{
    internal static class DependencyInjectionMenuItems
    {
        [MenuItem("Window/Analysis/Dependency Injection Debugger %e")]
        private static void OpenDependencyInjectionDebuggingWindow()
        {
            EditorWindow.GetWindow<DependencyInjectionDebuggerWindow>(false, "DependencyInjection Debugger", true);
        }

        [MenuItem("Assets/Create/DependencyInjection/Settings")]
        private static void CreateDependencyInjectionSettings()
        {
            var directory = UnityEditorUtility.GetSelectedPathInProjectWindow();
            var desiredAssetPath = Path.Combine(directory, "DependencyInjectionSettings.asset");
            UnityEditorUtility.CreateScriptableObject<DependencyInjectionSettings>(desiredAssetPath);
        }

        [MenuItem("Assets/Create/DependencyInjection/ProjectScope")]
        private static void CreateDependencyInjectionProjectScope()
        {
            var directory = UnityEditorUtility.GetSelectedPathInProjectWindow();
            var desiredAssetPath = Path.Combine(directory, $"{nameof(ProjectScope)}.prefab");

            void Edit(GameObject prefab)
            {
                prefab.AddComponent<ProjectScope>();
            }

            UnityEditorUtility.CreatePrefab(desiredAssetPath, Edit);
        }
        
        [MenuItem("GameObject/DependencyInjection/SceneScope")]
        private static void CreateDependencyInjectionSceneScope()
        {
            var sceneScope = new GameObject(nameof(SceneScope)).AddComponent<SceneScope>();
            Selection.activeObject = sceneScope.gameObject;
            EditorSceneManager.MarkSceneDirty(sceneScope.gameObject.scene);
        }
    }
}