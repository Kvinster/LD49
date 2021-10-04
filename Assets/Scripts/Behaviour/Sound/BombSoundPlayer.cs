using UnityEngine;

namespace LD49.Behaviour.Sound {
	public sealed class BombSoundPlayer : MonoBehaviour {
		public Bomb              Bomb;
		public RandomSoundPlayer BlowUpSoundPlayer;

		void Start() {
			Bomb.OnBlownUp += OnBombBlownUp;
		}

		void OnBombBlownUp() {
			BlowUpSoundPlayer.Play();
		}
	}
}
