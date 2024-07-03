using Assets.Scripts.Infrastructure.Services;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class GameEndedPanel : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _score;
        [SerializeField] private Button _playAgainButton;
        [SerializeField] private Button _quitButton;

        public void Init(ISceneLoader sceneloader)
        {
            _playAgainButton.onClick.AddListener(() => sceneloader.LoadGameplayScene());
            _quitButton.onClick.AddListener(() => sceneloader.LoadStartScene());
        }

        public void SetScore(int score) 
            => _score.text = score.ToString();
    }
}
