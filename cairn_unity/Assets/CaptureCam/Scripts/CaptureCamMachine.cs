using UnityEngine;
using System.Collections;

public enum CaptureCamStates
{
	NONE,
	Idle,
	Interactable,
	Recording,
};

public class CaptureCamMachine : StateMachine<CaptureCamStates, CaptureCamMachine>
{
	void Start()
	{
		 SetState(CaptureCamStates.Idle);
	}

	void OnEnable()
	{
	}
}
