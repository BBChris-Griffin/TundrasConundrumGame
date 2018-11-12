using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnswerClick : MonoBehaviour {

    private AdventureGame game;
    private PlayerController player;
	public void Click()
    {
        GameObject mainGameObject = GameObject.FindGameObjectWithTag("GameController");
        if (mainGameObject != null)
        {
            game = mainGameObject.GetComponent<AdventureGame>();
        }

        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.GetComponent<PlayerController>();
        }

        if (player.GetComponent<Rigidbody>().velocity == Vector3.zero)
        {
            if (!game.GetState().GetIsTransition())
            {
                if (this.gameObject.GetComponentInChildren<Text>().text == game.GetState().GetCorrectAnswer())
                {
                    game.setState(game.GetState().GetNextState()[0]);
                    game.SetupText();

                    // New Script
                    game.CreateItem();
                }
                else
                {
                    game.setState(game.GetFailState());
                    game.SetupText();
                }

                game.SetDirection(0);
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

                if (this.gameObject.GetComponentInChildren<Text>().text == "Left")
                {
                    game.SetDirection(-1);
                }
                else if (this.gameObject.GetComponentInChildren<Text>().text == "Right")
                {
                    game.SetDirection(1);
                }
            }
        } 
    }
}
