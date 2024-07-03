using Assets.Scripts.Infrastructure.Factory;
using Assets.Scripts.Logic;
using Assets.Scripts.UI;
using UnityEngine;

namespace Assets.Scripts.Infrastructure
{
    public class GameplayBootstrap : MonoBehaviour
    {
        [SerializeField] private Pendulum _pendulum;
        [SerializeField] private ScoreCounterUI _scoreCounterUI;
        [SerializeField] private GameEndedUI _gameEndedUI;
        [SerializeField] private GameEndedPanel _gameEndedPanel;
        [SerializeField] private CirclesCounter _circlesCounter;
        [SerializeField] private CircleSpawner _circleSpawner;
        [SerializeField] private CirclesFactory _circlesFactory;

        private ScoreCounter _scoreCounter;

        private void Awake()
        {
            var gameBootstrap = FindObjectOfType<GameBootstrap>();

            _scoreCounter = new ScoreCounter();
            _scoreCounter.SubscribeToEvents();

            _circleSpawner.Init(_circlesFactory);

            _pendulum.Init(gameBootstrap.InputService, _circlesCounter);
            _gameEndedUI.Init(_scoreCounter, _circlesCounter);
            _gameEndedPanel.Init(gameBootstrap.SceneLoader);
            _scoreCounterUI.Init(_scoreCounter);
        }

        private void OnDestroy()
        {
            _scoreCounter.UnsubscribeFromEvents();
        }
    }
}