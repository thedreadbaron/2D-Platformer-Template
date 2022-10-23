using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using UnityEngine.SceneManagement;

public class CharacterController2D : MonoBehaviour
{
	public GameObject debugIndicator;
	public bool _ToggleDoubleJump = false;
	public bool _ToggleDash = false;
	public bool _ToggleWallSlide = false;

	PlayerAudioEvents audioController;
	[SerializeField] private float m_JumpForce = 400f;							// Amount of force added when the player jumps.
	[Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .1f;	// How much to smooth out the movement
	private float ice_smoothing = 0f;
	[SerializeField] private bool m_AirControl = false;							// Whether or not a player can steer while jumping;
	[Range(0, .3f)] [SerializeField] private float m_AirControlSmoothing = .15f;
	[SerializeField] private LayerMask m_WhatIsGround;							// A mask determining what is ground to the character
	[SerializeField] private LayerMask m_WhatIsWall;
	[SerializeField] private Transform m_GroundCheck;							// A position marking where to check if the player is grounded.
	[SerializeField] private Transform m_WallCheck;								//Posicion que controla si el personaje toca una pared

	const float k_GroundedRadius = .15f; // Radius of the overlap circle to determine if grounded
	private bool groundCheckToggle = true;
	public bool m_Grounded;            // Whether or not the player is grounded.
	private Rigidbody2D m_Rigidbody2D;
	private CapsuleCollider2D capsule_collider;
	public bool m_FacingRight = true;  // For determining which way the player is currently facing.
	private Vector3 velocity = Vector3.zero;
	private float limitFallSpeed = 25f; // Limit fall speed
	//private float limitJumpSpeed = 50f;
	private float limitHorizontalSpeed = 25f;
	private Collider2D collider_2D;
	public PhysicsMaterial2D slippery;
	public PhysicsMaterial2D grippy;
	public Vector2 velocityBeforePhysicsUpdate;

	public bool canDoubleJump = true; //If player can double jump
	public bool isBounced = false;
	private bool canJump = true;
	//public bool crouch = false;
	private bool coyote_time = false;
	[SerializeField] private float m_DashForce = 25f;
	private bool canDash = true;
	private bool isDashing = false; //If player is dashing
	private bool isDoubleJumping = false;
	private bool m_IsWall = false; //If there is a wall in front of the player
	private bool isWallSliding = false; //If player is sliding in a wall
	private bool oldWallSlidding = false; //If player is sliding in a wall in the previous frame
	private float prevVelocityX = 0f;
	private bool canCheck = false; //For check if player is wallsliding

	public float life = 10f; //Life of the player
	public bool invincible = false; //If player can die
	private bool canMove = true; //If player can move

	private Animator animator;
	public ParticleSystem particleJumpUp; //Trail particles
	public ParticleSystem particleJumpDown; //Explosion particles

	//private float jumpWallStartX = 0;
	//private float jumpWallDistX = 0; //Distance between player and wall
	//private bool limitVelOnWallJump = false; //For limit wall jump distance with low fps

	[Header("Events")]
	[Space]

	public UnityEvent OnFallEvent;
	public UnityEvent OnLandEvent;

	[System.Serializable]
	public class BoolEvent : UnityEvent<bool> { }

	private void Awake()
	{
		collider_2D = GetComponent<Collider2D>();
		audioController = GetComponent<PlayerAudioEvents>();
		m_Rigidbody2D = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
		capsule_collider = GetComponent<CapsuleCollider2D>();

		if (OnFallEvent == null)
			OnFallEvent = new UnityEvent();

		if (OnLandEvent == null)
			OnLandEvent = new UnityEvent();
	}


	private void FixedUpdate()
	{
		velocityBeforePhysicsUpdate = m_Rigidbody2D.velocity;
		bool wasGrounded = m_Grounded;
		m_Grounded = false;
		//crouch = false;
		// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
		// This can be done using layers instead but Sample Assets will not overwrite your project settings.
		Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject && groundCheckToggle)
				m_Grounded = true;
				canDash = true;
				if (!wasGrounded && m_Grounded)
				{
					OnLandEvent.Invoke();
					isBounced = false;
					if (!m_IsWall && !isDashing) 
						particleJumpDown.Play();
					canDoubleJump = true;
					//if (m_Rigidbody2D.velocity.y < 0f)
						//limitVelOnWallJump = false;				
				}
		}

		m_IsWall = false;

		if (!m_Grounded)
		{
			OnFallEvent.Invoke();
			Collider2D[] collidersWall = Physics2D.OverlapCircleAll(m_WallCheck.position, k_GroundedRadius, m_WhatIsWall);
			for (int i = 0; i < collidersWall.Length; i++)
			{
				if (collidersWall[i].gameObject != null)
				{
					isDashing = false;
					m_IsWall = true;
				}
			}
			prevVelocityX = m_Rigidbody2D.velocity.x;
		}

		if (!m_Grounded && wasGrounded)
		{
			StartCoroutine(Coyote());
		}

		if (!m_Grounded && m_Rigidbody2D.velocity.y > 0)
		{
			groundCheckToggle = false;
		}
		else
		{
			groundCheckToggle = true;
		}

		/*if (limitVelOnWallJump)
		{
			if (m_Rigidbody2D.velocity.y < -0.5f)
				limitVelOnWallJump = false;
			jumpWallDistX = (jumpWallStartX - transform.position.x) * transform.localScale.x;
			if (jumpWallDistX < -0.5f && jumpWallDistX > -1f) 
			{
				canMove = true;
			}
			else if (jumpWallDistX < -1f && jumpWallDistX >= -2f) 
			{
				canMove = true;
				//m_Rigidbody2D.velocity = new Vector2(10f * transform.localScale.x, m_Rigidbody2D.velocity.y);
			}
			else if (jumpWallDistX < -2f) 
			{
				limitVelOnWallJump = false;
				//m_Rigidbody2D.velocity = new Vector2(0, m_Rigidbody2D.velocity.y);
			}
			else if (jumpWallDistX > 0) 
			{
				limitVelOnWallJump = false;
				//m_Rigidbody2D.velocity = new Vector2(0, m_Rigidbody2D.velocity.y);
			}
		}*/
	}


	public void Move(float move, bool jump, bool dash, bool crouch, bool jumpPress)
	{
		if (canMove) {
			if (_ToggleDash && !m_Grounded && crouch && canDash && !isWallSliding && move != 0)
			{
				m_Rigidbody2D.AddForce(new Vector2(transform.localScale.x * m_DashForce, -m_DashForce/3f),ForceMode2D.Impulse);
				particleJumpUp.Play();
				StartCoroutine(DashCooldown());
			}
			if (isDashing && m_Grounded)
			{
				//m_Rigidbody2D.AddForce(new Vector2(transform.localScale.x * m_DashForce, -m_DashForce), ForceMode2D.Force);
				//m_Rigidbody2D.velocity = new Vector2(transform.localScale.x * m_DashForce, -m_DashForce);
				isDashing = false;
			}
			//only control the player if grounded or airControl is turned on
			else if (m_Grounded || m_AirControl )
			{
				if (m_Rigidbody2D.velocity.y < -limitFallSpeed && !isDashing)
					m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, -limitFallSpeed);
				//if (m_Rigidbody2D.velocity.y > limitJumpSpeed)
					//m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, limitJumpSpeed);
				if (m_Rigidbody2D.velocity.x > limitHorizontalSpeed)
					m_Rigidbody2D.velocity = new Vector2(limitHorizontalSpeed, m_Rigidbody2D.velocity.y);
				if (m_Rigidbody2D.velocity.x < -limitHorizontalSpeed)
					m_Rigidbody2D.velocity = new Vector2(-limitHorizontalSpeed, m_Rigidbody2D.velocity.y);
				// Move the character by finding the target velocity
				Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
				// And then smoothing it out and applying it to the character
				if ((m_Grounded && move != 0) || coyote_time || isDoubleJumping)
				m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref velocity, m_MovementSmoothing + ice_smoothing);
				else if (!m_Grounded)
				m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref velocity, m_MovementSmoothing + m_AirControlSmoothing);

				// If the input is moving the player right and the player is facing left...
				if (move > 0 && !m_FacingRight && !isWallSliding)
				{
					// ... flip the player.
					Flip();
				}
				// Otherwise if the input is moving the player left and the player is facing right...
				else if (move < 0 && m_FacingRight && !isWallSliding)
				{
					// ... flip the player.
					Flip();
				}
			}

			if (!jump && !m_Grounded && m_Rigidbody2D.velocity.y > 0 && !isBounced)
			{
				m_Rigidbody2D.gravityScale = 9f;
			}
			else
			{
				m_Rigidbody2D.gravityScale = 4.0f;
			}

			if (collider_2D.sharedMaterial != grippy && move == 0 && m_Grounded)
			{
				collider_2D.sharedMaterial = grippy;
			}
			else if (collider_2D.sharedMaterial != slippery && move != 0 && m_Grounded)
			{
				collider_2D.sharedMaterial = slippery;
			}
			else if (collider_2D.sharedMaterial != slippery && !m_Grounded)
			{
				collider_2D.sharedMaterial = slippery;
			}

			// If the player should jump...
			if ((m_Grounded || coyote_time) && jumpPress && canJump)
			{
				// Add a vertical force to the player.
				animator.SetBool("IsJumping", true);
				animator.SetBool("JumpUp", true);
				m_Grounded = false;
				StartCoroutine(JumpCooldown());
				m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, 0);
				m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
				canDoubleJump = true;
				particleJumpDown.Play();
				particleJumpUp.Play();
				audioController.PlayAudioJump();
			}
			else if (_ToggleDoubleJump && !m_Grounded && jumpPress && canDoubleJump && !isWallSliding)
			{
				canDoubleJump = false;
				StartCoroutine(DoubleJumpCooldown());
				m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, 0);
				m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce / 1.2f));
				animator.SetBool("IsDoubleJumping", true);
				audioController.PlayAudioDoubleJump();
				particleJumpUp.Play();
				canDash = true;
			}

			else if (_ToggleWallSlide && m_IsWall && !m_Grounded)
			{
				if (!oldWallSlidding && m_Rigidbody2D.velocity.y < 0 || isDashing)
				{
					isWallSliding = true;
					m_WallCheck.localPosition = new Vector3(-m_WallCheck.localPosition.x, m_WallCheck.localPosition.y, 0);
					Flip();
					StartCoroutine(WaitToCheck(0.1f));
					canDoubleJump = true;
					animator.SetBool("IsWallSliding", true);
				}
				isDashing = false;

				if (isWallSliding)
				{
					if (move * transform.localScale.x > 0.1f)
					{
						StartCoroutine(WaitToEndSliding());
					}
					if (crouch)
					{
						//normal gravity
					}
					else 
					{
						oldWallSlidding = true;
						m_Rigidbody2D.velocity = new Vector2(-transform.localScale.x * 2, -5);
					}
				}

				if (jumpPress && isWallSliding)
				{
					StartCoroutine(JumpCooldown());
					animator.SetBool("IsJumping", true);
					animator.SetBool("JumpUp", true);
					audioController.PlayAudioJump();
					m_Rigidbody2D.velocity = new Vector2(0f, 0f);
					m_Rigidbody2D.AddForce(new Vector2(transform.localScale.x * m_JumpForce * 0.5f, m_JumpForce));
					//jumpWallStartX = transform.position.x;
					//limitVelOnWallJump = true;
					canDoubleJump = true;
					isWallSliding = false;
					animator.SetBool("IsWallSliding", false);
					oldWallSlidding = false;
					m_WallCheck.localPosition = new Vector3(Mathf.Abs(m_WallCheck.localPosition.x), m_WallCheck.localPosition.y, 0);
					//canMove = false;
				}
				else if (dash && canDash)
				{
					isWallSliding = false;
					animator.SetBool("IsWallSliding", false);
					oldWallSlidding = false;
					m_WallCheck.localPosition = new Vector3(Mathf.Abs(m_WallCheck.localPosition.x), m_WallCheck.localPosition.y, 0);
					canDoubleJump = true;
					StartCoroutine(DashCooldown());
				}
			}
			else if (_ToggleWallSlide && isWallSliding && !m_IsWall && canCheck) 
			{
				isWallSliding = false;
				animator.SetBool("IsWallSliding", false);
				oldWallSlidding = false;
				m_WallCheck.localPosition = new Vector3(Mathf.Abs(m_WallCheck.localPosition.x), m_WallCheck.localPosition.y, 0);
				canDoubleJump = true;
			}
		}
	}


	public void Flip()
	{
		// Switch the way the player is labelled as facing.
		m_FacingRight = !m_FacingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	public void ApplyDamage(float damage, Vector3 position) 
	{
		if (!invincible)
		{
			animator.SetBool("Hit", true);
			life -= damage;
			Vector2 damageDir = Vector3.Normalize(transform.position - position) * 40f ;
			m_Rigidbody2D.velocity = Vector2.zero;
			m_Rigidbody2D.AddForce(damageDir * 10);
			if (life <= 0)
			{
				StartCoroutine(WaitToDead());
			}
			else
			{
				StartCoroutine(Stun(0.25f));
				StartCoroutine(MakeInvincible(1f));
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Ice")
		{
			ice_smoothing = 0.3f;
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.tag == "Ice")
		{
			ice_smoothing = 0f;
		}
	}

	IEnumerator DashCooldown()
	{
		animator.SetBool("IsDashing", true);
		isDashing = true;
		canDash = false;
		yield return new WaitForSeconds(0.1f);
		isDashing = false;
		//yield return new WaitForSeconds(0.4f);
		//canDash = true;
	}

	IEnumerator Stun(float time) 
	{
		canMove = false;
		yield return new WaitForSeconds(time);
		canMove = true;
	}
	IEnumerator MakeInvincible(float time) 
	{
		invincible = true;
		yield return new WaitForSeconds(time);
		invincible = false;
	}
	IEnumerator WaitToMove(float time)
	{
		canMove = false;
		yield return new WaitForSeconds(time);
		canMove = true;
	}

	IEnumerator WaitToCheck(float time)
	{
		canCheck = false;
		yield return new WaitForSeconds(time);
		canCheck = true;
	}

	IEnumerator WaitToEndSliding()
	{
		yield return new WaitForSeconds(0.1f);
		m_WallCheck.localPosition = new Vector3(Mathf.Abs(m_WallCheck.localPosition.x), m_WallCheck.localPosition.y, 0);
		yield return new WaitForSeconds(0.02f);
		canDoubleJump = true;
		isWallSliding = false;
		animator.SetBool("IsWallSliding", false);
		oldWallSlidding = false;
		
	}

	IEnumerator WaitToDead()
	{
		animator.SetBool("IsDead", true);
		canMove = false;
		invincible = true;
		GetComponent<Attack>().enabled = false;
		yield return new WaitForSeconds(0.4f);
		m_Rigidbody2D.velocity = new Vector2(0, m_Rigidbody2D.velocity.y);
		yield return new WaitForSeconds(1.1f);
		SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
	}

	IEnumerator Coyote()
	{
		coyote_time = true;
		yield return new WaitForSeconds(0.10f);
		coyote_time = false;
	}

	IEnumerator JumpCooldown()
	{
		canJump = false;
		yield return new WaitForSeconds(0.3f);
		canJump = true;
	}

	IEnumerator DoubleJumpCooldown()
	{
		isDoubleJumping = true;
		yield return new WaitForSeconds(0.05f);
		isDoubleJumping = false;
	}
}
