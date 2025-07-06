using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonToggleController : MonoBehaviour
{
    public UnityEvent ButtonToggleOn;
    public UnityEvent ButtonToggleOff;
    public GameObject button;
    AudioSource audioSource;

    bool open = false;
    bool triggered = false;
    float height;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        height = button.transform.localPosition.x;

        if (triggered && height >= 0.1)
        {
            triggered = false;
        }

        if (!triggered && !open && height < 0.05)
        {
            ButtonToggleOn.Invoke();
            audioSource.Play();
            open = true;
            triggered = true;
        }
        else if (!triggered && open && height < 0.05)
        {
            ButtonToggleOff.Invoke();
            audioSource.Play();
            open = false;
            triggered = true;
        }
    }
}
