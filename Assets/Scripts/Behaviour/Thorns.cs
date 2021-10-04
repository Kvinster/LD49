using UnityEngine;

using System;

using LD49.Manager;

namespace LD49.Behaviour {
	public class Thorns : MonoBehaviour {
		public event Action OnActivated;

		private void OnTriggerEnter2D(Collider2D other) {
			if ( other.CompareTag("Player") || other.CompareTag("Bomb") ) {
				LevelManager.Instance.FailLevel();
				OnActivated?.Invoke();
			}
		}
	}
}
