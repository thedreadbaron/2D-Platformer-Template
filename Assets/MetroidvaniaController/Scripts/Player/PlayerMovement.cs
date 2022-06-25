using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour {

	public CharacterController2D controller;
	public Animator animator;
	private PlayerControls playerControls;

	public float runSpeed = 40f;

	float horizontalMove = 0f;
	bool jump = false;
	bool dash = false;
	public bool crouch = false;
	Vector2 move;

	//bool dashAxis = false;

	private void Awake()
	{
		playerControls = new PlayerControls();
		playerControls.Player.Move.performed += ctx => move = ctx.ReadValue<Vector2>();
		playerControls.Player.Move.canceled += ctx => move = Vector2.zero;
		playerControls.Player.Jump.performed += ctx => jump = true;
		playerControls.Player.Jump.canceled += ctx => jump = false;
		playerControls.Player.Crouch.performed += ctx => crouch = true;
		playerControls.Player.Crouch.canceled += ctx => crouch = false;
	}

	private void OnEnable()
	{
		playerControls.Enable();
	}

	private void OnDisable()
	{
		playerControls.Disable();
	}
	
	// Update is called once per frame
	void Update () {

		/*horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

		animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

		if (Input.GetKey("space") || Input.GetKey(KeyCode.W))
		{
			jump = true;
		}

		if (Input.GetKeyDown(KeyCode.LeftShift))
		{
			dash = true;
		}

		if (Input.GetAxisRaw("Dash") == 1 || Input.GetAxisRaw("Dash") == -1) //RT in Unity 2017 = -1, RT in Unity 2019 = 1
		{
			if (dashAxis == false)
			{
				dashAxis = true;
				dash = true;
			}
		}
		else
		{
			dashAxis = false;
		}
		*/

	}

	public void OnFall()
	{
		animator.SetBool("IsJumping", true);
	}

	public void OnLanding()
	{
		animator.SetBool("IsJumping", false);
	}

	void FixedUpdate ()
	{
		horizontalMove = move.x * runSpeed;

		animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

		//if (Input.GetKey("space") || Input.GetKey(KeyCode.W))
		//{
		//	jump = true;
		//}

		//if (Input.GetKeyDown(KeyCode.LeftShift))
		//{
			//dash = true;
		//}
		// Move our character
		controller.Move(horizontalMove * Time.fixedDeltaTime, jump, dash, crouch);
		//jump = false;
		dash = false;
	}
}
