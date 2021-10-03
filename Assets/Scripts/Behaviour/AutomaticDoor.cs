using UnityEngine;

using DG.Tweening;

namespace LD49.Behaviour {
	public sealed class AutomaticDoor : BaseDoor {
		[Header("Parameters")]
		public float OpenTime;
		public float ClosedTime;

		Tween _anim;

		protected override void OnDisable() {
			base.OnDisable();
			_anim?.Kill();
		}

		protected override void Start() {
			base.Start();

			if ( IsOpened ) {
				_anim = DOTween.Sequence()
					.AppendInterval(OpenTime)
					.AppendCallback(Close)
					.AppendInterval(AnimTime)
					.AppendInterval(ClosedTime)
					.AppendCallback(Open)
					.AppendInterval(AnimTime)
					.SetLoops(-1);
			} else {
				_anim = DOTween.Sequence()
					.AppendInterval(ClosedTime)
					.AppendCallback(Open)
					.AppendInterval(AnimTime)
					.AppendInterval(OpenTime)
					.AppendCallback(Close)
					.AppendInterval(AnimTime)
					.SetLoops(-1);
			}
		}
	}
}
