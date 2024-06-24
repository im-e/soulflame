using UnityEngine;
using System.Collections;

public class OverworldPlayerController : MonoBehaviour
{

	

	[Header("Components")]
	public Animator animator;
	public LayerMask jumpLayer;
	private OverworldController overworldController;
	private GroundState groundState;
	private Rigidbody2D rb;
	private GameObject upToEnter;
	[Header("Player Configuration")]
	public bool charFacingLeft = false;
	private bool portalHovered = false;
	private bool spriteFacingLeft;
	private bool rotationNeeded;
	[Header("Movement Stats")]
	private float charSpeed = 12;
	private float charAccel = 8;
	private float charAirAccel = 4;
	private float charJumpForce = 8;
	private float charJumpDuration = 0.375f;
	private float charDashSpeed = 20;
	private float charDashDecel = 1;
	private float charDashJumpBufffer = 0.2f;    //characters minimum jump time before they can dash
	private bool stoppedMoving = false;
	[Header("Jumping Debug")]
	private float charJumpDurationLeft;      //How much jump time the player has left
	private float charJumpTime = 0;                  //How much time has passed since the player jumped
	private bool charJumped = false;                   //Is the character jumping
	private bool charJumping = false;            //Is the character variable jumping
	private float charJumpDelay = 0.2f;
	private float charJumpDelayTimer;
	private bool charWallSliding = false;
	private int charWallDirection = 0;
	[Header("Dashing Debug")]
	private float charPreDashVelocity;          //characters X velocity before a dash
	private float charDashDir;                  //characters X direction for the dash
	private bool charDashAvailable = false;     //does the character have a dash
	private bool charDashing = false;           //is the character dashing
	//inputs
	private float inputHorizontal;
	private float inputJumpDown;
	private float inputJumpHeld;
	private float inputDash;
	private float inputSubmit;

	void Start()
	{
		overworldController = GetComponent<OverworldController>();
		//get the rigidbody component
		rb = GetComponent<Rigidbody2D>();
		//create a ground state class
		groundState = new GroundState(transform.gameObject, jumpLayer);
		//turn the sprite to face the player in the chosen direction
		if (!charFacingLeft) transform.localRotation = Quaternion.Euler(0, 180, 0);
		//update the sprite direction control bool
		spriteFacingLeft = charFacingLeft;
		upToEnter = GameObject.Find("Up To Enter");
		upToEnter.SetActive(false);
	}


	void Update()
	{
		if (portalHovered) upToEnter.transform.position =
				new Vector3(transform.position.x + 0.9f, transform.position.y, transform.position.z);

		//Handle input
		inputHorizontal = Input.GetAxis("Horizontal");

		//Jump Input
		if (Input.GetButtonDown("Jump"))
		{
			inputJumpDown = 1;
			charJumpDelayTimer = Time.time + charJumpDelay;
		}
		if (Input.GetButton("Jump")) 		inputJumpHeld = 1;
		if (Input.GetButtonUp("Jump"))		inputJumpHeld = 0;

		//Dash Input
		if (Input.GetButtonDown("Dash")) inputDash = 1;

		//Enter Input
		if (Input.GetButton("Submit")) inputSubmit = 1;
		else inputSubmit = 0;
	}

	void FixedUpdate()
	{
		if (overworldController.playerHasControl)
		{
			//Sprite Rotation
			//Sprite desired rotation based on input
			if (inputHorizontal != 0)
			{
				if (inputHorizontal < 0) charFacingLeft = true;
				else if ( inputHorizontal > 0) charFacingLeft = false;
			}
			//Check if a rotation needed
			if (!spriteFacingLeft && charFacingLeft) rotationNeeded = true; //if facing left is false and player is going left a rotation is needed
			else if (spriteFacingLeft && !charFacingLeft) rotationNeeded = true; //if facing left is true and player is going right a rotation is needed
			else rotationNeeded = false;//otherwise the player is rotated the right way
										//if there is a rotation needed
			if (rotationNeeded && charFacingLeft) //rotate the sprite to the left and update the bool 
			{ transform.localRotation = Quaternion.Euler(0, 0, 0); spriteFacingLeft = true; }
			else if (rotationNeeded && !charFacingLeft) //rotate the sprite to the right and update the bool 
			{ transform.localRotation = Quaternion.Euler(0, 180, 0); spriteFacingLeft = false; }

			//Wall Slide Check
			if (!groundState.isGround() && groundState.isWall()) charWallSliding = true;
			else charWallSliding = false;

			//Dashing
			if (groundState.isGround()) charDashAvailable = true;
			if (!groundState.isGround() && inputDash == 1 && charDashAvailable && !charDashing && charJumpTime >= charDashJumpBufffer)
			//Dash
			{
				animator.SetBool("Dashing", charDashing);   //signal dashing has begun for animator
				charDashing = true;                     //player is dashing
				charDashAvailable = false;              //disable the player from dashing again

				if (charWallSliding)
				{                   //take horizontal direction - if they are wallsliding - perform opposite dash dir
					if (charFacingLeft && inputHorizontal != -1) charDashDir = 1;//if left, dash right
					else if (!charFacingLeft && inputHorizontal != 1) charDashDir = -1;
				}
				else
				{                               //if not wall sliding
					if (charFacingLeft) charDashDir = -1;//if left dash left
					else charDashDir = 1;               //if right dash right
				}

				charJumping = false;                        //disable the player variable jumping
				charPreDashVelocity = rb.velocity.x;        //save the players velocity before the dash
				rb.gravityScale = 0;                        //disable gravity
				rb.velocity = new Vector2(charDashDir * charDashSpeed, 0);
			}
			else if (charDashing) //If the player is in the middle of a dash
								  //Continue dashing with dash deceleration until the speed reaches the speed of the player before the dash
			{
				rb.velocity = new Vector2(rb.velocity.x - (charDashDir * charDashDecel), rb.velocity.y);
				if (charDashDir == 1)
				{                                                                   //dash is to the right					
					if (rb.velocity.x <= charPreDashVelocity || rb.velocity.x <= -charPreDashVelocity)  //if the player has slowed to their pre-dash velocity
					{ charDashing = false; }                                        //return the gravity to normal
				}
				else if (charDashDir == -1)
				{                                                           //dash is to the left
					if (rb.velocity.x >= charPreDashVelocity || rb.velocity.x >= -charPreDashVelocity)  //if the player has slowed to their pre-dash velocity
					{ charDashing = false; }                                        //return the gravity to normal
				}

				if (!charDashing)
				{
					charDashing = false;
					rb.gravityScale = 2;
				}
			}
			else //if player has not started a dash or is not dashing
				 //Then apply normal movement- walking, jumping etc..
			{
				//reset dash values
				charDashDir = 0;
				charPreDashVelocity = 0;

				//Walking Movement
				//Move player by the speed value multiplied by ground or air acceleration
				rb.AddForce(new Vector2(((inputHorizontal * charSpeed) - rb.velocity.x) * (groundState.isGround() ? charAccel : charAirAccel), 0));
				//Stop player if input.x is 0 (and grounded)
				rb.velocity = new Vector2((inputHorizontal == 0 && groundState.isGround()) ? 0 : rb.velocity.x, rb.velocity.y);

				//Jumping
				//See if the player has jumped
				if ((charJumpDelayTimer > Time.time || inputJumpDown == 1) && (groundState.isGround() || groundState.isWall()))
				//if jump is pressed for the first time and touching the ground or wall
				{
					charJumping = true;         //character has started jumping
					charJumpDurationLeft = charJumpDuration;                //reset the jump time

					rb.velocity = new Vector2(rb.velocity.x, charJumpForce);        //make the character jump

					if (!groundState.isGround())                                //if player is not touching the ground (wall jump)
					{
						charWallDirection = groundState.wallDirection();
						if (charWallDirection == -1) charFacingLeft = false; //left
						else if (charWallDirection == 1) charFacingLeft = true; //right
						rb.velocity = new Vector2(-charWallDirection * charSpeed * 0.75f, rb.velocity.y); //Add force negative to wall direction (with speed reduction)
						animator.SetTrigger("WallJumped");      //signal wall jump for the animator
					}
					else
					{
						charJumped = true;
						charJumpTime = 0;                   //reset jump time
						animator.SetTrigger("GroundJumped");    //signal char jumped to animator (ground jump)
					}
				}
				else if (inputJumpHeld == 1 && charJumping == true)         //jump is held and jumping hasnt ended by player letting go of jump
				{
					//check if jump time hasnt ended yet and the player isnt stuck on ceiling forcing them to have 0 .y velocity
					if (charJumpDurationLeft > 0 && rb.velocity.y > 0)
					{
						rb.velocity = new Vector2(rb.velocity.x, charJumpForce); //make the character jump
						charJumpDurationLeft -= Time.deltaTime;                 //reduce the jump time
					}
				}

				if (charJumped)
				{
					if (charJumpTime >= charDashJumpBufffer)
					{
						if (groundState.isGround())
						{
							charJumped = false;
							rb.velocity = new Vector2(rb.velocity.x, 0);
						}
					}
					charJumpTime += Time.deltaTime;
				}

				//if the jumping input stops set jumping to false
				if (inputJumpHeld == 0) charJumping = false;
			}

		}
		else
        {
			rb.velocity = new Vector2(0, 0);
        }
		
		//Animation values
		//send values to the animator
		animator.SetFloat("VelocityY", rb.velocity.y);
		animator.SetBool("WallSliding", charWallSliding);
		animator.SetBool("Dashing", charDashing);

		//check if the input of the horizontal axis is negative
		if (inputHorizontal < 0) animator.SetFloat("InputHorizontal", inputHorizontal * -1); //send to animator
		else animator.SetFloat("InputHorizontal", inputHorizontal); //otherwise send the positive value

		//check if the velocity of the rb is negative
		if (rb.velocity.x < 0) animator.SetFloat("VelocityX", rb.velocity.x * -1); //make the velocity positive and send it to the animator
		else animator.SetFloat("VelocityX", rb.velocity.x);         //otherwise send the positive value
		animator.SetBool("Grounded", groundState.isGround());
		if (inputHorizontal == 0 && groundState.isGround()) stoppedMoving = true;
		else stoppedMoving = false;
		animator.SetBool("Stopped", stoppedMoving);

		//Variable Reset
		//resetting input values
		inputHorizontal = 0;
		inputJumpDown = 0;
		inputJumpHeld = 0;
		inputDash = 0;
	}

    private void OnTriggerStay2D(Collider2D collider)
    {
		switch (collider.tag)
		{
			case "LevelPortal":
				if (inputSubmit == 1) overworldController.LoadLevel(collider.name);
				break;
			case "TutorialPortal":
				if (inputSubmit == 1) overworldController.ShowTutorial();
				break;
			default: break;
		}
	}
	private void OnTriggerEnter2D(Collider2D collision)
    {
		switch(collision.tag)
        {
			case "LevelPortal":
				upToEnter.SetActive(true);
				portalHovered = true;
				break;
			case "TutorialPortal":
				upToEnter.SetActive(true);
				portalHovered = true;
				break;
			default: break;
        }
    }


	private void OnTriggerExit2D(Collider2D collision)
    {
		switch (collision.tag)
		{
			case "Sign":
				{
					switch (collision.GetComponent<ObjID>().id)
					{
						case -1:
							overworldController.Quit();
							break;
						case 0:
							overworldController.MainMenu(collision.name);
							break;
						case 1:
							overworldController.goToWorld1(collision.name);
							break;
						case 2:
							overworldController.goToWorld2(collision.name);
							break;
						case 3:
							overworldController.goToWorld3(collision.name);
							break;

						default:
							break;
					}
					break;
				}
			case "LevelPortal":
				upToEnter.SetActive(false);
				portalHovered = false;
				break;
			case "TutorialPortal":
				upToEnter.SetActive(false);
				portalHovered = false;
				break;


			default: break;
		}
	}

}
