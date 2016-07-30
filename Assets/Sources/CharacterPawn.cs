using UnityEngine;
using System.Collections;

public class CharacterPawn : MonoBehaviour 
{
	[SerializeField] private float _maxSpeed = 10f;                    // The fastest the player can travel in the x axis.
	private bool _facingRight = true;  // For determining which way the player is currently facing.

	[SerializeField]
	private Animator _anim;            // Reference to the player's animator component.
	[SerializeField]
	private Rigidbody _rigidbody;
	[SerializeField]
	private TextMesh _numberText;

	private Vector3 _currentSpeed;

	private void Awake()
	{
		_currentSpeed = new Vector3();
	}

	public void Move(Vector2 move)
	{
		// The Speed animator parameter is set to the absolute value of the horizontal input.
		_anim.SetFloat("Speed", Mathf.Abs(move.x) + Mathf.Abs(move.y));

		// Move the character
		_currentSpeed.x = move.x * _maxSpeed;
		_currentSpeed.y = move.y * _maxSpeed;

		_rigidbody.velocity = _currentSpeed;

		// If the input is moving the player right and the player is facing left...
		if (move.x > 0 && !_facingRight)
		{
			// ... flip the player.
			Flip();
		}
		// Otherwise if the input is moving the player left and the player is facing right...
		else if (move.x < 0 && _facingRight)
		{
			// ... flip the player.
			Flip();
		}
	}

	public void SetNumber(int pNumber)
	{
		_numberText.text = pNumber.ToString();
	}

	private void Flip()
	{
		// Switch the way the player is labelled as facing.
		_facingRight = !_facingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = _anim.transform.localScale;
		theScale.x *= -1;
		_anim.transform.localScale = theScale;
	}

}
