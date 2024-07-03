using Assets.Scripts.Logic;
using Assets.Scripts.UI;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private ScoreCounterUI _scoreCounterUI;
    [SerializeField] private GameEndedUI _gameEndedUI;
    [SerializeField] private CirclesCounter _circlesCounter;

    private ScoreCounter _scoreCounter;

    private void Awake()
    {
        _scoreCounter = new ScoreCounter();
        _scoreCounter.SubscribeToEvents();

        _gameEndedUI.Init(_scoreCounter, _circlesCounter);
        _scoreCounterUI.Init(_scoreCounter);
    }

    private void OnDestroy()
    {
        _scoreCounter.UnsubscribeFromEvents();
    }
}
