using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncyPlatform : MonoBehaviour
{
    GameObject Player;
    private AudioSource audioSource;
    public float upwardForce = 30f;

    void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        audioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.rigidbody.linearVelocity.y < 0.1f)
        {
            collision.rigidbody.AddForce(new Vector2(0, upwardForce), ForceMode2D.Impulse);
            audioSource.pitch = (Random.Range(0.9f, 1.1f));
            audioSource.Play();
            if (collision.gameObject.CompareTag("Player"))
                {
                Player.GetComponent<CharacterController2D>().isBounced = true;
                Player.GetComponent<CharacterController2D>().canDoubleJump = true;
                }
        }
        
    }
}
