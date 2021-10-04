using UnityEngine;

namespace LD49.Behaviour.Sound {
	public sealed class ArmTipSoundPlayer : MonoBehaviour {
		public ArmTip            ArmTip;
		public RandomSoundPlayer GrabSoundPlayer;

		void Start() {
			ArmTip.OnClawClosed += OnClawClosed;
		}

		void OnClawClosed() {
			GrabSoundPlayer.Play();
		}
	}
}
