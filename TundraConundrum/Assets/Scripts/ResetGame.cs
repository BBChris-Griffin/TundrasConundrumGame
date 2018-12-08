using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetGame : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Restart()
    {
        GlobalVariables.reset = true;
        SceneManager.LoadScene("3DGame");
    }
}
