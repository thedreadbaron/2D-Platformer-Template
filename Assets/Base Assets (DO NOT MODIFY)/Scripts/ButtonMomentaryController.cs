using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonMomentaryController : MonoBehaviour
{
    public UnityEvent ButtonMomentaryOn;
    public UnityEvent ButtonMomentaryOff;
    public GameObject button;
    AudioSource audioSource;

    bool triggered = false;
    float height;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        height = button.transform.localPosition.x;

        if (triggered && height >= 0.05)
        {
            ButtonMomentaryOff.Invoke();
            //audioSource.Play();
            triggered = false;
        }

        if (!triggered && height < 0.05)
        {
            ButtonMomentaryOn.Invoke();
            audioSource.Play();
            triggered = true;
        }
    }
}
