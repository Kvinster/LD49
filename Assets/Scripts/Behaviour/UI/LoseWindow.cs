using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace LD49.Behaviour.UI {
	public sealed class LoseWindow : MonoBehaviour {
		public Button MenuButton;
		public Button RestartButton;

		void Start() {
			MenuButton.onClick.AddListener(OnMenuClick);
			RestartButton.onClick.AddListener(OnRestartClick);
		}

		public void Show() {
			gameObject.SetActive(true);
		}

		public void Hide() {
			gameObject.SetActive(false);
		}

		void OnMenuClick() {
			SceneManager.LoadScene("MainMenu");
		}

		void OnRestartClick() {
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}
	}
}
