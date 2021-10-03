using UnityEngine;

namespace LD49.Behaviour {
	public sealed class ButtonDoor : BaseDoor {
		public enum DoorBehaviour {
			Open  = 0,
			Close = 1,
			None  = 2
		}

		[Header("Parameters")]
		public DoorBehaviour OnButtonPress = DoorBehaviour.Open;
		public DoorBehaviour OnButtonRelease = DoorBehaviour.None;

		[Header("Dependencies")]
		public GameplayButton Button;

		protected override void Start() {
			Button.OnPressed  += OnButtonPressed;
			Button.OnReleased += OnButtonReleased;
		}

		void OnButtonPressed() {
			ApplyBehaviour(OnButtonPress);
		}

		void OnButtonReleased() {
			ApplyBehaviour(OnButtonRelease);
		}

		void ApplyBehaviour(DoorBehaviour behaviour) {
			switch ( behaviour ) {
				case DoorBehaviour.Open: {
					Open();
					break;
				}
				case DoorBehaviour.Close: {
					Close();
					break;
				}
				case DoorBehaviour.None: {
					return;
				}
				default: {
					Debug.LogErrorFormat("ButtonDoor.ApplyBehaviour: unsupported DoorBehaviour '{0}'",
						behaviour.ToString());
					return;
				}
			}
		}
	}
}
