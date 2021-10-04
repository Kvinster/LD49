using UnityEngine;

namespace LD49.Behaviour.Sound {
	public sealed class ThornsSoundPlayer : MonoBehaviour {
		public Thorns            Thorns;
		public RandomSoundPlayer ActivatedSoundPlayer;

		void Start() {
			Thorns.OnActivated += OnThornsActivated;
		}

		void OnThornsActivated() {
			ActivatedSoundPlayer.Play();
		}
	}
}
