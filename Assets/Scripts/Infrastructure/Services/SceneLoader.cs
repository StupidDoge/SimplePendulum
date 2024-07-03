using UnityEngine.SceneManagement;

public class SceneLoader : ISceneLoader
{
    private const string StartSceneName = "StartScene";
    private const string GameplaySceneName = "Gameplay";

    public void LoadGameplayScene()
        => SceneManager.LoadScene(GameplaySceneName);

    public void LoadStartScene()
        => SceneManager.LoadScene(StartSceneName);
}
