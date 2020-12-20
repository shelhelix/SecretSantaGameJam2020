using System;
using DG.Tweening;
using SecretSantaGameJam2020.Behaviours.Common;
using SecretSantaGameJam2020.Utils.CustomAttributes;
using UnityEngine;

namespace SecretSantaGameJam2020.Behaviours.UI {
    public class ScreenTransitionController : BaseGameComponent {
        const string ProgressProperty = "_Progress";
        
        [NotNull] public SpriteRenderer TransitionSprite;

        readonly int _progressPropertyId = Shader.PropertyToID(ProgressProperty);
        
        MaterialPropertyBlock _propertyBlock;

        protected override void Awake() {
            base.Awake();
            _propertyBlock = new MaterialPropertyBlock();
            TransitionSprite.GetPropertyBlock(_propertyBlock);
            SetProgress(1f);
        }

        void Update() {
            var pos = Camera.main.transform.position;
            transform.position = new Vector3(pos.x, pos.y, 0f);
        }

        public Sequence ChangeSceneTransition(float transitionTime, Action sceneChangeAction) {
            var closingTrans = CloseTransition(transitionTime / 2);
            var openTrans    = OpenTransition(transitionTime / 2);
            var resultTrans  = DOTween.Sequence();
            return resultTrans
                .Append(closingTrans)
                .AppendCallback(() => sceneChangeAction?.Invoke())
                .Append(openTrans);
        } 
        
        public Sequence OpenTransition(float transitionTime) {
            return Transition(0f, 1f, transitionTime);
        }
        
        public Sequence CloseTransition(float transitionTime) {
            return Transition(1f, 0f, transitionTime);
        }

        public Sequence Transition(float startValue, float endValue, float duration) {
            var seq = DOTween.Sequence();
            var progress = startValue;
            seq.Append(DOTween.To(() => progress, x => {
                progress = x;
                SetProgress(progress);
            }, endValue, duration));
            return seq;
        }

        void SetProgress(float progress) {
            _propertyBlock.SetFloat(_progressPropertyId, progress);
            TransitionSprite.SetPropertyBlock(_propertyBlock);
        }
    }
}