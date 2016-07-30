using UnityEngine;
using System.Collections;
using UnityEngine.Assertions;
using System.Collections.Generic;

public class GameMode : MonoBehaviour 
{
	[SerializeField]
	private GameState _gameState;
	[SerializeField]
	private GameObject _characterPrefab;
	[SerializeField]
	private GameObject _shadowPrefab;

	private Transform _playerStartTran;

	private GameObject _spawnedCharacter;
	private CharacterPawn _characterPawn;

	private List<GameObject> _spawnedShadows;

	PlayerController _playerController;

	private void Awake()
	{
		_spawnedShadows = new List<GameObject>();

		GameObject playerStart = GameObject.FindWithTag("PlayerStart");

		Assert.IsNotNull(playerStart, "Player start is NULL");

		_playerStartTran = playerStart.transform;
		_spawnedCharacter = Instantiate(_characterPrefab, _playerStartTran.position, _playerStartTran.rotation) as GameObject;

		_playerController = _spawnedCharacter.GetComponent<PlayerController>();
		_playerController.onUserAxisInput += _gameState.RegisterUserAxisInput;
		_playerController.onFinishPressed += Reborn;

		_characterPawn = _spawnedCharacter.GetComponent<CharacterPawn>();
		_characterPawn.SetNumber(1);

		_gameState.Init();
	}

	private void Destroy()
	{
		_playerController.onUserAxisInput -= _gameState.RegisterUserAxisInput;
		_playerController.onFinishPressed -= Reborn;
	}

	private void Reborn(bool pIsNextShadow)
	{
		FrameCounter.Reset();

		ChangeGameState(pIsNextShadow);

		Respawn();
	}

	private void ChangeGameState(bool pIsNextShadow)
	{
		if(pIsNextShadow)
		{
			_gameState.NextShadow();
		}
		else 
		{
			_gameState.PrevShadow();
		}
	}

	private void Respawn()
	{
		_spawnedCharacter.transform.position = _playerStartTran.position;
		_spawnedCharacter.transform.rotation = _playerStartTran.rotation;

		List<Dictionary<int, Vector2>> shadownsInput = _gameState.GetRecordedInput();
		int shadowsCount = shadownsInput.Count;
		int currentUserIndex = _gameState.GetCurrentRecordIndex();

		_characterPawn.SetNumber(currentUserIndex + 1);

		for(int i = 0; i < shadowsCount; ++i)
		{
			GameObject shadow = GetShadow(i, shadownsInput[i]);
			shadow.transform.position = _playerStartTran.position;
			shadow.transform.rotation = _playerStartTran.rotation;

			if(i == currentUserIndex)
			{
				shadow.SetActive(false);
			}
		}
	}

	private GameObject GetShadow(int pIndex, Dictionary<int, Vector2> pInput)
	{
		GameObject shadow = null;
		if(pIndex < _spawnedShadows.Count)
		{
			shadow = _spawnedShadows[pIndex];
		}
		else 
		{
			shadow = Instantiate(_shadowPrefab, _playerStartTran.position, _playerStartTran.rotation) as GameObject;
			_spawnedShadows.Add(shadow);

			CharacterPawn shadowPawn = shadow.GetComponent<CharacterPawn>();
			shadowPawn.SetNumber(pIndex + 1);

			ShadowController controller = shadow.GetComponent<ShadowController>();
			controller.Init(pInput);
		}

		shadow.gameObject.SetActive(true);
		return shadow;
	}

}
