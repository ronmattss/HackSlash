using UnityEngine;
using System.Collections;

public class WarriorAnimationDemo : MonoBehaviour {

	public Animator animator;
	public GameObject target;

	float rotationSpeed = 30;
	Vector3 inputVec;
	bool blockBool = false;
	bool dead = false;
	bool isMoving;
	bool isStrafing;
	bool isBlocking = false;
	bool isStunned = false;
	bool inBlock;

	//Warrior types
	public enum Warrior{Karate, Ninja, Brute, Sorceress};

	public Warrior warrior;
	
	void Update()
	{
		//Get input from controls
		float z = Input.GetAxisRaw("Horizontal");
		float x = -(Input.GetAxisRaw("Vertical"));
		inputVec = new Vector3(x, 0, z);

		//Apply inputs to animator
		animator.SetFloat("Input X", z);
		animator.SetFloat("Input Z", -(x));

		if(!dead)  //if character isn't dead
		{
			if (x > .1 || x < -.1 || z > .1 || z < -.1)  //if there is some input (account for controller deadzone)
			{
				//set that character is moving
				animator.SetBool("Moving", true);
				isMoving = true;
				
				if (Input.GetKey(KeyCode.LeftShift) || Input.GetAxis("TargetBlock") > .1)  //if strafing or blocking
				{
					isStrafing = true;
					animator.SetBool("Running", false);
				}
				else
				{
					isStrafing = false;
					animator.SetBool("Running", true);
				}
			}
			else
			{
				//character is not moving
				animator.SetBool("Moving", false);
				animator.SetBool("Running", false);
				isMoving = false;
			}

			if (Input.GetAxis("TargetBlock") < -.1)
			{
				if(!inBlock)
				{
					animator.SetBool("Block", true);
					isBlocking = true;
				}
			}

			if (Input.GetAxis("TargetBlock") == 0)
			{
				inBlock = false;
				animator.SetBool("Block", false);
				isBlocking = false;
			}
			
			if (!isBlocking && !blockBool)  //if not blocking
			{
				if (Input.GetButtonDown("Jump"))
				{
					if(isStrafing)
						animator.SetTrigger("JumpTrigger");

					if(isMoving)
						animator.SetTrigger("JumpForwardTrigger");
					else
				    	animator.SetTrigger("JumpTrigger");
				}

				if (Input.GetButtonDown("Fire0"))
				{
					animator.SetTrigger("RangeAttack1Trigger");
					if (warrior == Warrior.Brute)
						StartCoroutine (COStunPause(2.4f));
					else
						StartCoroutine (COStunPause(1.2f));
				}

				if (Input.GetButtonDown("Fire1"))
				{
					animator.SetTrigger("Attack1Trigger");
					if (warrior == Warrior.Brute)
						StartCoroutine (COStunPause(1.2f));
					else if (warrior == Warrior.Sorceress)
						StartCoroutine (COStunPause(1.2f));
					else
						StartCoroutine (COStunPause(.6f));
				}

				if (Input.GetButtonDown("Fire2"))
				{
					animator.SetTrigger("MoveAttack1Trigger");
					if (warrior == Warrior.Brute)
						StartCoroutine (COStunPause(1.7f));
					else if (warrior == Warrior.Sorceress)
						StartCoroutine (COStunPause(1.4f));
					else
						StartCoroutine (COStunPause(.9f));
				}

				if (Input.GetButtonDown("Fire3"))
				{
					animator.SetTrigger("SpecialAttack1Trigger");
					if (warrior == Warrior.Brute)
						StartCoroutine (COStunPause(2f));
					else
						StartCoroutine (COStunPause(1.7f));
				}

				if (Input.GetButtonDown("LightHit"))
				{
					animator.SetTrigger("LightHitTrigger");
					StartCoroutine (COStunPause(2.8f));
				}
			}
			else
			{
				if (Input.GetButtonDown("Jump"))
					animator.SetTrigger("BlockHitReactTrigger");

				if (Input.GetButtonDown("Fire0"))
					animator.SetTrigger("BlockHitReactTrigger");
				
				if (Input.GetButtonDown("Fire1"))
					animator.SetTrigger("BlockHitReactTrigger");
				
				if (Input.GetButtonDown("Fire2"))
					animator.SetTrigger("BlockHitReactTrigger");
				
				if (Input.GetButtonDown("Fire3"))
					animator.SetTrigger("BlockHitReactTrigger");
				
				if (Input.GetButtonDown("LightHit"))
					animator.SetTrigger("BlockHitReactTrigger");
			}

			if(Input.GetAxis("DashVertical") > .5) 
			{
				StartCoroutine (CODash("DashBackwardBool"));
			}

			if(Input.GetAxis("DashVertical") < -.5) 
			{
				StartCoroutine (CODash("DashForwardBool"));
			}

			if(Input.GetAxis("DashHorizontal") < -.5) 
			{
				StartCoroutine (CODash("DashLeftBool"));
			}

			if(Input.GetAxis("DashHorizontal") > .5) 
			{
				StartCoroutine (CODash("DashRightBool"));
			}

		}

		if(!dead)  //if character isn't dead
		{
			if(!isBlocking)  //if not blocking
			{
				if (Input.GetButtonDown("Death"))
				{
					animator.SetTrigger("DeathTrigger");
					dead = true;
				}
			}
		}
		else
		{
			if (Input.GetButtonDown("Death"))
			{
				animator.SetTrigger("ReviveTrigger");

				if (warrior == Warrior.Brute)
					StartCoroutine (COStunPause(1.8f));
				else
					StartCoroutine (COStunPause(1f));

				dead = false;
			}
		}

		UpdateMovement();  //update character position and facing
	}

	public IEnumerator CODash(string direction)
	{
		animator.SetBool(direction, true);
		yield return null;
		animator.SetBool(direction, false);
	}

	public IEnumerator COStunPause(float pauseTime)
	{
		isStunned = true;
		yield return new WaitForSeconds(pauseTime);
		isStunned = false;
	}
	
	void RotateTowardsMovementDir()  //face character along input direction
	{
		if(!dead)  //if character isn't dead
		{
			if (!blockBool && !isBlocking && !isStunned)
			{
				if (inputVec != Vector3.zero && !isStrafing)
				{
					transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(inputVec), Time.deltaTime * rotationSpeed);
				}
			}
		}
	}

	float UpdateMovement()
	{
		Vector3 motion = inputVec;  //get movement input from controls

		//reduce input for diagonal movement
		motion *= (Mathf.Abs(inputVec.x) == 1 && Mathf.Abs(inputVec.z) == 1)?.7f:1;
		
		if (!isStrafing)
			RotateTowardsMovementDir();  //if not strafing, face character along input direction

		if (isStrafing)  //if strafing, look at the target
		{
			//make character point at target
			Quaternion targetRotation;
			Vector3 targetPos = target.transform.position;
			targetRotation = Quaternion.LookRotation(targetPos - new Vector3(transform.position.x,0,transform.position.z));
			transform.eulerAngles = Vector3.up * Mathf.MoveTowardsAngle(transform.eulerAngles.y,targetRotation.eulerAngles.y,(rotationSpeed * Time.deltaTime) * rotationSpeed);
		}

		return inputVec.magnitude;  //return a movement value for the animator, not currently used
	}

	void OnGUI () 
	{
		if(!dead)  //if character isn't dead
		{
			if(!blockBool && !isBlocking)  //if character is not blocking
			{
				if (GUI.Button (new Rect (25, 20, 100, 30), "Dash Forward")) 
				{
					StartCoroutine(CODash("DashForwardBool"));
				}

				if (GUI.Button (new Rect (135, 20, 100, 30), "Dash Right")) 
				{
					StartCoroutine(CODash("DashRightBool"));
				}

				if (GUI.Button (new Rect (245, 20, 100, 30), "Jump")) 
				{
					if (isMoving)  //if character is moving
					{
						if(!isStrafing)  //if character is running play Jump Forward anim
						{
							animator.SetTrigger("JumpForwardTrigger");
						}
						else
							animator.SetTrigger("JumpTrigger");  //play regular jump anim
					}
					else
						animator.SetTrigger("JumpTrigger");  //play regular jump anim
				}
				
				if (GUI.Button (new Rect (25, 50, 100, 30), "Dash Backward")) 
				{
					StartCoroutine(CODash("DashBackwardBool"));
				}

				if (GUI.Button (new Rect (135, 50, 100, 30), "Dash Left")) 
				{
						StartCoroutine(CODash("DashLeftBool"));
				}

				if (GUI.Button (new Rect (25, 85, 100, 30), "Attack1")) 
				{
					animator.SetTrigger("Attack1Trigger");

					if (warrior == Warrior.Brute || warrior == Warrior.Sorceress)  //if character is Brute or Sorceress
						StartCoroutine (COStunPause(1.2f));
					else
						StartCoroutine (COStunPause(.6f));
				}
			}

			blockBool = GUI.Toggle (new Rect (25, 215, 100, 30), blockBool, "Block");
			
			if (blockBool)
				animator.SetBool("Block", true);
			else
				animator.SetBool("Block", false);

			if(blockBool && !isBlocking)
			{
				if (GUI.Button (new Rect (30, 240, 100, 30), "BlockHitReact")) 
				{
					animator.SetTrigger("BlockHitReactTrigger");
				}
			}
			else if(!blockBool)  //if not blocking
			{
				if(!isBlocking)
				{
					if (GUI.Button (new Rect (30, 240, 100, 30), "Hit React")) 
					{
						animator.SetTrigger("LightHitTrigger");
						StartCoroutine (COStunPause(2.8f));
					}
				}
			}

			if(!blockBool && !isBlocking)  //if blocking
			{
				if (GUI.Button (new Rect (25, 115, 100, 30), "RangeAttack1")) 
				{
					animator.SetTrigger("RangeAttack1Trigger");
					if (warrior == Warrior.Brute)  //if character is Brute
						StartCoroutine (COStunPause(2.4f));
					else
						StartCoroutine (COStunPause(1.2f));
				}

				if (GUI.Button (new Rect (25, 145, 100, 30), "MoveAttack1")) 
				{
					animator.SetTrigger("MoveAttack1Trigger");
					if (warrior == Warrior.Brute)  //if character is Brute
						StartCoroutine (COStunPause(1.7f));
					else if (warrior == Warrior.Sorceress)  //if character is Sorceress
						StartCoroutine (COStunPause(1.4f));
					else
						StartCoroutine (COStunPause(.9f));
				}

				if (GUI.Button (new Rect (25, 175, 100, 30), "SpecialAttack1")) 
				{
					animator.SetTrigger("SpecialAttack1Trigger");
					if (warrior == Warrior.Brute)  //if character is Brute
						StartCoroutine (COStunPause(2f));
					else
						StartCoroutine (COStunPause(1.7f));
				}

				if (GUI.Button (new Rect (30, 270, 100, 30), "Death")) 
				{
					animator.SetTrigger("DeathTrigger");
					dead = true;
				}
			}
		}

		if (dead)  //if the character is dead
		{
			if (GUI.Button (new Rect (30, 270, 100, 30), "Revive")) 
			{
				animator.SetTrigger("ReviveTrigger");
				if (warrior == Warrior.Brute)  //if character is Brute
					StartCoroutine (COStunPause(1.8f));
				else
					StartCoroutine (COStunPause(1f));

				dead = false;
			}
		}
	}
}