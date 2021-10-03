using UnityEngine;

using LD49.Manager;

namespace LD49.Behaviour {
	public class FallTrigger : MonoBehaviour {
		private void OnTriggerEnter2D(Collider2D other) {
			if ( other.CompareTag("Player") || other.CompareTag("Bomb") ) {
				LevelManager.Instance.FailLevel();
			}
		}
	}
}
