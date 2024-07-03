using Assets.Scripts.Infrastructure.Services;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class StartMenuButton : MonoBehaviour
    {
        [SerializeField] private Button _playButton;

        public void Init(ISceneLoader sceneLoader) 
            => _playButton.onClick.AddListener(() => sceneLoader.LoadGameplayScene());
    }
}