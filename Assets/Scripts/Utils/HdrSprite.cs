using UnityEngine;

namespace LD49.Utils {
	[ExecuteInEditMode]
	public sealed class HdrSprite : MonoBehaviour {
		static readonly int   HdrColor = Shader.PropertyToID("_HdrColor");

		[ColorUsage(true, true)]
		public Color          StartColor = Color.white;
		public SpriteRenderer SpriteRenderer;

		bool _colorOverriden;

		MaterialPropertyBlock _materialPropertyBlock;
		public Color Color {
			set {
				SpriteRenderer.GetPropertyBlock(MaterialPropertyBlock);
				MaterialPropertyBlock.SetColor(HdrColor, value);
				SpriteRenderer.SetPropertyBlock(MaterialPropertyBlock);

				_colorOverriden = true;
			}
		}

		MaterialPropertyBlock MaterialPropertyBlock => _materialPropertyBlock ?? (_materialPropertyBlock = new MaterialPropertyBlock());

		void Start() {
			if ( !Application.isPlaying ) {
				return;
			}
			if ( !_colorOverriden ) {
				Color = StartColor;
			}
		}

		void Update() {
			if ( !Application.isPlaying && SpriteRenderer ) {
				Color = StartColor;
			}
		}
	}
}
