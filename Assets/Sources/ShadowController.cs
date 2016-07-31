using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using InputHistory = System.Collections.Generic.Dictionary<int, InputStruct>;

public class ShadowController : MonoBehaviour 
{
	[SerializeField]
	private CharacterPawn _pawn;

	private InputHistory _inputs;

	public void Init(InputHistory pInputs)
	{
		_inputs = pInputs;
	}

	private void FixedUpdate()
	{
		if(_inputs == null)
		{
			return;
		}

		InputStruct input = new InputStruct(false, 0);
		if(_inputs.ContainsKey(FrameCounter.currentFrame))
		{
			input = _inputs[FrameCounter.currentFrame];
		}
		_pawn.Move(input);
	}
}
