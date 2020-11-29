using UnityEngine;

using SecretSantaGameJam2020.Utils;

namespace SecretSantaGameJam2020.Behaviours.Common {
	public class BaseGameComponent : MonoBehaviour {
		protected virtual void Awake() {
			if ( !Application.isPlaying ) {
				return;
			}
			CustomAttributesChecker.CheckAttributes(this);
		}

		protected virtual void OnValidate() {
			if ( (gameObject.hideFlags & HideFlags.DontSaveInEditor) != 0 ) {
				return;
			}
			CustomAttributesChecker.CheckAttributes(this);
		}
	}
}