using LD49.Utils;

using UnityEngine;

namespace LD49.Behaviour {
	public sealed class Tentacle : MonoBehaviour {
		[Header("Parameters")]
		public int Length;
		public float TargetDist;
		public float SmoothSpeed;
		public float TrailSpeed;
		public float MaxRadius;
		[Header("Dependencies")]
		public Transform OriginPos;
		public LineRenderer LineRenderer;
		public Transform TargetDir;

		Vector2[] _segmentV;

		Vector3[] _segmentPositions;

		void Start() {
			LineRenderer.positionCount = Length;
			_segmentPositions          = new Vector3[Length];
			_segmentV                  = new Vector2[Length];
		}

		void Update() {
			Vector2 mousePos = CameraUtility.Instance.Camera.ScreenToWorldPoint(Input.mousePosition);
			Vector2 targetPos;

			var distance = Vector2.Distance(mousePos, OriginPos.position);
			if ( distance > MaxRadius ) {
				Vector2 originPos = OriginPos.position;
				targetPos = originPos + (mousePos - originPos).normalized * MaxRadius;
			} else {
				targetPos = mousePos;
			}

			_segmentPositions[0] = targetPos;

			for ( var i = 1; i < _segmentPositions.Length - 1; ++i ) {
				_segmentPositions[i] =
					Vector2.SmoothDamp(_segmentPositions[i], _segmentPositions[i - 1] + TargetDir.right * TargetDist,
						ref _segmentV[i], SmoothSpeed);
			}
			_segmentPositions[_segmentPositions.Length - 1] = OriginPos.position;
			LineRenderer.SetPositions(_segmentPositions);
		}
	}
}
