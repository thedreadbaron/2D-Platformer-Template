using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToMenu : MonoBehaviour
{
    private Scene scene;
    private SceneController sceneController;

    void Awake()
    {
        sceneController = GameObject.FindObjectOfType<SceneController>();
    }

    public void ReturnToMenu()
    {
        scene = SceneManager.GetActiveScene();
        sceneController.UnloadSceneAsync(scene);
        Time.timeScale = 1;
    }
}
