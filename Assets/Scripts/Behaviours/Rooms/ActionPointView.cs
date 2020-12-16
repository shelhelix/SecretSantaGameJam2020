using UnityEngine;

using SecretSantaGameJam2020.Behaviours.Common;
using SecretSantaGameJam2020.Utils.CustomAttributes;

using DG.Tweening;

namespace SecretSantaGameJam2020.Behaviours.Rooms {
	public class ActionPointView : BaseGameComponent {
		[NotNull] public ActionPoint    ActionPoint;
		[NotNull] public SpriteRenderer PointSprite;
		[NotNull] public CanvasGroup    KeyDescriptionCanvasGroup;

		public Color InactiveColor;
		public Color ActiveColor;

		public float TransitionTime;

		void Start() {
			ActionPoint.OnTeleportStateChanged += OnTeleportStateChanged;
			SetStateInstant(false);
		}

		void OnDestroy() {
			ActionPoint.OnTeleportStateChanged -= OnTeleportStateChanged;
		}

		void OnTeleportStateChanged(bool isActive) {
			if ( isActive ) {
				PointSprite.DOColor(ActiveColor, TransitionTime);
				KeyDescriptionCanvasGroup.DOFade(1f, TransitionTime);
			}
			else {
				PointSprite.DOColor(InactiveColor, TransitionTime);
				KeyDescriptionCanvasGroup.DOFade(0f, TransitionTime);
			}
		}

		void SetStateInstant(bool isActive) {
			if ( isActive ) {
				PointSprite.color = ActiveColor;
			}
			else {
				PointSprite.color = InactiveColor;
			}
			KeyDescriptionCanvasGroup.alpha = (isActive) ? 1f : 0f;
		}
	}
}