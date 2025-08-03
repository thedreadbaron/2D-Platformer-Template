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

    private PlayerMovement playerMovement;
    private Rigidbody2D rb2D;
    private CapsuleCollider2D capsulecollider;
    private SpriteRenderer[] spriteRenderers;
    private PlayerAudioEvents playerAudioEvents;
    private CharacterController2D characterController2D;
    private Animator animator;

    void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        playerMovement = Player.GetComponent<PlayerMovement>();
        rb2D = Player.GetComponent<Rigidbody2D>();
        capsulecollider = Player.GetComponent<CapsuleCollider2D>();
        spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        playerAudioEvents = Player.GetComponent<PlayerAudioEvents>();
        characterController2D = Player.GetComponent<CharacterController2D>();
        animator = Player.GetComponent<Animator>();
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

   public IEnumerator DeathSequence(float delay)
    {
        fade = true;
        playerMovement.playerControls.Disable();
        yield return new WaitForSeconds(delay);
        rb2D.bodyType = RigidbodyType2D.Kinematic;
        capsulecollider.isTrigger = true;
        rb2D.linearVelocity = new Vector2(0,0);
        foreach (SpriteRenderer spriteRenderer in spriteRenderers)
        {
            if (spriteRenderer != null)
            {
                spriteRenderer.enabled = false;
            }
        }
        playerAudioEvents.PlayAudioPoof();
        particleExplode.Play();
        yield return new WaitForSeconds(1f);
        capsulecollider.isTrigger = false;
        rb2D.bodyType = RigidbodyType2D.Dynamic;
        var delta = Player.transform.position - currentCheckpoint.position;
        Player.transform.position = currentCheckpoint.position;
        virtualCamera.OnTargetObjectWarped(Player.transform, delta);
        virtualCamera.PreviousStateIsValid = false;
        fade = false;
        particleImplode.Play();
        foreach (SpriteRenderer spriteRenderer in spriteRenderers)
        {
            if (spriteRenderer != null)
            {
                spriteRenderer.enabled = true;
            }
        }
        animator.SetBool("FadeIn", true);
        if (characterController2D.m_FacingRight == false) characterController2D.Flip();
        yield return new WaitForSeconds(0.5f);
        playerMovement.playerControls.Enable();
        animator.SetBool("FadeIn", false);
        characterController2D.life = 1f;
    }
}
