using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathText : MonoBehaviour {

    private AdventureGame game;
    private  Text text;
    // Use this for initialization
    void Start()
    {
        GameObject mainGameObject = GameObject.FindGameObjectWithTag("GameController");
        if (mainGameObject != null)
        {
            game = mainGameObject.GetComponent<AdventureGame>();
        }
        text = GameObject.FindGameObjectWithTag("Death Text").GetComponent<Text>();
        text.text = "You fool. The correct answer was " + game.GetOldAnswer();
    }

    // Update is called once per frame
    void Update () {
		
	}
}
