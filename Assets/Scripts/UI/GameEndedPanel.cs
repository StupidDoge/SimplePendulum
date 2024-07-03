using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class GameEndedPanel : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _score;
        [SerializeField] private Button _playAgainButton;
        [SerializeField] private Button _quitButton;

        private void Awake()
        {
            _playAgainButton.onClick.AddListener(() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex));
            _quitButton.onClick.AddListener(() => Debug.LogError("Quit"));
        }

        public void SetScore(int score) 
            => _score.text = score.ToString();
    }
}
