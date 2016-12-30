using UnityEngine;
using System.Collections;
using VRTK;

public class LevelButton : VRTK_InteractableObject
{
    public GameManager gameManager;
    public int levelId;
    // Use this for initialization
    //void Start () {

    //}

    //// Update is called once per frame
    //void Update () {


    public override void StartUsing(GameObject usingObject)
    {
        if (gameManager)
        {
            gameManager.LoadLevel(levelId);
        }
        //base.StartUsing(usingObject);
        //FireBullet();
    }
}
