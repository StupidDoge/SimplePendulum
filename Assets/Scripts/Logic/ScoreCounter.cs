using System;

namespace Assets.Scripts.Logic
{
    public class ScoreCounter
    {
        public event Action<int> OnScoreChanged;

        private const int LineSize = 3;

        public int Score { get; private set; }

        public void SubscribeToEvents()
            => Circle.OnLineDestroyedWithScore += AddScore;

        public void UnsubscribeFromEvents()
            => Circle.OnLineDestroyedWithScore -= AddScore;

        private void AddScore(int score)
        {
            Score += score * LineSize;
            OnScoreChanged?.Invoke(Score);
        }
    }
}