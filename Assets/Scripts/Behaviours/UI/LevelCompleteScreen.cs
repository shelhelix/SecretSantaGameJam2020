﻿using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

using SecretSantaGameJam2020.Behaviours.Common;
using SecretSantaGameJam2020.Utils.CustomAttributes;

using DG.Tweening;
using TMPro;

namespace SecretSantaGameJam2020.Behaviours.UI {
    public class LevelCompleteScreen: BaseGameComponent, IScreen {
        [NotNull] public ScreenTransitionController TransitionController;
        
        [NotNull] public TMP_Text Text;
        [NotNull] public TMP_Text GoToNextLevelText;
        [NotNull] public Button   Button;
        
        public float TransitionTime    = 3f;
        
        public float TextAppearingTime = 0.5f;
        public float TextEndScale      = 1.5f;
        
        public float RestartTextStartScale    = 0.5f;
        public float RestartTextAppearingTime = 0.2f;
        
        Sequence _activeSequence;
        
        public void Show() {
            gameObject.SetActive(true);
            
            Button.onClick.AddListener(() => {
                Button.onClick.RemoveAllListeners();
                GoToNextLevelText.text = "Loading...";
                SceneManager.LoadSceneAsync("EntryPoint");
            });
            _activeSequence = TransitionController.CloseTransition(TransitionTime).Append(CreateAppearingTextSeq());
        }

        public void Hide() {
            _activeSequence?.Kill();
            _activeSequence = null;
            gameObject.SetActive(false);
        }

        Sequence CreateAppearingTextSeq() {
            Text.alpha              = 0f;
            GoToNextLevelText.alpha = 0f;
            Text.transform.localScale              = Vector3.one;
            GoToNextLevelText.transform.localScale = Vector3.one * RestartTextStartScale ;
            return DOTween.Sequence()
                .Append(Text.DOFade(1f, TextAppearingTime))
                .Join(Text.transform.DOScale(Vector3.one * TextEndScale, TextAppearingTime))
                .Append(GoToNextLevelText.DOFade(1f, RestartTextAppearingTime))
                .Join(GoToNextLevelText.transform.DOScale(Vector3.one, RestartTextAppearingTime));
        }
    }
}