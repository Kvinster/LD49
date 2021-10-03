using UnityEngine;
using UnityEngine.SceneManagement;

using LD49.Config;

namespace LD49.Service {
	public static class SceneService {
		const string LevelSceneNamePrefix = "Level_";
		const string MainMenuSceneName    = "MainMenu";

		public static int GetLevelIndexFromSceneName() {
			var sceneName = SceneManager.GetActiveScene().name;
			if ( !sceneName.StartsWith(LevelSceneNamePrefix) ) {
				Debug.LogErrorFormat("SceneService.GetLevelIndexFromSceneName: unexpected scene name '{0}'", sceneName);
				return -1;
			}
			return int.Parse(sceneName.Substring(LevelSceneNamePrefix.Length));
		}

		public static void LoadLevel(int levelIndex) {
			if ( !CheckLevelExists(levelIndex) ) {
				Debug.LogErrorFormat("SceneService.LoadLevel: level '{0}' doesn't exist", levelIndex);
				return;
			}
			SceneManager.LoadScene(LevelSceneNamePrefix + levelIndex);
		}

		public static bool CheckLevelExists(int levelIndex) {
			if ( levelIndex < 0 ) {
				Debug.LogErrorFormat("SceneService.CheckLevelExists: invalid level index '{0}'", levelIndex);
				return false;
			}
			return (levelIndex < LevelsConfig.Instance.TotalLevels);
		}

		public static void LoadMainMenu() {
			SceneManager.LoadScene(MainMenuSceneName);
		}
	}
}
