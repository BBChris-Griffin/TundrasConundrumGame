using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateRoom : MonoBehaviour {

	public GameObject path;
	public State failState;
	public State victoryState;
	private State startState;
	private AdventureGame game;
	private Vector3 position;
	// Use this for initialization
	void Start () {
		GameObject gameController = GameObject.FindGameObjectWithTag("GameController");
		if(gameController != null)
		{
			game = gameController.GetComponent<AdventureGame>();
		}
		startState = game.startingState;
		position = new Vector3(0, 0, 0);
		InstantiateRoom();
	}

	void InstantiateRoom()
	{
		State currState = startState;
		LevelOrderSearch(currState, position);

	}

	void LevelOrderSearch(State rootState, Vector3 currPosition)
	{
        try
        {
            if (rootState == victoryState || rootState == failState)
            {
                Instantiate(path, currPosition, Quaternion.identity);
                return;
            }
            else
            {
                Instantiate(path, currPosition, Quaternion.identity);
            }

            currPosition = currPosition + new Vector3(0, 0, -1);
            LevelOrderSearch(rootState.GetNextState()[0], currPosition);
            LevelOrderSearch(rootState.GetNextState()[1], currPosition);
        }
        catch
        {
            return;
        }
	}
}
