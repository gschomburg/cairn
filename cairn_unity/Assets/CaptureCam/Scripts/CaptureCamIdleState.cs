using UnityEngine;
using System.Collections;

public class CaptureCamIdleState : StateBase<CaptureCamStates, CaptureCamMachine>
{
  public void Start()
	{
		
	}

	void OnEnable()
	{
		Debug.Log("CaptureCamIdleState enable");
	}

	void Update()
	{
	}

	void OnDisable()
	{
		Debug.Log("CaptureCamIdleState disable");
	}
}
