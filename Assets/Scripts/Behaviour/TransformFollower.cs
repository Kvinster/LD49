using UnityEngine;

namespace LD49.Behaviour {
	public sealed class TransformFollower : MonoBehaviour {
		[Header("Parameters")]
		public bool MoveHorizontal;
		public bool MoveVertical;
		[Header("Dependencies")]
		public Transform Target;

		void LateUpdate() {
			if ( !Target ) {
				return;
			}
			var targetPos = Target.position;
			var newPos    = new Vector3(targetPos.x, targetPos.y, transform.position.z);
			if ( !MoveHorizontal ) {
				newPos.x = transform.position.x;
			}
			if ( !MoveVertical ) {
				newPos.y = transform.position.y;
			}
			transform.position = newPos;
		}
	}
}
