using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
public class StateMachineCreator : EditorWindow
{
	public string stateNameEnter ="";

	public string stateMachineName ="Player";
	public List<string> states = new List<string>();

    private string stateMachineText = "Machine";
    private string stateText = "State";
//	private string className ="";
	private string machineName ="";
	private string statesName ="";

	private int totalStates = 1;
	
	// Add menu item named "My Window" to the Window menu
	[MenuItem("Window/StateEditor")]
	public static void ShowWindow()
	{
		//Show existing window instance. If one doesn't exist, make one.
		EditorWindow.GetWindow(typeof(StateMachineCreator));
	}

	void GenerateStates()
	{
		GUILayout.Space(10);
		
		if(GUILayout.Button("Create States"))
		{
			machineName = stateMachineName + stateMachineText;
			statesName = stateMachineName + stateText+"s";
            
			for(int i = 0; i < states.Count; i++)
			{
				string stateName = stateMachineName+states[i]+stateText;
			
				string path = Application.streamingAssetsPath+"/"+stateName+".cs"; 
			
				using (System.IO.StreamWriter sw = System.IO.File.CreateText(path)) 
				{
					sw.WriteLine("using UnityEngine;");
					sw.WriteLine("using System.Collections;");
					
					sw.WriteLine("");


					sw.WriteLine("public class "+stateName +" : StateBase<"+statesName+", "+ machineName+">");
					sw.WriteLine("{");

					sw.WriteLine("  public void Start()");
					sw.WriteLine("	{");
                    sw.WriteLine("	}\n");
                    
					sw.WriteLine("	void OnEnable()");
					sw.WriteLine("	{");
					sw.WriteLine("	}\n");
					
					sw.WriteLine("	void Update()");
					sw.WriteLine("	{");
					sw.WriteLine("	}\n");

					sw.WriteLine("	void OnDisable()");
					sw.WriteLine("	{");
					sw.WriteLine("	}");

					
					sw.WriteLine("}");
					
				}
			}
		}

	}

	void OnGUI()
	{
        GUILayout.Label("- State Machine Name");
		stateMachineName = GUILayout.TextField(stateMachineName, 25);

		GUILayout.Space(10);
        
		for(int i = 0; i < states.Count; i++)
		{
			GUILayout.Label(" "+states[i]);
		}

		stateNameEnter = GUILayout.TextField(stateNameEnter, 25);

		if(GUILayout.Button("Add State"))
		{
			states.Add(stateNameEnter);
			stateNameEnter = "";
		}

		if(GUILayout.Button("Clear State"))
		{
			states.RemoveAt(states.Count-1);
		}

		GUILayout.Space(10);

		if(GUILayout.Button("Create Machine"))
		{

			machineName = stateMachineName + stateMachineText;
			statesName = stateMachineName + stateText+"s";

			string path = Application.streamingAssetsPath+"/"+machineName+".cs"; 
			
			using (System.IO.StreamWriter sw = System.IO.File.CreateText(path)) 
			{
				sw.WriteLine("using UnityEngine;");
				sw.WriteLine("using System.Collections;");

				sw.WriteLine("");

				sw.WriteLine("public enum "+statesName);
				sw.WriteLine("{");

                sw.WriteLine("\t" + "NONE" + ",");

                for (int i = 0; i < states.Count; i++)
				{
					sw.WriteLine("\t"+states[i]+",");
				}

				sw.WriteLine("};");

				sw.WriteLine("");

				sw.WriteLine("public class "+machineName +" : StateMachine<"+statesName+", "+ machineName+">");
				sw.WriteLine("{");
				
				sw.WriteLine("	void Start()");
				sw.WriteLine("	{");
				sw.WriteLine("	}\n");
				
				sw.WriteLine("	void OnEnable()");
				sw.WriteLine("	{");
				sw.WriteLine("	}");
				
				sw.WriteLine("}");
				
			}
		}
		
		GenerateStates();
	}
}
