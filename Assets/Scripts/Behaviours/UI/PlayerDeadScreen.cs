using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using SecretSantaGameJam2020.Behaviours.Common;
using SecretSantaGameJam2020.State;
using SecretSantaGameJam2020.Utils.CustomAttributes;

using DG.Tweening;
using TMPro;

namespace SecretSantaGameJam2020.Behaviours.UI {
    public class PlayerDeadScreen: BaseGameComponent, IScreen {
        [NotNull] public ScreenTransitionController TransitionController;
        
        [NotNull] public TMP_Text Text;
        [NotNull] public TMP_Text RestartText;
        [NotNull] public Button   Button;
        
        public float TransitionTime    = 3f;
        
        public float TextAppearingTime = 0.5f;
        public float TextEndScale      = 1.5f;
        
        public float RestartTextStartScale    = 0.5f;
        public float RestartTextAppearingTime = 0.2f;
        
        Sequence _activeSequence;
        
        public void Show() {
            gameObject.SetActive(true);
            Button.interactable = false;
            _activeSequence = TransitionController.CloseTransition(TransitionTime)
                .Append(CreateAppearingTextSeq())
                .AppendCallback(() => { Button.interactable = true; });
            Button.onClick.AddListener(() => {
                Button.onClick.RemoveAllListeners();
                RestartText.text = "Loading...";
                GameState.Instance.Reset();
                SceneManager.LoadSceneAsync("EntryPoint");
            });
        }

        public void Hide() {
            Button.onClick.RemoveAllListeners();
            _activeSequence?.Kill();
            _activeSequence = null;
            gameObject.SetActive(false);
        }

        Sequence CreateAppearingTextSeq() {
            Text.alpha        = 0f;
            RestartText.alpha = 0f;
            Text.transform.localScale        = Vector3.one;
            RestartText.transform.localScale = Vector3.one * RestartTextStartScale ;
            return DOTween.Sequence()
                .Append(Text.DOFade(1f, TextAppearingTime))
                .Join(Text.transform.DOScale(Vector3.one * TextEndScale, TextAppearingTime))
                .Append(RestartText.DOFade(1f, RestartTextAppearingTime))
                .Join(RestartText.transform.DOScale(Vector3.one, RestartTextAppearingTime));
        }
    }
}