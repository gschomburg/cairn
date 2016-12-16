using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public enum Levels { Intro, Level1, Level2, None };

public class GameManager : MonoBehaviour {
    

    public List<string> scenes;
    public Levels AutoLoad = Levels.None;

    // Use this for initialization
    void Start () {
        if (AutoLoad != Levels.None)
        {
            //load a level
            LoadLevel((int)AutoLoad);
        }
	}
	
    public void LoadLevel(int id)
    {
        SceneManager.LoadScene(scenes[id]);
        //switches scenes
    }
}
