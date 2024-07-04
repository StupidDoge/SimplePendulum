using Assets.Scripts.Logic;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class GameEndedUI : MonoBehaviour
    {
        [SerializeField] private RectTransform _container;
        [SerializeField] private GameEndedPanel _gameEndedPanel;

        private ScoreCounter _scoreCounter;
        private CirclesCounter _circlesCounter;

        public void Init(ScoreCounter scoreCounter, CirclesCounter circlesCounter)
        {
            _scoreCounter = scoreCounter;
            _circlesCounter = circlesCounter;

            _circlesCounter.OnFieldFullfilled += ShowGameEndedPanel;
        }

        private void ShowGameEndedPanel()
        {
            _container.gameObject.SetActive(true);
            _gameEndedPanel.SetScore(_scoreCounter.Score);
        }

        private void OnDestroy()
        {
            _circlesCounter.OnFieldFullfilled -= ShowGameEndedPanel;
        }
    }
}