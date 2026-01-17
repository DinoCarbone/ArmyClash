using DG.Tweening;
using UnityEngine;

namespace Core.Animations
{
    public class BackAndForthAnimator : IAnimation
    {
        private readonly Transform target;
        private readonly float speed;
        private readonly float distance;
        private Sequence animationSequence;
        private Vector3 localStartPosition;

        public BackAndForthAnimator(Transform target, float speed, float distance = 1f)
        {
            this.target = target;
            this.speed = speed;
            this.distance = distance;
            this.localStartPosition = target.localPosition;
        }

        public void Play()
        {
            StopAnimation();

            float duration = distance / speed;

            animationSequence = DOTween.Sequence();

            animationSequence.Append(
                target.DOLocalMoveZ(localStartPosition.z + distance, duration)
                      .SetEase(Ease.InOutSine)
            );

            animationSequence.Append(
                target.DOLocalMoveZ(localStartPosition.z, duration)
                      .SetEase(Ease.InOutSine)
            );

            animationSequence.Play();
        }

        public void StopAnimation()
        {
            if (animationSequence != null && animationSequence.IsActive())
            {
                animationSequence.Kill();
            }
            animationSequence = null;
            
            target.localPosition = localStartPosition;
        }

        public bool IsAnimating => animationSequence != null && animationSequence.IsPlaying();
    }
}