using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    private GameObject levelSelectScene;

    void Start()
    {
        SceneManager.sceneUnloaded += OnSceneUnloaded;
        levelSelectScene = this.gameObject;
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);       
    }

    public void UnloadSceneAsync(Scene scene)
    {
        SceneManager.UnloadSceneAsync(scene);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        levelSelectScene.SetActive(false);
        SceneManager.SetActiveScene(scene);
    }

    private void OnSceneUnloaded(Scene current)
    {
        levelSelectScene.SetActive(true);
    }

}
