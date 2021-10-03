using UnityEngine;
using UnityEngine.Assertions;

namespace LD49.Config {
	[CreateAssetMenu(menuName = "Custom/LevelsConfig")]
	public sealed class LevelsConfig : ScriptableObject {
		const string Path = "Configs/LevelsConfig";

		static LevelsConfig _instance;
		public static LevelsConfig Instance {
			get {
				if ( !_instance ) {
					_instance = Resources.Load<LevelsConfig>(Path);
					Assert.IsTrue(_instance);
				}
				return _instance;
			}
		}

		public int TotalLevels;
	}
}
