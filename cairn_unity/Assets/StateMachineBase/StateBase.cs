using UnityEngine;
using System.Collections;
using System.Runtime.CompilerServices;
using System;

public abstract class StateBase<T, TMachine> : MonoBehaviour {


    protected T stateEnum;
    public T StateEnum
    {
        get { return stateEnum; }
    }

    //protected T nextState;
    private TMachine stateMachine;
    public TMachine Machine
    {
        get { return stateMachine; }
        set { stateMachine = value; }
    }
   
    protected System.Type type;

	public virtual void Awake()
	{
	}

    public virtual void Reset()
    {
        enabled = false;
    }

	public virtual void Init(string machineName)
	{
        type = this.GetType();

        string typeName = type.ToString();
        typeName = typeName.Replace("State", "");
        typeName = typeName.Replace(machineName, "");
        stateEnum = (T)Enum.Parse(typeof(T), typeName, true);
    }
}

public class MenuBaseState<T, TMachine> : StateBase<T, TMachine>
{

   
    protected virtual void ActivateMenu()
    {
    }

    protected virtual void DeactivateMenu()
    {
    }

    protected virtual void OutroComplete()
    {
    }
    
}

public class ScreenState<T, TMachine> : StateBase<T, TMachine>
{
    /*
    protected HVR_Screen mScreen;
    
    public override void Awake()
    {
        mScreen = GetComponentInChildren<HVR_Screen>(true);
    }
    */


}