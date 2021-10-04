using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;

using System;

using LD49.Utils;

namespace LD49.Behaviour {
	public sealed class ArmTip : MonoBehaviour {
		[Header("Parameters")]
		public float MoveSpeed;
		public float     MaxMoveRadius;
		public float     Tolerance;
		public float     GrabRadius;
		public LayerMask GrabLayerMask;
		[Header("Dependencies")]
		public Transform OriginPos;
		public Rigidbody2D   Rigidbody;
		public Rigidbody2D[] ArmRigidbodies;

		Camera _camera;

		Vector2 _mousePos;

		float        _grabbedRbMass;
		FixedJoint2D _grabJoint;

		readonly Collider2D[] _colliders = new Collider2D[3];

		public bool IsGrabbing       => _grabJoint;
		public bool IsGrabbingObject => IsGrabbing && _grabJoint.connectedBody;
		public bool IsGrabbingScene  => IsGrabbing && !_grabJoint.connectedBody;

		public event Action OnGrabbed;
		public event Action OnReleased;

		void Start() {
			_camera = CameraUtility.Instance.Camera;
		}

		void Update() {
			_mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);

			TryGrab();
		}

		void FixedUpdate() {
			var distance = Vector2.Distance(Rigidbody.position, _mousePos);
			if ( Mathf.Approximately(distance, 0f) ) {
				return;
			}
			if ( distance < Tolerance ) {
				Rigidbody.MovePosition(_mousePos);
				// Rigidbody.velocity = Vector2.zero;
				foreach ( var rb in ArmRigidbodies ) {
					rb.velocity = Vector2.zero;
				}
				return;
			}
			if ( Vector2.Distance(OriginPos.position, _mousePos) <= MaxMoveRadius ) {
				Rigidbody.MovePosition(_mousePos);
				return;
			}
			Rigidbody.velocity = (_mousePos - Rigidbody.position).normalized * MoveSpeed;
		}

		void TryGrab() {
			if ( Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject() ) {
				if ( _grabJoint ) {
					return;
				}
				var hits = Physics2D.OverlapCircleNonAlloc(Rigidbody.position, GrabRadius, _colliders, GrabLayerMask);
				for ( var i = 0; i < hits; ++i ) {
					var collider = _colliders[i];
					if ( collider && !collider.isTrigger ) {
						var bomb = collider.GetComponent<Bomb>();
						if ( bomb ) {
							bomb.BombDeactivated();
							var rb = collider.attachedRigidbody;
							Assert.IsTrue(rb);
							_grabJoint               = gameObject.AddComponent<FixedJoint2D>();
							_grabJoint.connectedBody = rb;
							_grabbedRbMass           = rb.mass;
							rb.gravityScale          = 0f;
							rb.mass                  = 0f;
						} else if ( !collider.GetComponent<Rigidbody2D>() ) {
							_grabJoint = gameObject.AddComponent<FixedJoint2D>();
						}
						if ( _grabJoint ) {
							OnGrabbed?.Invoke();
						}
						break;
					}
				}
			} else {
				if ( _grabJoint ) {
					var rb = _grabJoint.connectedBody;
					if ( rb ) {
						rb.gravityScale = 1f;
						rb.mass         = _grabbedRbMass;
						var bomb = rb.GetComponent<Bomb>();
						if ( bomb ) {
							bomb.ActiveBomb();
						}
					}
					Destroy(_grabJoint);
					OnReleased?.Invoke();
				}
			}
		}
	}
}
