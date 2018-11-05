using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdventureGame : MonoBehaviour {

    [SerializeField] Text textComponent;
    public State startingState;
    public State failState;
    public State victoryState;
    public GameObject answerButton;
    public GameObject startPoint;
    public float buttonOffset;
    public float init2ButtonOffset;
    public float init3ButtonOffset;
    public GameObject path;
    public Transform pathLocation;
    public Text hintText;
    public GameObject item;
    public GameObject iceFlake;
    public GameObject itemInfo;
    public GameObject player;

    private State currState;
    private List<GameObject> buttons;
    private bool displayHint;
    private bool startWalking;
    private int direction; // -1 means left, 0 means forward, 1 means right
    private Quaternion itemRotation;

    enum moveDirection { left, right, forward};

    // Use this for initialization
    void Start ()
    {
        itemRotation = item.transform.rotation;
        direction = 0;
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

        //GameObject floor = Instantiate(path);
        //floor.transform.position = player.transform.position;
        //floor.transform.LookAt(player.transform);
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
        if(currState != victoryState)
        {
            GameObject newItem = Instantiate(item, itemInfo.transform.position, itemRotation);
        }
        else
        {
            GameObject newItem = Instantiate(iceFlake, itemInfo.transform.position, itemRotation);
        }
        //newItem.transform.parent = player.transform;
        //newItem.transform.rotation = Quaternion.Euler(new Vector3(0.0f, item.transform.rotation.y + player.transform.rotation.y, 45f));
    }

    public int GetDirection()
    {
        return direction;
    }

    public void SetDirection(int direction)
    {
        this.direction = direction;
    }
}
