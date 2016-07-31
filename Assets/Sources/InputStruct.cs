using UnityEngine;
using System.Collections;

public class InputStruct {
	public bool jumping { get; private set; }
	public float deltaX { get; private set; }

	public InputStruct(bool _jumping, float _deltaX) {
		jumping = _jumping;
		deltaX = _deltaX;
	}
}
