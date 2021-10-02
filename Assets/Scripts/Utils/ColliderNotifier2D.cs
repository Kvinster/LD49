using UnityEngine;

using System;

namespace LD49.Utils {
	public sealed class ColliderNotifier2D : MonoBehaviour {
		public event Action<Collider2D> OnTriggerEnter;
		public event Action<Collider2D> OnTriggerStay;
		public event Action<Collider2D> OnTriggerExit;

		void OnTriggerEnter2D(Collider2D other) {
			OnTriggerEnter?.Invoke(other);
		}

		void OnTriggerStay2D(Collider2D other) {
			OnTriggerStay?.Invoke(other);
		}

		void OnTriggerExit2D(Collider2D other) {
			OnTriggerExit?.Invoke(other);
		}
	}
}
