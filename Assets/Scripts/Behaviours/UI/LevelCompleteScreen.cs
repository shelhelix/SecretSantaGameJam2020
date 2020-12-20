using SecretSantaGameJam2020.Behaviours.Common;
using SecretSantaGameJam2020.Utils.CustomAttributes;

using DG.Tweening;
using TMPro;

namespace SecretSantaGameJam2020.Behaviours.UI {
    public class LevelCompleteScreen: BaseGameComponent, IScreen {
        [NotNull] public ScreenTransitionController TransitionController;
        
        [NotNull] public TMP_Text Text;
        
        public float TransitionTime    = 3f;
        public float TextAppearingTime = 0.5f;
        
        Sequence _activeSequence;
        
        public void Show() {
            gameObject.SetActive(true);
            Text.alpha = 0f;
            _activeSequence = TransitionController.CloseTransition(TransitionTime).Append(CreateAppearingTextSeq());
        }

        public void Hide() {
            _activeSequence?.Kill();
            _activeSequence = null;
            gameObject.SetActive(false);
        }

        Sequence CreateAppearingTextSeq() {
            return DOTween.Sequence().Append(Text.DOFade(1f, TextAppearingTime));
        }
    }
}