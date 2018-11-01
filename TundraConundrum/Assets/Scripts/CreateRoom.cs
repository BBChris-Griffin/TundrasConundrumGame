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
            if (rootState != null)
            {
                Instantiate(path, currPosition, Quaternion.identity);
            }
        }
        catch
        {
            return;
        }
		LevelOrderSearch(rootState.GetNextState()[0], currPosition + new Vector3(0, 1, 0));
		LevelOrderSearch(rootState.GetNextState()[1], currPosition + new Vector3(0, 1, 0));
	}
}
