using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdventureGame : MonoBehaviour
{

    [SerializeField] Text roomTitle;
    [SerializeField] Text textComponent;
    public State startingState;
    public State victoryState;
    public State failState;
    public GameObject answerButton;
    public GameObject startPoint;
    public float buttonOffset;
    public float init1ButtonOffset;
    public float init2ButtonOffset;
    public float init3ButtonOffset;
    public Text hintText;


    private State currState;
    private List<GameObject> buttons;
    private bool displayHint;
    private int currentHint;
    private bool firebaseUsed;
    // Use this for initialization
    void Start()
    {
        currentHint = 0;
        firebaseUsed = false;
        buttons = new List<GameObject>();
        displayHint = false;
        hintText.text = "";
        roomTitle.text = startingState.GetRoomTitle();
        currState = startingState;
        SetupText();
    }

    // Update is called once per frame
    void Update()
    {
        CheckForFirebaseState();
        if (currState.GetHints().Length != 0)
        {
            hintText.text = currState.GetHints()[currentHint];
        }
        else
        {
            hintText.text = "";
        }
    }

    private void CheckForFirebaseState()
    {
        if (!firebaseUsed && this.gameObject.GetComponent<FirebaseData>())
        {
            FirebaseData firebase = this.gameObject.GetComponent<FirebaseData>();
            if (firebase.DataRetrieved())
            {
                currState = firebase.GetStartState();
                firebaseUsed = true;
                victoryState.SetANextState(currState, 0);
                failState.SetANextState(currState, 0);
            }
            roomTitle.text = currState.GetRoomTitle();
            SetupText();
        }
    }

    public void SetupText()
    {
        textComponent.text = currState.GetStoryText() + "\n\n";

        if (buttons.Count != 0)
        {
            for (int i = 0; i < buttons.Count; i++)
            {
                Destroy(buttons[i]);
            }
            buttons.Clear();
        }

        float startPos = 0.0f;

        if (currState.GetAnswers().Length == 2)
        {
            startPos = init2ButtonOffset;
        }
        else if (currState.GetAnswers().Length == 3)
        {
            startPos = init3ButtonOffset;
        }
        else if (currState.GetAnswers().Length == 1)
        {
            startPos = init1ButtonOffset;
        }

        if (!currState.GetIsTransition())
        {
            currState.ShuffleAnswers();
        }

        for (int i = 0; i < currState.GetAnswers().Length; i++)
        {
            GameObject answer = Instantiate(answerButton, startPoint.transform).gameObject;
            answer.transform.position = new Vector3(startPos + (startPoint.transform.position.x + (buttonOffset * i) / currState.GetAnswers().Length),
                startPoint.transform.position.y, startPoint.transform.position.z);
            answer.GetComponentInChildren<Text>().text = currState.GetAnswers()[i];

            if (currState.GetAnswers()[i] == currState.GetCorrectAnswer())
            {
                answer.tag = "CorrectButton";
            }
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

    public State GetVictoryState()
    {
        return victoryState;
    }

    public void setState(State state)
    {
        displayHint = false;
        currState = state;
    }

    public List<GameObject> GetButtons()
    {
        return buttons;
    }

    public void FlipHintMarker()
    {
        if (currentHint == (currState.GetHints().Length - 1))
        {
            currentHint = 0;
        }
        else
        {
            currentHint++;
        }
    }

}