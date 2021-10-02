using UnityEngine;

namespace LD49.Behaviour {
	public sealed class Player : MonoBehaviour {
		[Header("Parameters")]
		public float MovementForce;
		public float GrabbingMovementForce;
		public float JumpForce;
		public float JumpTime;
		[Space]
		public float GroundCheckRadius;
		public LayerMask GroundCheckLayerMask;
		[Header("Dependencies")]
		public Transform GroundCheckPos;
		public Rigidbody2D Rigidbody;
		public ArmTip      ArmTip;

		bool  _isJumping;
		float _jumpTimer;

		void FixedUpdate() {
			TryMove();
			TryJump();
		}

		void TryMove() {
			var hor = Input.GetAxisRaw("Horizontal");
			if ( !Mathf.Approximately(hor, 0f) ) {
				Rigidbody.velocity = new Vector2(hor * (ArmTip.IsGrabbing ? GrabbingMovementForce : MovementForce),
					Rigidbody.velocity.y);
			} else {
				Rigidbody.velocity = new Vector2(0f, Rigidbody.velocity.y);
			}
		}

		void TryJump() {
			if ( ArmTip.IsGrabbing ) {
				_isJumping = false;
				return;
			}
			if ( !_isJumping && Input.GetKey(KeyCode.Space) &&
			     Physics2D.OverlapCircle(GroundCheckPos.position, GroundCheckRadius, GroundCheckLayerMask) ) {
				_isJumping         = true;
				_jumpTimer         = JumpTime;
				Rigidbody.velocity = new Vector2(Rigidbody.velocity.x, JumpForce);
			} else if ( _isJumping && Input.GetKey(KeyCode.Space) ) {
				if ( _jumpTimer > 0f ) {
					Rigidbody.velocity =  new Vector2(Rigidbody.velocity.x, JumpForce);
					_jumpTimer         -= Time.fixedDeltaTime;
				} else {
					_isJumping = false;
				}
			} else if ( Input.GetKeyUp(KeyCode.Space) ) {
				_isJumping = false;
			}
		}
	}
}
