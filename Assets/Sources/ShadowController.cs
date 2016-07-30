using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShadowController : MonoBehaviour 
{
	[SerializeField]
	private CharacterPawn _pawn;

	private Dictionary<int, Vector2> _inputs;

	public void Init(Dictionary<int, Vector2> pInputs)
	{
		_inputs = pInputs;
	}

	private void FixedUpdate()
	{
		if(_inputs == null)
		{
			return;
		}

		Vector2 input = Vector2.zero;
		if(_inputs.ContainsKey(FrameCounter.currentFrame))
		{
			input = _inputs[FrameCounter.currentFrame];
		}
		_pawn.Move(input);
	}
}
