using UnityEngine;
using UnityEngine.UI;

using LD49.Service;

namespace LD49.Behaviour.UI {
	public sealed class WinWindow : MonoBehaviour {
		public GameObject GameCompletedRoot;
		public Button     MenuButton;
		public GameObject NextLevelButtonRoot;
		public Button     NextLevelButton;

		bool _isInit;

		int _levelIndex;

		public void Show() {
			TryInit();

			var hasNextLevel = SceneService.CheckLevelExists(_levelIndex + 1);
			GameCompletedRoot.SetActive(!hasNextLevel);
			NextLevelButtonRoot.SetActive(hasNextLevel);

			gameObject.SetActive(true);
		}

		public void Hide() {
			gameObject.SetActive(false);
		}

		void TryInit() {
			if ( _isInit ) {
				return;
			}

			MenuButton.onClick.AddListener(OnMenuClick);
			NextLevelButton.onClick.AddListener(OnNextLevelClick);

			_levelIndex = SceneService.GetLevelIndexFromSceneName();

			_isInit = true;
		}

		void OnMenuClick() {
			SceneService.LoadMainMenu();
		}

		void OnNextLevelClick() {
			SceneService.LoadLevel(_levelIndex + 1);
		}
	}
}
