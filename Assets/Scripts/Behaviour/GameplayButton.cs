using UnityEngine;

using System;
using System.Collections.Generic;

using LD49.Utils;

namespace LD49.Behaviour {
	[ExecuteInEditMode]
	public sealed class GameplayButton : MonoBehaviour {
		[Header("Dependencies")]
		public ColliderNotifier2D Notifier;
		public Collider2D         NotifierCollider;
		public Collider2D[]       IgnoreColliders;
		[Space]
		public Transform OriginPos;
		public SpringJoint2D Joint;

		readonly HashSet<GameObject> _pressables = new HashSet<GameObject>();

		public bool IsPressed { get; private set; }

		public event Action OnPressed;
		public event Action OnReleased;

		void Start() {
			if ( !Application.isPlaying ) {
				return;
			}
			Notifier.OnTriggerEnter += OnNotifierEnter;
			Notifier.OnTriggerExit  += OnNotifierExit;

			foreach ( var ignoreCollider in IgnoreColliders ) {
				Physics2D.IgnoreCollision(NotifierCollider, ignoreCollider, true);
			}

			UpdatePressed();
		}

		void Update() {
			if ( !Application.isPlaying ) {
				if ( !OriginPos || !Joint ) {
					return;
				}
				var anchorPos = (Vector2)OriginPos.position + new Vector2(-0.2f, 0);
				if ( Joint.connectedAnchor != anchorPos ) {
					Joint.connectedAnchor = anchorPos;
#if UNITY_EDITOR
					UnityEditor.EditorUtility.SetDirty(Joint);
#endif
				}
			}
		}

		void OnNotifierEnter(Collider2D other) {
			var go = other.gameObject;
			if ( _pressables.Contains(go) ) {
				return;
			}
			_pressables.Add(go);
			UpdatePressed();
		}

		void OnNotifierExit(Collider2D other) {
			var go = other.gameObject;
			if ( !_pressables.Contains(go) ) {
				return;
			}
			_pressables.Remove(go);
			UpdatePressed();
		}

		void UpdatePressed() {
			var oldPressed = IsPressed;
			IsPressed = _pressables.Count > 0;
			if ( IsPressed == oldPressed ) {
				return;
			}
			if ( IsPressed ) {
				OnPressed?.Invoke();
			} else {
				OnReleased?.Invoke();
			}
		}
	}
}
