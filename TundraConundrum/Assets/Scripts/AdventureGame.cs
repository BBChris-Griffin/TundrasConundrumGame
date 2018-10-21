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
    public float init2ButtonOffset;
    public float init3ButtonOffset;
    public Text hintText;
    public GameObject item;
    public GameObject itemInfo;

    private State currState;
    private List<GameObject> buttons;
    private bool displayHint;
    private bool startWalking;

    // Use this for initialization
    void Start ()
    {
        startWalking = false;
        buttons = new List<GameObject>();
        displayHint = false;
        hintText.text = "";
        currState = startingState;
        SetupText();
    }

	// Update is called once per frame
	void Update ()
    {
        if(displayHint)
        {
            if(currState.GetHints().Length != 0)
            {
              hintText.text = currState.GetHints()[0];
            }
        }
        else
        {
          hintText.text = "";
        }
	  }

    public void SetupText()
    {
        startWalking = true;
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
            startPos = init2ButtonOffset;
        }
        else if (currState.GetAnswers().Length == 3)
        {
            startPos = init3ButtonOffset;
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
        displayHint = false;
        currState = state;
    }

    public List<GameObject> GetButtons()
    {
        return buttons;
    }

    public void FlipHintMarker()
    {
      displayHint = !displayHint;
    }

    public bool StartWalking()
    {
        return startWalking;
    }

    public void StopWalking()
    {
        startWalking = false;
    }

    public void CreateItem()
    {
        Instantiate(item, itemInfo.transform.position, item.transform.rotation);
    }
}
