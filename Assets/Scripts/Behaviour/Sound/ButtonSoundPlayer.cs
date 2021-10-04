using UnityEngine;

namespace LD49.Behaviour.Sound {
	public sealed class ButtonSoundPlayer : MonoBehaviour {
		public GameplayButton    Button;
		public RandomSoundPlayer PressedSoundPlayer;
		public RandomSoundPlayer ReleasedSoundPlayer;

		void Start() {
			Button.OnPressed  += OnButtonPressed;
			Button.OnReleased += OnButtonReleased;
		}

		void OnButtonPressed() {
			if ( PressedSoundPlayer ) {
				PressedSoundPlayer.Play();
			}
		}

		void OnButtonReleased() {
			if ( ReleasedSoundPlayer ) {
				ReleasedSoundPlayer.Play();
			}
		}
	}
}
