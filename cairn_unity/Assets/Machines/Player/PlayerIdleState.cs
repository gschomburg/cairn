using UnityEngine;
using System.Collections;

public class PlayerIdleState : StateBase<PlayerStates, PlayerMachine>
{

	void OnEnable()
	{
        print("Idle Enabled" +Machine.TestPropert);
	}

	void Update()
	{
        if(Input.GetKeyUp(KeyCode.Space))
        {
            Machine.SetState(PlayerStates.Death);
        }
        else if (Input.GetKeyUp(KeyCode.Return))
        {
            Machine.SetState(PlayerStates.Moving);
        }
    }

	void OnDisable()
	{
	}
}
