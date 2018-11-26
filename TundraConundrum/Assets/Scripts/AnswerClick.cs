using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnswerClick : MonoBehaviour
{

    private AdventureGame game;
    private PlayerController player;
    private GameObject item;
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

        //if (this.gameObject.GetComponentInChildren<Text>().text == game.GetState().GetCorrectAnswer())
        //{
        //    if (game.GetState().GetNextState()[0] == game.GetState())
        //    {
        //        game.setState(game.GetVictoryState());
        //        game.SetupText();
        //    }
        //    else
        //    {
        //        game.setState(game.GetState().GetNextState()[0]);
        //        game.SetupText();
        //    }
        //}
        //else
        //{
        //    if (game.GetState().GetNextState()[1] == game.GetState())
        //    {
        //        game.setState(game.GetFailState());
        //        game.SetupText();
        //    }
        //    else
        //    {
        //        game.setState(game.GetState().GetNextState()[1]);
        //        game.SetupText();
        //    }
        //}

        item = GameObject.FindGameObjectWithTag("Key");
        if(item != null)
        {
            Destroy(item);
        }
        /////////////////////////////////////////////////////////////////////////
        if (!player.IsWalking())
        {
            if (!game.GetState().GetIsTransition())
            {
                if (this.gameObject.GetComponentInChildren<Text>().text == game.GetState().GetCorrectAnswer())
                {
                    if (game.GetState().GetNextState()[0] == game.GetState())
                    {
                        game.setState(game.GetVictoryState());
                        game.SetupText();
                    }
                    else
                    {
                        game.setState(game.GetState().GetNextState()[0]);
                        game.SetupText();
                    }

                    // New Script
                    game.CreateItem();
                }
                else
                {
                    if (game.GetState().GetNextState()[1] == game.GetState())
                    {
                        game.setState(game.GetFailState());
                        game.SetupText();
                    }
                    else
                    {
                        game.setState(game.GetState().GetNextState()[1]);
                        game.SetupText();
                    }
                }

                //game.SetDirection(0);

                // For Web Build, Really
                game.SetDirection(Random.Range(-1, 1));

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
