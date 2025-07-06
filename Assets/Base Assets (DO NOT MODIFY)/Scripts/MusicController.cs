using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip[] audioClips;
    int clipIndex;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        clipIndex = Random.Range(0, audioClips.Length);
    }

    // Update is called once per frame
    void Update()
    {
        if (audioSource.isActiveAndEnabled && !audioSource.isPlaying)
        {
            audioSource.clip = audioClips[clipIndex];
            audioSource.Play();
            if (clipIndex < audioClips.Length - 1) {
                clipIndex++;
            }
            else if (clipIndex >= audioClips.Length - 1) {
                clipIndex = 0;
            }
        }
    }
}
