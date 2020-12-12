using UnityEngine;

using SecretSantaGameJam2020.Behaviours.Common;
using SecretSantaGameJam2020.Utils.CustomAttributes;

using DG.Tweening;

namespace SecretSantaGameJam2020.Behaviours.Rooms {
	public class TeleportView : BaseGameComponent {
		[NotNull] public Teleport       Teleport;
		[NotNull] public SpriteRenderer TeleportSprite;
		[NotNull] public CanvasGroup    KeyDescriptionCanvasGroup;

		public Color InactiveColor;
		public Color ActiveColor;

		public float TransitionTime;

		void Start() {
			Teleport.OnTeleportStateChanged += OnTeleportStateChanged;
			SetStateInstant(false);
		}

		void OnDestroy() {
			Teleport.OnTeleportStateChanged -= OnTeleportStateChanged;
		}

		void OnTeleportStateChanged(bool isActive) {
			if ( isActive ) {
				TeleportSprite.DOColor(ActiveColor, TransitionTime);
				KeyDescriptionCanvasGroup.DOFade(1f, TransitionTime);
			}
			else {
				TeleportSprite.DOColor(InactiveColor, TransitionTime);
				KeyDescriptionCanvasGroup.DOFade(0f, TransitionTime);
			}
		}

		void SetStateInstant(bool isActive) {
			if ( isActive ) {
				TeleportSprite.color = ActiveColor;
			}
			else {
				TeleportSprite.color = InactiveColor;
			}
			KeyDescriptionCanvasGroup.alpha = (isActive) ? 1f : 0f;
		}
	}
}