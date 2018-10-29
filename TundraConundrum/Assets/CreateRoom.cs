using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateRoom : MonoBehaviour {

	public GameObject path;
	public State failState;
	public State victoryState;
	private State startState;
	private AdventureGame game;
	// Use this for initialization
	void Start () {
		gameController = FindGameObjectWithTag("GameController");
		if(gameController != null)
		{
			game = gameController.GetComponent<AdventureGame>();
		}
		startState = game.startingState;
	}

	void CreateRoom()
	{
		State currState = startState;
		
	}
}
