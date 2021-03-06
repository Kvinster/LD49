using UnityEngine;

using LD49.Service;

namespace LD49.Behaviour.Sound {
	public sealed class RandomSoundPlayer : MonoBehaviour {
		public AudioClip[] Sounds;

		public void Play() {
			if ( Sounds.Length == 0 ) {
				Debug.LogError("RandomSoundPlayer.Play: no sounds", this);
				return;
			}
			AudioService.PlaySound(Sounds[Random.Range(0, Sounds.Length)]);
		}
	}
}
