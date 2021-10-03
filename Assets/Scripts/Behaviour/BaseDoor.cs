using UnityEngine;

using DG.Tweening;

namespace LD49.Behaviour {
	public abstract class BaseDoor : MonoBehaviour {
		[Header("Base Parameters")]
		public bool StartOpen;
		public float AnimTime = 1f;
		[Header("Base Dependencies")]
		public Rigidbody2D Door;
		public Transform ClosedPos;
		public Transform OpenedPos;

		protected bool IsOpened;

		Tween _anim;

		protected virtual void OnDisable() {
			_anim?.Kill();
		}

		protected virtual void Start() {
			if ( StartOpen ) {
				IsOpened      = true;
				Door.position = OpenedPos.position;
			} else {
				IsOpened      = false;
				Door.position = ClosedPos.position;
			}
		}

		public void Open() {
			if ( IsOpened ) {
				return;
			}
			_anim?.Kill();
			_anim = Door.DOMove(OpenedPos.position,
				AnimTime * Vector2.Distance(Door.position, OpenedPos.position) /
				Vector2.Distance(ClosedPos.position, OpenedPos.position));
			IsOpened = true;
		}

		public void Close() {
			if ( !IsOpened ) {
				return;
			}
			_anim?.Kill();
			_anim = Door.DOMove(ClosedPos.position,
				AnimTime * Vector2.Distance(Door.position, ClosedPos.position) /
				Vector2.Distance(OpenedPos.position, ClosedPos.position));
			IsOpened = false;
		}
	}
}
