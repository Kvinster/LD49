using UnityEngine;

using System.Collections.Generic;

using LD49.Utils;

namespace LD49.Behaviour {
	public sealed class MovingPlatform : MonoBehaviour {
		[Header("Parameters")]
		public float MovementSpeed;
		[Header("Dependencies")]
		public ColliderNotifier2D Notifier;
		public Rigidbody2D Rigidbody;
		public Transform   StartPos;
		public Transform   EndPos;

		readonly HashSet<Rigidbody2D> _rigidbodies = new HashSet<Rigidbody2D>();

		bool _invert;

		void Start() {
			Notifier.OnTriggerEnter += OnNotifierEnter;
			Notifier.OnTriggerExit  += OnNotifierExit;
		}

		void FixedUpdate() {
			var shiftMag = MovementSpeed * Time.fixedDeltaTime;
			var a        = _invert ? EndPos : StartPos;
			var b        = _invert ? StartPos : EndPos;

			var distance = Vector2.Distance(Rigidbody.position, b.position);
			if ( distance < shiftMag ) {
				shiftMag = distance;
				_invert  = !_invert;
			}
			var dir   = ((Vector2)(b.position - a.position)).normalized;
			var shift = dir * shiftMag;
			Rigidbody.position = (Rigidbody.position + shift);
			foreach ( var rb in _rigidbodies ) {
				rb.position += shift;
			}
		}

		void OnNotifierEnter(Collider2D other) {
			var rb = other.GetComponent<Rigidbody2D>();
			if ( rb && (rb != Rigidbody) ) {
				_rigidbodies.Add(rb);
			}
		}

		void OnNotifierExit(Collider2D other) {
			var rb = other.GetComponent<Rigidbody2D>();
			if ( rb ) {
				_rigidbodies.Remove(rb);
			}
		}
	}
}
