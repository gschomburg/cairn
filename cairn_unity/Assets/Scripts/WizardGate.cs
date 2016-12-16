using UnityEngine;
using System.Collections;

public class WizardGate : MonoBehaviour {
    public GameObject player;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            player.layer = 15;
            Debug.Log("wizard gate enter!");
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            player.layer = 14;
        }
    }
}
