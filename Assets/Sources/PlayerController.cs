using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;
using System;

public class PlayerController : MonoBehaviour 
{
	[SerializeField]
	private CharacterPawn _pawn;

	public delegate void OnUserAxisInput(Vector2 input);
	public event OnUserAxisInput onUserAxisInput;

	public delegate void OnFinishPressed(bool isNext);
	public event OnFinishPressed onFinishPressed;

	private Vector2 _input;

	private void Start() 
	{
		_input = new Vector2();
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
		_input.x = CrossPlatformInputManager.GetAxis("Horizontal");
		_input.y = CrossPlatformInputManager.GetAxis("Vertical");

		// Pass all parameters to the character control script.
		_pawn.Move(_input);

		onUserAxisInput(_input);
	}
}
