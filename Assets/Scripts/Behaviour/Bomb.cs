using UnityEngine;

using LD49.Manager;

using TMPro;

namespace LD49.Behaviour {
	public class Bomb : MonoBehaviour {
		[SerializeField]
		private float _startExplosionTime;
		[SerializeField]
		private TMP_Text _timerText;
		[SerializeField]
		private SpriteRenderer _renderer;

		private float _explosionTime;
		private bool  _isActive;

		void Start() {
			ActiveBomb();
		}

		private void Update() {
			if ( !_isActive ) {
				return;
			}

			_explosionTime -= Time.deltaTime;
			if ( _explosionTime <= 0f ) {
				BlowUp();
				return;
			}
			_timerText.text = _explosionTime.ToString("F2");

			var progress = _explosionTime / _startExplosionTime;
			_renderer.color = new Color(1, progress, progress);
		}

		void BlowUp() {
			LevelManager.Instance.FailLevel();
			Destroy(gameObject);
		}

		public void ActiveBomb() {
			_timerText.gameObject.SetActive(true);
			_explosionTime = _startExplosionTime;
			_isActive      = true;
		}

		public void BombDeactivated() {
			_isActive       = false;
			_renderer.color = new Color(1, 1, 1);

			_timerText.gameObject.SetActive(false);
		}

		private void OnCollisionEnter2D(Collision2D other)
		{
			if (_isActive && other.gameObject.CompareTag("Platform"))
			{
				transform.parent = other.transform;
			}
		}

		private void OnCollisionExit2D(Collision2D other)
		{
			if (other.gameObject.CompareTag("Platform"))
			{
				transform.parent = null;
			}
		}
	}
}
