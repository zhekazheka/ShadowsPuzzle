using UnityEngine;
using System.Collections;

public class FrameCounter : MonoBehaviour 
{
	public static int currentFrame = 0;

	public static void Reset()
	{
		currentFrame = 0;
	}

	private void FixedUpdate()
	{
		++currentFrame;
	}

}
