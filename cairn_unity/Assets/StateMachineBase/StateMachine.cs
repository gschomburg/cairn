using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class StateMachine<TEnum, TMachine> :  MonoBehaviour {

    protected StateMachineManager machineManager;
    public StateMachineManager SMM
    {
        get { return machineManager; }
        set { machineManager = value; }
    }

    protected StateBase<TEnum, TMachine>[] states;
    public StateBase<TEnum, TMachine>[] States
    {
        get { return states; }
    }
    
    protected Dictionary<TEnum, StateBase<TEnum, TMachine>> statesTable;
    protected StatesController stateController; // !! this does nothing rite now

    protected TEnum currentState; // states are enums into the component hashtable
    public TEnum State
    {
        get { return currentState; }
    }

    protected TEnum lastState;
    public TEnum LastState
    {
        get { return lastState; }
        set { lastState = value; }
    }

    protected TEnum nextState;
    public TEnum NextState
    {
        get { return nextState; }
        set { nextState = value; }
    }


    // This is the type of StateMachine -Ex: PlayerMachine, StageMachine ect.
    protected System.Type type;
    public System.Type MachineType
    {
        get { return type; }
    }
    
    protected string machineName;
    public string Name
    {
        get { return machineName; }
    }

    protected bool isChangingState = false;

    public bool allowLogging = false;


    public StateMachine()
    {
        if (!typeof(TEnum).IsEnum)
        {
            throw new ArgumentException("TEnum must be an enumeration");
        }
    }

    public void SetDisabled()
    {
        statesTable[currentState].enabled = false;
        SetActive(false);
    }

    public void SetActive(bool active)
    {
        if (gameObject)
        {
            gameObject.SetActive(active);
        }
    }
    

    // Use this for initialization
    protected virtual void Awake()
    {
        if (stateController != null) { return; }

        machineManager = FindObjectOfType<StateMachineManager>();

        if(machineManager == null)
        {
            Debug.LogWarning("Missing StateMachineManager Object");
        }

        type = this.GetType();

        machineName = type.ToString();
        machineName = machineName.Replace("Machine", "");

        stateController = GetComponentInChildren<StatesController>();
        states = stateController.GetComponents<StateBase<TEnum, TMachine>>();
        InitializeAllStates();
    }

    public void ResetAllStates()
    {
        foreach (StateBase<TEnum, TMachine> state in states)
        {
            state.Reset();
        }
    }

    protected void InitializeAllStates()
    {
        statesTable = new Dictionary<TEnum, StateBase<TEnum, TMachine>>();

        foreach (StateBase<TEnum, TMachine> state in states)
        {
            state.enabled = false;
            state.Machine = GetComponent<TMachine>();
            state.Init(machineName);
            statesTable.Add(state.StateEnum, state);
        }
    }

    public virtual void SetStateDelayed(TEnum _newState, float _delay)
    {
        nextState = _newState;
        StartCoroutine(SetStateDelayed(_delay));
    }

    public virtual IEnumerator SetStateDelayed(float _delay)
    {
        isChangingState = true;
        yield return new WaitForSeconds(_delay);
        isChangingState = false;
        SetState(nextState);
    }

    public virtual bool SetState(TEnum _newState)
    {
        if (isChangingState) { return false; }
        

        // This prevents SetState from getting called twice in the same frame.
        if ( statesTable.ContainsKey(currentState)  &&
            (isChangingState || statesTable[currentState] == statesTable[_newState]) ) { return false; }
        
        isChangingState = true;

        lastState = currentState;
        currentState = _newState;

        // Dont allow to trigger if same state
        if (lastState != null && statesTable.ContainsKey(lastState))
        {
            if (allowLogging)
            {
                Debug.Log("EXIT: " + statesTable[lastState]);
            }

            if (statesTable[lastState].enabled == true)
            {
                statesTable[lastState].enabled = false;
            }
        }
        
        if (statesTable[currentState].enabled == false)
        {
            if (allowLogging)
            {
                Debug.Log("ENT: " + statesTable[currentState]);
            }

            statesTable[currentState].enabled = true;
        }
        
        isChangingState = false;
        
        return true;

    }

    public TEnum GetCurrentState()
    {
        return currentState;
    }

    public void MLog(string _str)
    {
        if (allowLogging)
        {

            Debug.Log(_str);
        }
    }

}


public class ScreenStateMachine<TEnum, TMachine> : StateMachine<TEnum, TMachine>
{

    // Use this for initialization
    protected override void Awake()
    {
        machineManager = FindObjectOfType<StateMachineManager>();

        if (machineManager == null)
        {
            Debug.LogWarning("Missing StateMachineManager Object");
        }

        type = this.GetType();

        machineName = type.ToString();
        machineName = machineName.Replace("Machine", "");

        states = GetComponentsInChildren<StateBase<TEnum, TMachine> >(true);

        InitializeAllStates();
    }

}
