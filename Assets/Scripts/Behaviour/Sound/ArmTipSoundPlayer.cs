using UnityEngine;

namespace LD49.Behaviour.Sound {
	public sealed class ArmTipSoundPlayer : MonoBehaviour {
		public ArmTip            ArmTip;
		public RandomSoundPlayer GrabSoundPlayer;
		public RandomSoundPlayer ReleaseSoundPlayer;

		void Start() {
			ArmTip.OnGrabbed  += OnArmGrabbed;
			ArmTip.OnReleased += OnArmReleased;
		}

		void OnArmGrabbed() {
			GrabSoundPlayer.Play();
		}

		void OnArmReleased() {
			ReleaseSoundPlayer.Play();
		}
	}
}
