using UnityEngine;
using UnityEngine.InputSystem;

namespace RPGCharacterAnims
{
    public class RPGCharacterInputController:MonoBehaviour
    {
		//InputSystem
		public @RPGInputs rpgInputs;

		//LegacyInputs.
		[HideInInspector] public Vector2 inputAiming;
		[HideInInspector] public Vector2 inputMovement;
        [HideInInspector] public Vector2 inputFacing;
		[HideInInspector] public Vector2 inputMouseFacing;
		[HideInInspector] public bool inputFace;
		[HideInInspector] public bool inputJump;
        [HideInInspector] public bool inputLightHit;
        [HideInInspector] public bool inputDeath;
        [HideInInspector] public bool inputAttackL;
        [HideInInspector] public bool inputAttackR;
        [HideInInspector] public bool inputCastL;
        [HideInInspector] public bool inputCastR;
        [HideInInspector] public bool inputSwitchDown;
		[HideInInspector] public bool inputSwitchUp;
		[HideInInspector] public bool inputSwitchLeft;
		[HideInInspector] public bool inputSwitchRight;
		[HideInInspector] public float inputBlock = 0;
		[HideInInspector] public bool inputTarget;
        [HideInInspector] public bool inputRoll;
        [HideInInspector] public bool inputShield;
        [HideInInspector] public bool inputRelax;

        //Variables.
        [HideInInspector] public bool allowedInput = true;
        [HideInInspector] public Vector3 moveInput;

        private void Awake()
        {
			rpgInputs = new @RPGInputs();
			allowedInput = true;
        }

		private void OnEnable()
		{
			rpgInputs.Enable();
		}

		private void OnDisable()
		{
			rpgInputs.Disable();
		}	

        private void Update()
        {
            Inputs();
			HasJoystickConnected();
			moveInput = CameraRelativeInput(inputMovement.x, inputMovement.y);
		}

		/// <summary>
		/// Input abstraction for easier asset updates using outside control schemes.
		/// </summary>
		private void Inputs()
        {
			try
			{
				inputAiming = rpgInputs.RPGCharacter.Aiming.ReadValue<Vector2>();
				inputMovement = rpgInputs.RPGCharacter.Move.ReadValue<Vector2>();
				inputFacing = rpgInputs.RPGCharacter.Facing.ReadValue<Vector2>();
				inputMouseFacing = Mouse.current.position.ReadValue();
				inputBlock = rpgInputs.RPGCharacter.Block.ReadValue<float>();
				inputFace = rpgInputs.RPGCharacter.Face.IsPressed();
				inputJump = rpgInputs.RPGCharacter.Jump.WasPressedThisFrame();
				inputLightHit = rpgInputs.RPGCharacter.LightHit.WasPressedThisFrame();
				inputDeath = rpgInputs.RPGCharacter.Death.WasPressedThisFrame();
				inputAttackL = rpgInputs.RPGCharacter.AttackL.WasPressedThisFrame();
				inputAttackR = rpgInputs.RPGCharacter.AttackR.WasPressedThisFrame();
				inputCastL = rpgInputs.RPGCharacter.CastL.WasPressedThisFrame();
				inputCastR = rpgInputs.RPGCharacter.CastR.WasPressedThisFrame();
				inputSwitchUp = rpgInputs.RPGCharacter.WeaponUp.WasPressedThisFrame();
				inputSwitchDown = rpgInputs.RPGCharacter.WeaponDown.WasPressedThisFrame();
				inputSwitchLeft = rpgInputs.RPGCharacter.WeaponLeft.WasPressedThisFrame();
				inputSwitchRight = rpgInputs.RPGCharacter.WeaponRight.WasPressedThisFrame();
				inputTarget = rpgInputs.RPGCharacter.Targeting.IsPressed();
				inputRoll = rpgInputs.RPGCharacter.Roll.WasPressedThisFrame();
				inputShield = rpgInputs.RPGCharacter.Shield.WasPressedThisFrame();
				inputRelax = rpgInputs.RPGCharacter.Relax.WasPressedThisFrame();
			}
			catch(System.Exception)
			{
				Debug.LogWarning("INPUTS NOT FOUND!");
			}
		}

        /// <summary>
        /// Movement based off camera facing.
        /// </summary>
        private Vector3 CameraRelativeInput(float inputX, float inputZ)
        {
            //Forward vector relative to the camera along the x-z plane.
            Vector3 forward = Camera.main.transform.TransformDirection(Vector3.forward);
            forward.y = 0;
            forward = forward.normalized;
            //Right vector relative to the camera always orthogonal to the forward vector.
            Vector3 right = new Vector3(forward.z, 0, -forward.x);
			Vector3 relativeVelocity = inputMovement.x * right + inputMovement.y * forward;
			//Reduce input for diagonal movement.
			if(relativeVelocity.magnitude > 1)
            {
                relativeVelocity.Normalize();
            }
            return relativeVelocity;
        }

        public bool HasAnyInput()
        {
            if(allowedInput && (HasMoveInput() || HasFacingInput() && inputJump != false))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool HasMoveInput()
        {
			if(allowedInput && inputMovement != Vector2.zero)
			{
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool HasFacingInput()
        {
            if(allowedInput && (inputFacing != Vector2.zero))
            {
                return true;
            }
            else
            {
				return false;
            }
        }

		public bool HasJoystickConnected()
		{
			//No joysticks.
			if(Input.GetJoystickNames().Length == 0)
			{
				//Debug.Log("No Joystick Connected");
				return false;
			}
			else
			{
				//Debug.Log("Joystick Connected");
				//If joystick is plugged in.
				for(int i = 0; i < Input.GetJoystickNames().Length; i++)
				{
					//Debug.Log(Input.GetJoystickNames()[i].ToString());
				}
				return true;
			}
		}

		public bool HasBlockInput()
		{
			if(inputBlock != 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
	}

	//Extension Method to allow checking InputSystem without Action Callbacks.
	public static class InputActionExtensions
	{
		public static bool IsPressed(this InputAction inputAction)
		{
			return inputAction.ReadValue<float>() > 0f;
		}

		public static bool WasPressedThisFrame(this InputAction inputAction)
		{
			return inputAction.triggered && inputAction.ReadValue<float>() > 0f;
		}

		public static bool WasReleasedThisFrame(this InputAction inputAction)
		{
			return inputAction.triggered && inputAction.ReadValue<float>() == 0f;
		}
	}
}