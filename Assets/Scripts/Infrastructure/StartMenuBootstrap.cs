using Assets.Scripts.UI;
using UnityEngine;

namespace Assets.Scripts.Infrastructure
{
    public class StartMenuBootstrap : MonoBehaviour
    {
        [SerializeField] private StartMenuButton _startMenuButton;

        private void Awake()
        {
            var gameBootstrap = FindObjectOfType<GameBootstrap>();

            _startMenuButton.Init(gameBootstrap.SceneLoader);
        }
    }
}