using Assets.Scripts.Infrastructure.Services;
using UnityEngine;

namespace Assets.Scripts.Infrastructure
{
    public class GameBootstrap : MonoBehaviour
    {
        public IInputService InputService { get; private set; }
        public ISceneLoader SceneLoader { get; private set; }

        private void Awake()
        {
            DontDestroyOnLoad(this);

            InitializeSceneLoader();
            InitializeInputService();
            SetFrameRateOnMobileDevices();

            SceneLoader.LoadStartScene();
        }

        private void InitializeSceneLoader()
        {
            SceneLoader = new SceneLoader();
        }

        private void InitializeInputService()
        {
            InputService = new InputService();
        }

        private void SetFrameRateOnMobileDevices()
        {
            Application.targetFrameRate = 60;
        }
    }
}