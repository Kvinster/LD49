using UnityEngine;

using TMPro;

namespace LD49.Behaviour.UI {
	public sealed class VersionText : MonoBehaviour {
		const string VersionTextTemplate = "Version: {0}";

		public TMP_Text Text;

		void Start() {
			Text.text = string.Format(VersionTextTemplate, Application.version);
		}
	}
}
