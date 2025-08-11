using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevel : MonoBehaviour
{
    GameObject Player;
    private bool triggered = false;
    private RespawnController respawnController;
    private SceneController sceneController;
    private Transform flagInactive;
    private Transform flagActive;
    private AudioSource audioSource;
    private GameObject thisScene;
    private Scene scene;

    public GameObject checkpointBurst;

    void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        respawnController = Player.GetComponent<RespawnController>();
        flagInactive = this.gameObject.transform.GetChild(0);
        flagActive = this.gameObject.transform.GetChild(1);
        audioSource = GetComponent<AudioSource>();
        sceneController = GameObject.FindObjectOfType<SceneController>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject == Player && !triggered)
        {
            flagInactive.gameObject.SetActive(false);
            flagActive.gameObject.SetActive(true);
            StartCoroutine(Fireworks());
            audioSource.Play();           
            triggered = true;
        }
    }

    IEnumerator Fireworks()
    {
        Instantiate(checkpointBurst, transform.position + new Vector3(Random.Range(-1f, 1f), Random.Range(1f, 2f), 0), transform.rotation);
        yield return new WaitForSeconds(0.1f);
        Instantiate(checkpointBurst, transform.position + new Vector3(Random.Range(-1f, 1f), Random.Range(1f, 2f), 0), transform.rotation);
        yield return new WaitForSeconds(0.1f);
        Instantiate(checkpointBurst, transform.position + new Vector3(Random.Range(-1f, 1f), Random.Range(1f, 2f), 0), transform.rotation);
        yield return new WaitForSeconds(0.1f);
        Instantiate(checkpointBurst, transform.position + new Vector3(Random.Range(-1f, 1f), Random.Range(1f, 2f), 0), transform.rotation);
        yield return new WaitForSeconds(0.1f);
        Instantiate(checkpointBurst, transform.position + new Vector3(Random.Range(-1f, 1f), Random.Range(1f, 2f), 0), transform.rotation);
        yield return new WaitForSeconds(0.1f);
        Instantiate(checkpointBurst, transform.position + new Vector3(Random.Range(-1f, 1f), Random.Range(1f, 2f), 0), transform.rotation);
        yield return new WaitForSeconds(0.1f);
        Instantiate(checkpointBurst, transform.position + new Vector3(Random.Range(-1f, 1f), Random.Range(1f, 2f), 0), transform.rotation);
        yield return new WaitForSeconds(0.1f);
        Instantiate(checkpointBurst, transform.position + new Vector3(Random.Range(-1f, 1f), Random.Range(1f, 2f), 0), transform.rotation);
        yield return new WaitForSeconds(0.1f);
        Instantiate(checkpointBurst, transform.position + new Vector3(Random.Range(-1f, 1f), Random.Range(1f, 2f), 0), transform.rotation);
        yield return new WaitForSeconds(0.1f);
        Instantiate(checkpointBurst, transform.position + new Vector3(Random.Range(-1f, 1f), Random.Range(1f, 2f), 0), transform.rotation);
        yield return new WaitForSeconds(0.5f);
        scene = SceneManager.GetActiveScene();
        sceneController.UnloadSceneAsync(scene);
    }
}
