using UnityEngine;
using UnityEngine.UI;

using LD49.Service;

namespace LD49.Behaviour.UI {
	public class MainMenu : MonoBehaviour {
		[SerializeField]
		private Button _startButton;

		private void Awake() {
			_startButton.onClick.AddListener(OnStartButton);
		}

		private void OnStartButton() {
			SceneService.LoadLevel(0);
		}
	}
}
