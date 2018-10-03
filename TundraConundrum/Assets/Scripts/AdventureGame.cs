using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdventureGame : MonoBehaviour {

    [SerializeField] Text textComponent;
    public State startingState;
    private State currState;

	// Use this for initialization
	void Start ()
    {
        currState = startingState;
        SetupText();
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(!currState.GetIsTransition())
        {
            QuestionControls();
        }
        else
        {
            TransitionControls();
        }
	}

    private void SetupText()
    {
        textComponent.text = currState.GetStoryText() + "\n\n";
        for(int i = 0; i < currState.GetAnswers().Length; i++)
        {
            textComponent.text += (i + 1) + ". " + currState.GetAnswers()[i] + "\n";
        }
    }

    private void QuestionControls()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            if(currState.GetAnswers()[0] == currState.GetCorrectAnswer())
            {
                currState = currState.GetNextState()[0];
                SetupText();
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (currState.GetAnswers()[1] == currState.GetCorrectAnswer())
            {
                currState = currState.GetNextState()[0];
                SetupText();
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (currState.GetAnswers()[2] == currState.GetCorrectAnswer())
            {
                currState = currState.GetNextState()[0];
                SetupText();
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            if (currState.GetAnswers()[3] == currState.GetCorrectAnswer())
            {
                currState = currState.GetNextState()[0];
                SetupText();
            }
        }
    }

    private void TransitionControls()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            currState = currState.GetNextState()[0];
            SetupText();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            //currState = currState.GetNextState()[1];
            //SetupText();

            // Temporary
            Application.Quit();
        }
    }
}
