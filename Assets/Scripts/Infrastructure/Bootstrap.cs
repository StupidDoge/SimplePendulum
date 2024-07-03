using Assets.Scripts.Logic;
using Assets.Scripts.UI;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private ScoreCounterUI _scoreCounterUI;
    [SerializeField] private GameEndedUI _gameEndedUI;
    [SerializeField] private GameEndedPanel _gameEndedPanel;
    [SerializeField] private CirclesCounter _circlesCounter;

    private ScoreCounter _scoreCounter;
    private ISceneLoader _sceneLoader;

    private void Awake()
    {
        _scoreCounter = new ScoreCounter();
        _scoreCounter.SubscribeToEvents();

        _sceneLoader = new SceneLoader();

        _gameEndedUI.Init(_scoreCounter, _circlesCounter);
        _gameEndedPanel.Init(_sceneLoader);
        _scoreCounterUI.Init(_scoreCounter);
    }

    private void OnDestroy()
    {
        _scoreCounter.UnsubscribeFromEvents();
    }
}
