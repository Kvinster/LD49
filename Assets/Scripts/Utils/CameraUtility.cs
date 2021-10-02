using UnityEngine;

namespace LD49.Utils {
	public sealed class CameraUtility : MonoBehaviour {
		static CameraUtility _instance;
		public static CameraUtility Instance {
			get {
				if ( !_instance ) {
					_instance = FindObjectOfType<CameraUtility>();
				}
				if ( !_instance ) {
					var go = new GameObject("[CameraUtility]");
					_instance = go.AddComponent<CameraUtility>();
				}
				return _instance;
			}
		}

		Camera _camera;

		public Camera Camera {
			get {
				TryUpdateCamera();
				return _camera;
			}
		}

		void Start() {
			TryUpdateCamera();
		}

		void Update() {
			TryUpdateCamera();
		}

		void TryUpdateCamera() {
			if ( _camera ) {
				return;
			}
			_camera = Camera.main;
		}
	}
}
