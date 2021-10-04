using UnityEngine;

using System;

using LD49.Manager;
using LD49.Utils;

using TMPro;

namespace LD49.Behaviour {
	public sealed class Bomb : MonoBehaviour {
		[ColorUsage(true, true)]
		public Color NormalColor = Color.white;
		[ColorUsage(true, true)]
		public Color FinalColor = Color.white;
		public HdrSprite SpriteRenderer;

		[SerializeField]
		private float _startExplosionTime;
		[SerializeField]
		private TMP_Text _timerText;

		private float _explosionTime;
		private bool  _isActive;

		public bool IsActive => _isActive;

		public event Action OnBlownUp;

		void Start() {
			ActiveBomb();
		}

		private void Update() {
			if ( !_isActive ) {
				if ( _explosionTime < _startExplosionTime ) {
					_explosionTime = Mathf.Clamp(_explosionTime + Time.deltaTime, 0f, _startExplosionTime);
					SetValue(1f - Mathf.Clamp01(_explosionTime / _startExplosionTime));
				}
				return;
			}

			_explosionTime -= Time.deltaTime;
			if ( _explosionTime <= 0f ) {
				BlowUp();
				return;
			}
			_timerText.text = _explosionTime.ToString("F2");

			SetValue(1f - Mathf.Clamp01(_explosionTime / _startExplosionTime));
		}

		void SetValue(float value) {
			SpriteRenderer.Color = Color.Lerp(NormalColor, FinalColor, Mathf.Clamp01(value));
		}

		public void ActiveBomb() {
			_timerText.gameObject.SetActive(true);
			_explosionTime = _startExplosionTime;
			_isActive      = true;
		}

		public void BombDeactivated() {
			_isActive            = false;
			SpriteRenderer.Color = NormalColor;

			_timerText.gameObject.SetActive(false);
		}

		void BlowUp() {
			LevelManager.Instance.FailLevel();
			OnBlownUp?.Invoke();
			Destroy(gameObject);
		}
	}
}
