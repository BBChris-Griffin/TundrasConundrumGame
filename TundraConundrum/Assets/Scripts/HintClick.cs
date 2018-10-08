using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HintClick : MonoBehaviour {

	private AdventureGame game;

	public void Click()
	{
		GameObject mainGameObject = GameObject.FindGameObjectWithTag("GameController");
		if (mainGameObject != null)
		{
				game = mainGameObject.GetComponent<AdventureGame>();
		}

		game.FlipHintMarker();
	}
}
