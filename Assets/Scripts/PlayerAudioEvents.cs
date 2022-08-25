using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioEvents : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] footsteps;
    [SerializeField]
    private AudioClip jump;
    [SerializeField]
    private AudioClip dash;
    [SerializeField]
    private AudioClip poof;
    private AudioSource audioSource;
    

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void PlayAudioRun()
    {
        AudioClip clip = footsteps[UnityEngine.Random.Range(0, footsteps.Length)];
        audioSource.pitch = (Random.Range(0.9f, 1.4f));
        audioSource.volume = 1f;
        audioSource.PlayOneShot(clip);
    }
    public void PlayAudioJump()
    {
        audioSource.pitch = (Random.Range(0.9f, 1.1f));
        audioSource.volume = 1f;
        audioSource.PlayOneShot(jump);
    }
    public void PlayAudioDoubleJump()
    {
        audioSource.pitch = (Random.Range(1.2f, 1.4f));
        audioSource.volume = 1f;
        audioSource.PlayOneShot(jump);
    }
    public void PlayAudioDash()
    {
        audioSource.pitch = (Random.Range(0.9f, 1.1f));
        audioSource.volume = 1f;
        audioSource.PlayOneShot(dash);
    }
    public void PlayAudioPoof()
    {
        audioSource.pitch = (Random.Range(0.5f, 0.7f));
        audioSource.volume = 0.8f;
        audioSource.PlayOneShot(poof);
    }
}
