using UnityEngine;

using LD49.Behaviour;
using LD49.Behaviour.UI;

namespace LD49.Manager {
	public sealed class LevelManager : MonoBehaviour {
		[Header("Dependencies")]
		public Bomb Bomb;
		public WinArea WinArea;
		[Space]
		public WinWindow WinWindow;
		public LoseWindow LoseWindow;

		void OnDisable() {
			Time.timeScale = 1f;
		}

		void Start() {
			if ( !Bomb ) {
				Debug.LogError("No bomb on level", this);
				return;
			}
			Bomb.OnBlowUp       += OnBombBlownUp;
			WinArea.OnCompleted += OnLevelCompleted;

			WinWindow.Hide();
			LoseWindow.Hide();
		}

		void OnBombBlownUp() {
			Time.timeScale = 0f;
			LoseWindow.Show();
		}

		void OnLevelCompleted() {
			Time.timeScale = 0f;
			WinWindow.Show();
		}
	}
}