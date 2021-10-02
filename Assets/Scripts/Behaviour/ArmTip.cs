using UnityEngine;

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

		readonly Collider2D[] _overlapChecks = new Collider2D[10];

		public bool IsGrabbing => _grabJoint;

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
			if ( Input.GetMouseButton(0) ) {
				if ( _grabJoint ) {
					return;
				}
				var overlap = Physics2D.OverlapCircleNonAlloc(Rigidbody.position, GrabRadius, _overlapChecks, GrabLayerMask);
				for ( var i = 0; i < overlap; ++i ) {
					var collider = _overlapChecks[i];
					var bomb     = collider.GetComponent<Bomb>();
					if ( !bomb ) {
						continue;
					}
					bomb.BombDeactivated();
					var rb       = collider.attachedRigidbody;
					if ( rb ) {
						_grabJoint               = gameObject.AddComponent<FixedJoint2D>();
						_grabJoint.connectedBody = rb;
						_grabbedRbMass           = rb.mass;
						rb.gravityScale          = 0f;
						rb.mass                  = 0f;
						break;
					}
				}
			} else {
				if ( _grabJoint ) {
					var bomb = _grabJoint.connectedBody.GetComponent<Bomb>();
					bomb.ActiveBomb();
					var rb   = _grabJoint.connectedBody;
					if ( rb ) {
						rb.gravityScale = 1f;
						rb.mass         = _grabbedRbMass;
					}
					Destroy(_grabJoint);
				}
			}
		}
	}
}
