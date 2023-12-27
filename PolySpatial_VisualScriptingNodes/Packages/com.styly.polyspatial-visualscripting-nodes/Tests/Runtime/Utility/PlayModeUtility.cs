#if UNITY_EDITOR
using UnityEditor;
using NUnit.Framework;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

namespace PolySpatialVisualScripting.Test
{
    /// <summary>
    /// Play Mode Test用ユーティリティ
    /// </summary>
    public static class PlayModeUtility
    {
        /// <summary>
        /// Unityシーンロード
        /// </summary>
        /// <param name="sceneName"></param>
        public static void LoadScene(string sceneName)
        {
            var guids = AssetDatabase.FindAssets("t:scene " + sceneName);
            Assert.That(guids.Length == 1, "Scene name must be unique.");

            var path = AssetDatabase.GUIDToAssetPath(guids[0]);
            EditorSceneManager.LoadSceneInPlayMode(path, new LoadSceneParameters(LoadSceneMode.Single));
        }
    }
}
#endif