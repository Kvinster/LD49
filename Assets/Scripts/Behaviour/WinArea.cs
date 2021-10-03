using UnityEngine;

using LD49.Manager;
using LD49.Utils;

namespace LD49.Behaviour {
	public sealed class WinArea : MonoBehaviour {
		[Header("Parameters")]
		public float WinTime = 3f;
		public Color NormalColor = Color.white;
		public Color WinColor    = Color.green;
		[Header("Dependencies")]
		public SpriteRenderer SpriteRenderer;
		public ColliderNotifier2D Notifier;

		Player _player;

		float _timer;

		bool _isCompleted;

		void Start() {
			Notifier.OnTriggerEnter += OnNotifierEnter;
			Notifier.OnTriggerExit  += OnNotifierExit;
		}

		void Update() {
			if ( !_player || !_player.IsGrabbingBomb ) {
				if ( _timer > 0f ) {
					_timer = Mathf.Max(0f, _timer - Time.deltaTime);
					SetValue(_timer / WinTime);
				}
			} else if ( !_isCompleted && _player.IsGrabbingBomb ) {
				_timer = Mathf.Clamp(_timer + Time.deltaTime, 0f, WinTime);
				SetValue(_timer / WinTime);
			}
		}

		void OnNotifierEnter(Collider2D other) {
			var player = other.GetComponentInChildren<Player>();
			if ( player ) {
				_player = player;
			}
		}

		void OnNotifierExit(Collider2D other) {
			if ( other.GetComponentInChildren<Player>() ) {
				_player = null;
			}
		}

		void SetValue(float value) {
			value = Mathf.Clamp01(value);
			if ( SpriteRenderer ) {
				SpriteRenderer.color = Color.Lerp(NormalColor, WinColor, value);
			}
			if ( !_isCompleted && Mathf.Approximately(value, 1f) ) {
				_isCompleted = true;
				LevelManager.Instance.WinLevel();
			}
		}
	}
}
