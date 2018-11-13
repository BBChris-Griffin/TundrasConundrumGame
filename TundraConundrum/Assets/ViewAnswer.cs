using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewAnswer : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	public void HighlightAnswer()
    {
        GameObject button = GameObject.FindGameObjectWithTag("CorrectButton");
        button.GetComponent<Image>().color = Color.green;
    }
}
