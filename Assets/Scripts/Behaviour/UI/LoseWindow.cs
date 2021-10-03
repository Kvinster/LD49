using UnityEngine;
using UnityEngine.UI;

using LD49.Service;

namespace LD49.Behaviour.UI {
	public sealed class LoseWindow : MonoBehaviour {
		public Button MenuButton;
		public Button RestartButton;

		bool _isInit;

		public void Show() {
			TryInit();
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
			RestartButton.onClick.AddListener(OnRestartClick);

			_isInit = true;
		}

		void OnMenuClick() {
			SceneService.LoadMainMenu();
		}

		void OnRestartClick() {
			SceneService.LoadLevel(SceneService.GetLevelIndexFromSceneName());
		}
	}
}
