using UnityEngine;

namespace LD49.Behaviour {
	public sealed class TransformFollower : MonoBehaviour {
		[Header("Parameters")]
		public float FollowSpeed;
		public bool MoveHorizontal;
		public bool MoveVertical;
		[Header("Dependencies")]
		public Transform Target;

		public bool IsEnabled { get; set; } = true;

		void Start() {
			if ( !Target ) {
				Debug.LogError("TransformFollower.Start: no Target", this);
				return;
			}
			UpdatePosition(true);
		}

		void LateUpdate() {
			if ( !Target || !IsEnabled ) {
				return;
			}
			UpdatePosition();
		}

		void UpdatePosition(bool force = false) {
			var targetPos = Target.position;
			var newPos    = new Vector3(targetPos.x, targetPos.y, transform.position.z);
			if ( !MoveHorizontal ) {
				newPos.x = transform.position.x;
			}
			if ( !MoveVertical ) {
				newPos.y = transform.position.y;
			}
			if ( force ) {
				transform.position = newPos;
			} else {
				if ( Vector2.Distance(transform.position, newPos) < FollowSpeed * Time.deltaTime ) {
					transform.position = newPos;
				} else {
					transform.Translate((newPos - transform.position).normalized * FollowSpeed * Time.deltaTime);
				}
			}
		}
	}
}
