using UnityEngine;

using LD49.Behaviour;
using LD49.Behaviour.UI;

namespace LD49.Manager {
	public sealed class LevelManager : MonoBehaviour {
		public static LevelManager Instance { get; private set; }

		[Header("Dependencies")]
		public WinWindow WinWindow;
		public LoseWindow LoseWindow;

		void OnEnable() {
			if ( Instance ) {
				Debug.LogError("Another instance of LevelManager already exists");
				Destroy(gameObject);
				return;
			}
			Instance = this;
		}

		void OnDisable() {
			if ( Instance == this ) {
				Instance = null;
			} else {
				Debug.LogError("Unexpected scenario");
				return;
			}

			Time.timeScale = 1f;
		}

		void Start() {
			WinWindow.Hide();
			LoseWindow.Hide();
		}

		public void WinLevel() {
			Time.timeScale = 0f;
			WinWindow.Show();
		}

		public void FailLevel() {
			Time.timeScale = 0f;
			LoseWindow.Show();
		}
	}
}
