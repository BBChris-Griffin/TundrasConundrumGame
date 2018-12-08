using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SuccessText : MonoBehaviour {

    private AdventureGame game;
    private Text text;
	// Use this for initialization
	void Start () {
        GameObject mainGameObject = GameObject.FindGameObjectWithTag("GameController");
        if (mainGameObject != null)
        {
            game = mainGameObject.GetComponent<AdventureGame>();
        }
        text = GameObject.FindGameObjectWithTag("Success Text").GetComponent<Text>();
        text.text = "You have completed " + game.GetRoomTitle() + ". Congratulations!!";
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
