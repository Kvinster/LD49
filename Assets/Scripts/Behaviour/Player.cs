using UnityEngine;

using LD49.Utils;

namespace LD49.Behaviour {
	public sealed class Player : MonoBehaviour {
		[Header("Parameters")]
		public float MaxGrabMoveDistance;
		public float GrabbingSceneMovementSpeed;
		public float MovementForce;
		public float GrabbingMovementSpeed;
		public float JumpForce;
		public float JumpTime;
		public float NormalGravityScale;
		[Space]
		public float GroundCheckRadius;
		public LayerMask GroundCheckLayerMask;
		[Header("Dependencies")]
		public Transform GroundCheckPos;
		public TransformFollower Follower;
		public Rigidbody2D       Rigidbody;
		public ArmTip            ArmTip;

		bool  _isGrounded;
		bool  _isJumping;
		float _jumpTimer;

		Camera _camera;

		Vector2 _prevMousePosScreen;
		Vector2 _prevMousePosWorld;

		public bool IsGrabbingBomb => ArmTip.IsGrabbing;

		void Start() {
			_camera             = CameraUtility.Instance.Camera;
			_prevMousePosScreen = Input.mousePosition;
			_prevMousePosWorld  = _camera.ScreenToWorldPoint(_prevMousePosScreen);
		}

		void FixedUpdate() {
			Rigidbody.gravityScale = ArmTip.IsGrabbingScene ? 0f : NormalGravityScale;
			if ( Follower ) {
				Follower.IsEnabled = !ArmTip.IsGrabbingScene;
			}
			_isGrounded = Physics2D.OverlapCircle(GroundCheckPos.position, GroundCheckRadius, GroundCheckLayerMask);
			TryGrabMove();
			TryMove();
			TryJump();
			_prevMousePosScreen = Input.mousePosition;
			_prevMousePosWorld  = _camera.ScreenToWorldPoint(_prevMousePosScreen);
		}

		void TryGrabMove() {
			if ( !ArmTip.IsGrabbingScene ) {
				return;
			}
			if ( (Vector2) Input.mousePosition == _prevMousePosScreen ) {
				Rigidbody.velocity = Vector2.zero;
				return;
			}
			Vector2 mousePos  = _camera.ScreenToWorldPoint(Input.mousePosition);
			var     targetPos = Rigidbody.position + (_prevMousePosWorld - mousePos);
			var     armPos    = ArmTip.Rigidbody.position;
			if ( Vector2.Distance(armPos, targetPos) > MaxGrabMoveDistance ) {
				targetPos = armPos + (targetPos - armPos).normalized * MaxGrabMoveDistance;
			}
			Rigidbody.velocity = (targetPos - Rigidbody.position).normalized * GrabbingSceneMovementSpeed;
		}

		void TryMove() {
			if ( ArmTip.IsGrabbingScene ) {
				return;
			}
			var hor = Input.GetAxisRaw("Horizontal");
			if ( !Mathf.Approximately(hor, 0f) ) {
				Rigidbody.velocity = new Vector2(
					hor * (ArmTip.IsGrabbingObject ? GrabbingMovementSpeed : MovementForce), Rigidbody.velocity.y);
			} else if ( _isGrounded ) {
				Rigidbody.velocity = new Vector2(0f, Rigidbody.velocity.y);
			}
		}

		void TryJump() {
			if ( ArmTip.IsGrabbing ) {
				_isJumping = false;
				return;
			}
			if ( !_isJumping && Input.GetKey(KeyCode.Space) && _isGrounded ) {
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
