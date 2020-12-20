using UnityEngine;

using SecretSantaGameJam2020.Behaviours.Common;
using SecretSantaGameJam2020.Utils.CustomAttributes;

using DG.Tweening;
using TMPro;

namespace SecretSantaGameJam2020.Behaviours.UI {
    public class PlayerDeadScreen: BaseGameComponent, IScreen {
        [NotNull] public ScreenTransitionController TransitionController;
        
        [NotNull] public TMP_Text Text;
        
        public float TransitionTime    = 3f;
        public float TextAppearingTime = 0.5f;
        public float TextEndScale      = 1.5f;
        
        Sequence _activeSequence;
        
        public void Show() {
            gameObject.SetActive(true);
            _activeSequence = TransitionController.CloseTransition(TransitionTime).Append(CreateAppearingTextSeq());
        }

        public void Hide() {
            _activeSequence?.Kill();
            _activeSequence = null;
            gameObject.SetActive(false);
        }

        Sequence CreateAppearingTextSeq() {
            Text.alpha = 0f;
            Text.transform.localScale = Vector3.one;
            return DOTween.Sequence().Append(Text.DOFade(1f, TextAppearingTime)).Join(Text.transform.DOScale(Vector3.one * TextEndScale, TextAppearingTime));
        }
    }
}