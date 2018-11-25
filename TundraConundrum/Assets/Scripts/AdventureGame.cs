using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdventureGame : MonoBehaviour
{

    [SerializeField] Text roomTitle;
    [SerializeField] Text textComponent;
    public State startingState;
    public State failState;
    public State victoryState;
    public GameObject answerButton;
    public GameObject startPoint;
    public float buttonOffset;
    public float init1ButtonOffset;
    public float init2ButtonOffset;
    public float init3ButtonOffset;
    public GameObject path;
    public Transform pathLocation;
    public Text hintText;
    public GameObject item;
    public GameObject iceFlake;
    public GameObject itemInfo;
    public GameObject player;
    public Transform tundraSpawn;
    public float tundraDescentSpeed;
    public float tundraDescentTime;
    public GameObject winUI;
    public GameObject failUI;


    private State currState;
    private List<GameObject> buttons;
    private bool displayHint;
    private bool startWalking;
    private int direction; // -1 means left, 0 means forward, 1 means right
    private Quaternion itemRotation;
    private GameObject newItem;
    private GameObject tundra;
    private bool death;
    private bool failure;
    private string pastCorrectAnswer;
    private bool blastFinished;

    enum moveDirection { left, right, forward};

    private int currentHint;
    private bool firebaseUsed;
    // Use this for initialization
    void Start()
    {
        death = false;
        failure = false;
        blastFinished = false;
        itemRotation = item.transform.rotation;
        direction = 0;
        startWalking = false;
        currentHint = 0;
        firebaseUsed = false;
        buttons = new List<GameObject>();
        displayHint = false;
        hintText.text = "";
        roomTitle.text = startingState.GetRoomTitle();
        currState = startingState;

        winUI.SetActive(false);
        failUI.SetActive(false);
        tundra = GameObject.FindGameObjectWithTag("Tundra");
        SetupText();
    }

    // Update is called once per frame
    void Update()
    {
        CheckForFirebaseState();

        if(currState == failState)
        {
            hintText.text = "Correct Answer: " + pastCorrectAnswer;
        }
        else
        {
            if (currState.GetHints().Length != 0)
            {
                hintText.text = currState.GetHints()[currentHint];
            }
            else
            {
                hintText.text = "";
            }
        }

        if(currState == failState && !death)
        {
            Death();
            //StartCoroutine(DeathAnimation());
            death = true;
            StartCoroutine(FailCredits());
        }

        if (blastFinished)
        {
            failUI.GetComponent<CanvasGroup>().alpha = Mathf.Lerp(failUI.GetComponent<CanvasGroup>().alpha, 1f, 0.1f);
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
        if(currState != failState && currState != victoryState)
        {
            startWalking = true;
        }
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

    public State GetVictoryState()
    {
        return victoryState;
    }

    public void setState(State state)
    {
        pastCorrectAnswer = currState.GetCorrectAnswer();
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

    public bool RoomComplete()
    {
        if (currState == victoryState)
        {
            return true;
        }
        else
        {
            return false;
        }
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
        if(newItem != null)
        {
            Destroy(newItem);
        }
        if(currState != victoryState)
        {
            newItem = Instantiate(item, itemInfo.transform.position, itemRotation);
        }
        //else
        //{
        //    newItem = Instantiate(iceFlake, itemInfo.transform.position, itemRotation);
        //}
        newItem.transform.parent = player.transform;
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

    private IEnumerator DeathAnimation()
    {
        Light eye = GameObject.FindGameObjectWithTag("LeftEye").GetComponent<Light>();
        eye.color = Color.red;
        eye = GameObject.FindGameObjectWithTag("RightEye").GetComponent<Light>();
        eye.color = Color.red;

        tundra.transform.position = tundraSpawn.position;

        //tundra.GetComponent<Rigidbody>().velocity = Vector3.down * tundraDescentSpeed;
        Vector3 velocity = tundra.GetComponent<Rigidbody>().velocity;
        tundra.GetComponent<Rigidbody>().velocity = new Vector3(velocity.x, -tundraDescentSpeed, velocity.z);
        yield return new WaitForSeconds(tundraDescentTime);
        tundra.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }

    private void Death()
    {
        Light eye = GameObject.FindGameObjectWithTag("LeftEye").GetComponent<Light>();
        eye.color = Color.red;
        eye = GameObject.FindGameObjectWithTag("RightEye").GetComponent<Light>();
        eye.color = Color.red;

        //tundra.transform.position = tundraSpawn.position;
        failure = true;

    }

    public bool Victory()
    {
        if(currState == victoryState)
        {
            return true;
        }

        return false;
    }

    public bool Failure()
    {
      return failure;
    }

    public string GetOldAnswer()
    {
        return pastCorrectAnswer;
    }

    private IEnumerator FailCredits()
    {
        yield return new WaitForSeconds(3f);
        failUI.SetActive(true);
        blastFinished = true;
    }

    public string GetRoomTitle()
    {
        return roomTitle.text;
    }
    //public GameObject GetNewItem()
    //{
    //    return newItem;
    //}

    //public void DestroyNewItem()
    //{
    //    Destroy(newItem);
    //}
}
