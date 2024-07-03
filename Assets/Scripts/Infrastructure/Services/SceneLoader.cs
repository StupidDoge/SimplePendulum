using UnityEngine.SceneManagement;

namespace Assets.Scripts.Infrastructure.Services
{
    public class SceneLoader : ISceneLoader
    {
        private const string StartSceneName = "StartScene";
        private const string GameplaySceneName = "Gameplay";

        public void LoadGameplayScene()
            => SceneManager.LoadScene(GameplaySceneName);

        public void LoadStartScene()
            => SceneManager.LoadScene(StartSceneName);
    }
}