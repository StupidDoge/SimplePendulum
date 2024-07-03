using Assets.Scripts.Logic;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class ScoreCounterUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _score;

        private ScoreCounter _scoreCounter;

        public void Init(ScoreCounter scoreCounter)
        {
            _scoreCounter = scoreCounter;
            _scoreCounter.OnScoreChanged += DisplayUpdatedScore;
        }

        private void DisplayUpdatedScore(int score)
        {
            _score.text = score.ToString();
        }

        private void OnDestroy()
        {
            _scoreCounter.OnScoreChanged -= DisplayUpdatedScore;
        }
    }
}