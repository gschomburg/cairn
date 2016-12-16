using UnityEngine;
using System.Collections;

public class EndLevelTrigger : MonoBehaviour {
    public GameObject player;

    public Levels nextLevel;
    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("OnTriggerEnter: " + other.gameObject);
        if (other.gameObject == player)
        {
            if(nextLevel!= Levels.None)
            {
                GameManager gameMngr = (GameManager)FindObjectOfType(typeof(GameManager));
                gameMngr.LoadLevel((int)nextLevel);
            }
        }
    }
}
