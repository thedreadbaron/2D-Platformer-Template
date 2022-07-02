using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    private GameObject pausedText;
    //private GameObject clickText;
    bool initialClick = true;

    //public AudioSource bgm;
    // Start is called before the first frame update
    void Start()
    {
        pausedText = transform.GetChild(0).gameObject;
        //clickText = transform.GetChild(1).gameObject;
        //Time.timeScale = 0;
        //clickText.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        Keyboard kb = InputSystem.GetDevice<Keyboard>();
        Mouse mouse = InputSystem.GetDevice<Mouse>();
        if (kb.pKey.wasPressedThisFrame && initialClick)
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

		if (Time.timeScale == 0 && !initialClick)
		{
			if (mouse.leftButton.wasPressedThisFrame) 
            {    
                //bgmEmitter.Play();
                //clickText.SetActive(false);
                initialClick = true;
                ResumeGame();
            }
		}
    }

    void PauseGame ()
    {
        Time.timeScale = 0;
        pausedText.SetActive(true);
        //bgmPause.Pause(true);
    }
	void ResumeGame ()
    {
        Time.timeScale = 1;
        pausedText.SetActive(false);
        //bgmPause.Pause(false);
    }

}
