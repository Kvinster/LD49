using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

using System;

namespace LD49.Behaviour.UI {
	public sealed class WinWindow : MonoBehaviour {
		public GameObject GameCompletedRoot;
		public Button     MenuButton;
		public GameObject NextLevelButtonRoot;
		public Button     NextLevelButton;

		void Start() {
			MenuButton.onClick.AddListener(OnMenuClick);
			NextLevelButton.onClick.AddListener(OnNextLevelClick);
		}

		public void Show() {
			var hasNextLevel = false; // TODO: check if there's next level
			GameCompletedRoot.SetActive(!hasNextLevel);
			NextLevelButtonRoot.SetActive(hasNextLevel);

			gameObject.SetActive(true);
		}

		public void Hide() {
			gameObject.SetActive(false);
		}

		void OnMenuClick() {
			SceneManager.LoadScene("MainMenu");
		}

		void OnNextLevelClick() {
			// TODO: load next level
			throw new NotImplementedException();
		}
	}
}
