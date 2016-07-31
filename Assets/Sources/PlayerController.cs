using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;
using System;

public class PlayerController : MonoBehaviour
{
	[SerializeField]
	private CharacterPawn _pawn;

	public delegate void OnUserAxisInput(InputStruct input);
	public event OnUserAxisInput onUserAxisInput;

	public delegate void OnFinishPressed(bool isNext);
	public event OnFinishPressed onFinishPressed;

	private InputStruct _inputStruct;

	private void Start() 
	{
		_inputStruct = new InputStruct(false, 0);
	}

	private void Update()
	{
		bool isNextPressed = CrossPlatformInputManager.GetButtonDown("Fire2");
		if(isNextPressed)
		{
			onFinishPressed(true);
			return;
		}

		bool isPrevPressed = CrossPlatformInputManager.GetButtonDown("Fire1");
		if(isPrevPressed)
		{
			onFinishPressed(false);
			return;
		}
	}

	private void FixedUpdate()
	{
		// Read the inputs.
		readInputStruct();

		if (_inputStruct != null) {
			// Pass all parameters to the character control script.
			_pawn.Move (_inputStruct);
			onUserAxisInput (_inputStruct);
		}
	}

	private void readInputStruct() {
		float x = CrossPlatformInputManager.GetAxis("Horizontal");
		bool jumping = isJumping ();

		if (Math.Abs (x) > 0 || jumping) {
			_inputStruct = new InputStruct (jumping, x);
		} else {
			_inputStruct = null;
		}
	}

	private bool isJumping() {
		return CrossPlatformInputManager.GetButtonDown ("Jump");
	}
}
