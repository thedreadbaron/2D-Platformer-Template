using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    private GameObject levelSelectScene;
    private Animator[] animators;

    void Start()
    {
        SceneManager.sceneUnloaded += OnSceneUnloaded;
        levelSelectScene = this.gameObject;
        animators = GetComponentsInChildren<Animator>();
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
        foreach (Animator animator in animators)
        {
            animator.Rebind();
            animator.Update(0f);
        }
        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);       
    }

    public void UnloadSceneAsync(Scene scene)
    {
        SceneManager.UnloadSceneAsync(scene);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (levelSelectScene != null)
        {
            levelSelectScene.SetActive(false);
        }      
        SceneManager.SetActiveScene(scene);
    }

    private void OnSceneUnloaded(Scene current)
    {
        levelSelectScene.SetActive(true);
    }

}
