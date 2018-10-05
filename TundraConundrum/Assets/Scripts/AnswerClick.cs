using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnswerClick : MonoBehaviour {

    private AdventureGame game;
	public void Click()
    {
        GameObject mainGameObject = GameObject.FindGameObjectWithTag("GameController");
        if (mainGameObject != null)
        {
            game = mainGameObject.GetComponent<AdventureGame>();
        }

        if(!game.GetState().GetIsTransition())
        {
            if (this.gameObject.GetComponentInChildren<Text>().text == game.GetState().GetCorrectAnswer())
            {
                game.setState(game.GetState().GetNextState()[0]);
                game.SetupText();
            }
            else
            {
                game.setState(game.GetFailState());
                game.SetupText();
            }
        }     
        else
        {
            if (this.gameObject.GetComponentInChildren<Text>().text == game.GetState().GetAnswers()[0])
            {             
                game.setState(game.GetState().GetNextState()[0]);
                game.SetupText();
            }
            else 
            {
                game.setState(game.GetState().GetNextState()[1]);
                game.SetupText();
            }
        }
    }
}
