using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdventureGame : MonoBehaviour {

    [SerializeField] Text textComponent;
    public State startingState;
    public State failState;
    public GameObject answerButton;
    public GameObject startPoint;
    public float buttonOffset;
    public float initTransButtonOffset;


    private State currState;
    private List<GameObject> buttons;

    // Use this for initialization
    void Start ()
    {
        buttons = new List<GameObject>();
        currState = startingState;
        SetupText();
    }
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    public void SetupText()
    {
        textComponent.text = currState.GetStoryText() + "\n\n";

        if(buttons.Count != 0)
        {
            for(int i = 0; i < buttons.Count; i++)
            {
                Destroy(buttons[i]);
            }
            buttons.Clear();
        }

        float startPos = 0.0f;
        if(currState.GetAnswers().Length == 2)
        {
            startPos = initTransButtonOffset;
        }

        for (int i = 0; i < currState.GetAnswers().Length; i++)
        {
            GameObject answer = Instantiate(answerButton, startPoint.transform).gameObject;
            answer.transform.position = new Vector3(startPos + (startPoint.transform.position.x + (buttonOffset * i) / currState.GetAnswers().Length), 
                startPoint.transform.position.y, startPoint.transform.position.z);
            answer.GetComponentInChildren<Text>().text = currState.GetAnswers()[i];
            buttons.Add(answer);
        }

    }

    public State GetState()
    {
        return currState;
    }

    public State GetFailState()
    {
        return failState;
    }

    public void setState(State state)
    {
        currState = state;
    }

    public List<GameObject> GetButtons()
    {
        return buttons;
    }

}
