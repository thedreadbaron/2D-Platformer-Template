using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Cinemachine;

public class RespawnController : MonoBehaviour
{
    GameObject Player;
    public CinemachineCamera virtualCamera;
    public Transform currentCheckpoint;
    public AudioSource bgm;
    public ParticleSystem particleExplode;
    public ParticleSystem particleImplode;
    float p = 1.00f;
    bool fade = false;

    void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        bgm.pitch = p;

        if (p > 1f)
        {
            p = 1f;
        }
        else if (fade && p > 0.33f)
        {
            p -= 0.4f * Time.deltaTime;
        }
        else if (!fade && p < 1f)
        {
            p += 1.0f * Time.deltaTime;
        }
    }

   public IEnumerator DeathSequence()
    {
        fade = true;
        Player.GetComponent<PlayerMovement>().playerControls.Disable();
        yield return new WaitForSeconds(2f);
        Player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        Player.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(0,0);
        Player.GetComponent<SpriteRenderer>().enabled = false;
        Player.GetComponent<PlayerAudioEvents>().PlayAudioPoof();
        particleExplode.Play();
        yield return new WaitForSeconds(1f);
        Player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        var delta = Player.transform.position - currentCheckpoint.position;
        Player.transform.position = currentCheckpoint.position;
        virtualCamera.OnTargetObjectWarped(Player.transform, delta);
        virtualCamera.PreviousStateIsValid = false;
        fade = false;
        particleImplode.Play();
        Player.GetComponent<SpriteRenderer>().enabled = true;
        Player.GetComponent<Animator>().SetBool("FadeIn", true);
        if (Player.GetComponent<CharacterController2D>().m_FacingRight == false) Player.GetComponent<CharacterController2D>().Flip();
        yield return new WaitForSeconds(0.5f);
        Player.GetComponent<PlayerMovement>().playerControls.Enable();
        Player.GetComponent<Animator>().SetBool("FadeIn", false);
    }
}
