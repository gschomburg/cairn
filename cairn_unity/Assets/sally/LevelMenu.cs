using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class LevelMenu : MonoBehaviour {

    Quaternion activationAngle = Quaternion.identity;
    public float angleThreshold = 45;

    GameManager gameManager;
    float buttonMargin = .6f;

    public Transform rotationTrigger;
    public float triggerAngle;

    List<GameObject> buttons;

	// Use this for initialization
	void Start () {
        buttons = new List<GameObject>();
        //build the menu
        gameManager = (GameManager)FindObjectOfType(typeof(GameManager));
        GameObject levelButton = transform.FindChild("LevelButton").gameObject;
        for (int i=0;  i < gameManager.scenes.Count; i++)
        {
            GameObject button = (GameObject)Instantiate(levelButton, levelButton.transform.position, levelButton.transform.rotation);
            button.transform.parent = transform;
            button.transform.localPosition = button.transform.localPosition +new Vector3(0, i * buttonMargin, 0);
            
            
            Text t = button.GetComponentInChildren<Text>();
            t.text = gameManager.scenes[i];

            LevelButton buttonObj = button.GetComponent<LevelButton>();
            buttonObj.levelId = i;
            buttonObj.gameManager = gameManager;

            buttons.Add(button);
        }
        levelButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        float angle = rotationTrigger.localEulerAngles.z;
        //Debug.Log(angle);
        if (angle < triggerAngle+angleThreshold && angle > triggerAngle - angleThreshold)
        {
            //transform.localScale = new Vector3(.2f,.2f,.2f);
            Activate(true);
        }
        else
        {
            //transform.localScale = new Vector3(.1f, .1f, .1f);
            Activate(false);
        }
    }
    void Activate(bool val)
    {
        foreach(GameObject b in buttons)
        {
            b.SetActive(val);
        }
    }
}
