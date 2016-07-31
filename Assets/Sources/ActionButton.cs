using UnityEngine;
using System.Collections;

public enum EActionButtonState
{
	Default = 0,
	Unpressed,
	Pressed
}

public class ActionButton : MonoBehaviour 
{
	[SerializeField]
	private SpriteRenderer _spriteRenderer;

	[Header("State Images")]
	[SerializeField]
	private Sprite _unpressedSprite;
	[SerializeField]
	private Sprite _pressedSprite;

	public delegate void OnButtonInteract(EActionButtonState pNewState);
	public event OnButtonInteract onButtonInteract;

	private EActionButtonState _currentState;
	public  EActionButtonState currentState
	{
		get 
		{
			return _currentState;
		}
		private set 
		{
			_currentState = value;
			SetState(value);
		}
	}

	private void Awake()
	{
		currentState = EActionButtonState.Unpressed;
	}

	private void OnTriggerEnter(Collider pOther)
	{
		GameObject obj = pOther.gameObject;
		if(obj.tag == "Player")
		{
			currentState = EActionButtonState.Pressed;

			if(onButtonInteract != null)
			{
				onButtonInteract(_currentState);
			}
		}
	}

	private void OnTriggerExit(Collider pOther)
	{
		GameObject obj = pOther.gameObject;
		if(obj.tag == "Player")
		{
			currentState = EActionButtonState.Unpressed;

			if(onButtonInteract != null)
			{
				onButtonInteract(_currentState);
			}
		}
	}

	private void SetState(EActionButtonState pNewState)
	{
		gameObject.SetActive(true);
		switch(pNewState) 
		{
			case EActionButtonState.Default:
				gameObject.SetActive(false);
				break;
			case EActionButtonState.Unpressed:
				_spriteRenderer.sprite = _unpressedSprite;
				break;
			case EActionButtonState.Pressed:
				_spriteRenderer.sprite = _pressedSprite;
				break;
		}
	}
}
