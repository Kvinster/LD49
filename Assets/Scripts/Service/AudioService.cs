using UnityEngine;
using UnityEngine.Assertions;

namespace LD49.Service {
	public static class AudioService {
		static GameObject _gameObject;

		static AudioSource _audioSource;

		static GameObject GameObject {
			get {
				if ( !_gameObject ) {
					_gameObject = new GameObject("[AudioService]");
					Object.DontDestroyOnLoad(_gameObject);
				}
				return _gameObject;
			}
		}

		static AudioSource AudioSource {
			get {
				if ( !_audioSource ) {
					_audioSource = GameObject.AddComponent<AudioSource>();
				}
				return _audioSource;
			}
		}

		public static void PlaySound(AudioClip clip, float volumeScale = 1f) {
			Assert.IsNotNull(clip);
			AudioSource.PlayOneShot(clip, volumeScale);
		}
	}
}
