using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using InputHistory = System.Collections.Generic.Dictionary<int, InputStruct>;

public class GameState : MonoBehaviour 
{
	private List<InputHistory> _recordedInput;

	private InputHistory _currentInput;

	private int _currentRecordIndex;

	public void Init()
	{
		_currentRecordIndex = 0;

		_recordedInput = new List<InputHistory>();

		AddNewRecord();
	}

	public void NextShadow()
	{
		++_currentRecordIndex;

		if(_currentRecordIndex < _recordedInput.Count)
		{
			_currentInput = _recordedInput[_currentRecordIndex];
			_currentInput.Clear();
		}
		else 
		{
			AddNewRecord();
		}
	}

	public void PrevShadow()
	{
		--_currentRecordIndex;

		if(_currentRecordIndex < 0)
		{
			_currentRecordIndex = 0;
		}

		_currentInput = _recordedInput[_currentRecordIndex];
		_currentInput.Clear();
	}

	private void AddNewRecord()
	{
		_currentInput = new InputHistory();
		_recordedInput.Add(_currentInput);
	}

	public void RegisterUserInput(InputStruct input)
	{
		_currentInput.Add(FrameCounter.currentFrame, input);
	}

	public List<InputHistory> GetRecordedInput()
	{
		return _recordedInput;
	}

	public int GetCurrentRecordIndex()
	{
		return _currentRecordIndex;
	}
}
