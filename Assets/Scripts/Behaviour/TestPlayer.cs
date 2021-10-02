using UnityEngine;

namespace LD49.Behaviour {
	public sealed class TestPlayer : MonoBehaviour {
		public float MovementSpeed;

		void Update() {
			var hor = Input.GetAxisRaw("Horizontal");
			var ver = Input.GetAxisRaw("Vertical");
			transform.Translate(new Vector2(hor, ver).normalized * MovementSpeed * Time.deltaTime);
		}
	}
}
