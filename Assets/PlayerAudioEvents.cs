using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioEvents : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] footsteps;
    [SerializeField]
    private AudioClip jump;
    private AudioSource audioSource;
    

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void PlayAudioRun()
    {
        AudioClip clip = footsteps[UnityEngine.Random.Range(0, footsteps.Length)];
        audioSource.pitch = (Random.Range(0.9f, 1.4f));
        audioSource.PlayOneShot(clip);
    }
    public void PlayAudioJump()
    {
        audioSource.pitch = (Random.Range(0.9f, 1.1f));
        audioSource.PlayOneShot(jump);
    }
}
