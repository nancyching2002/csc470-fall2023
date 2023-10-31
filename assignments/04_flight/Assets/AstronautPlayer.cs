using UnityEngine;
using System.Collections;
using TMPro;


	public class AstronautPlayer : MonoBehaviour {
		private Animator anim;
		private CharacterController controller;
		public Camera cameraObject;
		Vector3 oldCamPos;

		public TMP_Text win;

		// These are the variables that will scale the effect of the movement, gravity,
		// and rotation code in update.
		float forwardSpeed = 6;
		float rotateSpeed = 60;
		float jumpForce = 18;
		float gravityModifier = 4.5f;
		float yVelocity = 0;

		//Jump variables
		public int doubleJump = 0;

		//Win
		public bool won = false;


		void Start () {
			controller = GetComponent<CharacterController>();
			anim = gameObject.GetComponentInChildren<Animator>();

		}
		
		private void OnTriggerEnter(Collider other)
		{
			if (other.CompareTag("Power"))
			{
				doubleJump+= 1;
				Debug.Log(doubleJump);
			}
			if (other.CompareTag("Treasure"))
			{
				//You win!
				won = true;
				Debug.Log("WON");
            	win.enabled = true;
			}
		}

		void Update () {

			float hAxis = Input.GetAxis("Horizontal");
        	float vAxis = Input.GetAxis("Vertical");

			// CAMERA
        	Vector3 newCamPos = transform.position + -transform.forward * 6 + Vector3.up * 1f;
			if (oldCamPos == null)
			{
				oldCamPos = newCamPos;
			}
			cameraObject.transform.position = (newCamPos + oldCamPos) / 2f;
			cameraObject.transform.LookAt(transform);
			oldCamPos = newCamPos;

			// --- ROTATION ---
			// Rotate on the y axis based on the hAxis value
			// NOTE: If the player isn't pressing left or right, hAxis will be 0 and there will be no rotation
			transform.Rotate(0, hAxis * rotateSpeed * Time.deltaTime, 0, Space.Self);

			// --- DEALING WITH GRAVITY ---
			if (!controller.isGrounded)
			{
				// If we go in this block of code, cc.isGrounded is false, which means
				// the last time cc.Move was called, we did not try to enter the ground.
				yVelocity += Physics.gravity.y * gravityModifier * Time.deltaTime;
			} else {
				// If we are in this block of code, we are on the ground.
				// Set the yVelocity to be some small number to try to push us into
				// the ground and thus make cc.isGrounded be true.
				yVelocity = -1;
			}


            // JUMP. When the player presses space, set yVelocity to the jump force. This will immediately
            // make the player start moving upwards, and gravity will start slowing the movement upward
            // and eventually make the player hit the ground (thus landing in the 'if' statment above)

			if (Input.GetKeyDown(KeyCode.Space) && controller.isGrounded)
				{
					yVelocity = jumpForce;
				}
			else if (Input.GetKeyDown(KeyCode.Space) && !controller.isGrounded && doubleJump >= 1){
				yVelocity = jumpForce;
				doubleJump-=1;
			}



			// --- TRANSLATION ---
			// Move the player forward based on the vAxis value
			// Note, If the player isn't pressing up or down, vAxis will be 0 and there will be no movement
			// based on input. However, yVelocity will still move the player downward.
			Vector3 amountToMove = vAxis * transform.forward * forwardSpeed;
			amountToMove.y = yVelocity;

			// This will move the player according to the forward vector and the yVelocity using the
			// CharacterController.
			controller.Move(amountToMove * Time.deltaTime);

			//ANIMATIONS
			if (vAxis != 0) {
				anim.SetInteger ("AnimationPar", 1);
			}  else {
					anim.SetInteger ("AnimationPar", 0);
			}
			
		}
	}

