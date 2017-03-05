using UnityEngine;
using System.Collections;

public class PlayerDeathState : StateBase<PlayerStates, PlayerMachine>
{
    public override void Awake()
    {
        base.Awake();
        stateEnum = PlayerStates.Death;
	}

	void OnEnable()
	{
	}

	void Update()
	{
        if (Input.GetKeyUp(KeyCode.Space))
        {
            Machine.SetState(PlayerStates.Moving);
        }
        else if (Input.GetKeyUp(KeyCode.Return))
        {
            Machine.SetState(PlayerStates.Idle);
        }
    }

	void OnDisable()
	{
	}
}
