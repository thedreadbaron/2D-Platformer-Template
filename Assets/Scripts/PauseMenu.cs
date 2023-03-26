using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    public GameObject pausedText;
    public AudioLowPassFilter lowPass;
    public AudioSource BGM;
    public PlayerControls playerControls;
    // Start is called before the first frame update
    void Awake()
    {
        playerControls = new PlayerControls();
    }

    private void OnEnable()
	{
		playerControls.Enable();
	}

	private void OnDisable()
	{
		playerControls.Disable();
	}

    // Update is called once per frame
    void OnPause()
    {
        if(Time.timeScale == 1)
        {
            PauseGame();
        }
        else if(Time.timeScale == 0)
        {
            ResumeGame();
        }
    }

    void PauseGame()
    {
        Time.timeScale = 0;
        pausedText.SetActive(true);
        lowPass.enabled = true;
        BGM.volume = 0.25f;
    }
	void ResumeGame()
    {
        Time.timeScale = 1;
        pausedText.SetActive(false);
        lowPass.enabled = false;
        BGM.volume = 0.4f;
    }

    void OnNextSong()
    {
        BGM.Stop();
    }

}
