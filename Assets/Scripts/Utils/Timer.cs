using System;

namespace Utils
{

    public class Timer
    {
        private float duration;
        private float currentTime;
        private bool isRunning;
        private Action<float> onUpdateProgress;
        private Action onComplete;

        public void Start(float duration, Action<float> onUpdateProgress, Action onComplete = null)
        {
            this.duration = duration;
            currentTime = 0f;
            isRunning = true;
            this.onUpdateProgress = onUpdateProgress;
            this.onComplete = onComplete;
        }

        public void Update(float deltaTime)
        {
            if (!isRunning) return;

            currentTime += deltaTime;

            float progress = Math.Clamp(currentTime / duration, 0f, 1f);
            onUpdateProgress?.Invoke(progress);

            if (currentTime >= duration)
            {
                onComplete?.Invoke();
                Stop();
            }
        }

        public void Stop()
        {
            isRunning = false;
        }

        public void Pause()
        {
            isRunning = false;
        }

        public void Resume()
        {
            if (currentTime < duration)
            {
                isRunning = true;
            }
        }

        public void Reset()
        {
            currentTime = 0f;
            isRunning = false;
        }

        public bool IsRunning => isRunning;
        public float CurrentTime => currentTime;
        public float RemainingTime => Math.Max(0f, duration - currentTime);
        public float Progress => Math.Clamp(currentTime / duration, 0f, 1f);
    }
}