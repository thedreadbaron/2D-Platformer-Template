using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour {

	public CharacterController2D controller;
	public Animator animator;
	public PlayerControls playerControls;
	public Transform cameraOffset;

	public float runSpeed = 40f;

	float horizontalMove = 0f;
	bool jump = false;
	public bool jumpPress = false;
	public bool jumpBuffer = false;
	public bool buffer_jump_toggle = true;
	public bool buffer_dash_toggle = true;
	public bool crouch = false;
	public bool crouchPress = false;
	public bool dash = false;
	public bool dashPress = false;
	public bool dashBuffer = false;
	Vector2 move;
	Vector3 look;

	//bool dashAxis = false;

	private void Awake()
	{
		playerControls = new PlayerControls();
		playerControls.Player.Move.performed += ctx => move = ctx.ReadValue<Vector2>();
		playerControls.Player.Move.canceled += ctx => move = Vector2.zero;
		playerControls.Player.Look.performed += ctx => look = ctx.ReadValue<Vector2>();
		playerControls.Player.Look.canceled += ctx => look = Vector2.zero;
		playerControls.Player.Jump.performed += ctx => jump = true;
		playerControls.Player.Jump.canceled += ctx => jump = false;
		playerControls.Player.Jump.performed += ctx => jumpPress = true;
		playerControls.Player.Jump.performed += ctx => jumpBuffer = true;
		playerControls.Player.Dash.performed += ctx => dash = true;
		playerControls.Player.Dash.canceled += ctx => dash = false;
		playerControls.Player.Dash.performed += ctx => dashPress = true;
		playerControls.Player.Dash.performed += ctx => dashBuffer = true;
		playerControls.Player.Crouch.performed += ctx => crouch = true;
		playerControls.Player.Crouch.canceled += ctx => crouch = false;
		playerControls.Player.Crouch.performed += ctx => crouchPress = true;
	}

	private void OnEnable()
	{
		playerControls.Enable();
	}

	private void OnDisable()
	{
		playerControls.Disable();
	}

	public void OnFall()
	{
		animator.SetBool("IsJumping", true);
	}

	public void OnLanding()
	{
		animator.SetBool("IsJumping", false);
	}

	void Update()
	{
		cameraOffset.position = this.transform.position + look * 3;
	}

	void FixedUpdate ()
	{

		if (move.x > 0.25f && move.x < 0.75f)
		{
		horizontalMove = move.x * runSpeed;
		}
		else if (move.x < -0.25f && move.x > -0.75f)
		{
		horizontalMove = move.x * runSpeed;
		}
		else if (move.x <= -0.75f || move.x >= 0.75f)
		{
		horizontalMove = (move.x/Mathf.Abs(move.x)) * runSpeed;
		}
		else
		{
		horizontalMove = 0;
		}

		animator.SetFloat("Speed", Mathf.Abs(horizontalMove/55));

		if (jumpPress && buffer_jump_toggle)
		{
			buffer_jump_toggle = false;
			StartCoroutine(JumpBufferHold());
		}

		if (dashPress && buffer_dash_toggle)
		{
			buffer_dash_toggle = false;
			StartCoroutine(DashBufferHold());
		}

		controller.Move(horizontalMove * Time.fixedDeltaTime, jump, jumpPress, jumpBuffer, crouch, crouchPress, dash, dashPress, dashBuffer);
		jumpPress = false;
		crouchPress = false;
		dashPress = false;
	}

	IEnumerator JumpBufferHold()
	{
		yield return new WaitForSeconds(0.1f);
		jumpBuffer = false;
		buffer_jump_toggle = true;
	}

	IEnumerator DashBufferHold()
	{
		yield return new WaitForSeconds(0.15f);
		dashBuffer = false;
		buffer_dash_toggle = true;
	}
}
