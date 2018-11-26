using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

using SimpleFirebaseUnity;
using SimpleFirebaseUnity.MiniJSON;

public class FirebaseData : MonoBehaviour {

    //public string userID;
    public string roomID;
    public bool webBuild;
    public State blankState;

    private State startState;
    private List<State> puzzleStates;
    private bool set;
    private string firebaseID;
    private bool ready;
    private bool gameFinished;
    private int count;
    private AdventureGame game;

    // Use this for initialization
    void Awake () {
        if(GlobalVariables.reset && webBuild)
        {
            roomID = GlobalVariables.savedRoomID;
        }
        count = 0;
        ready = false;
        set = false;
        gameFinished = false;
        firebaseID = "https://tundrasconundrum-6af20.firebaseio.com";
        puzzleStates = new List<State>();
        startState = new State();
        if(!webBuild)
        {
            Console.WriteLine("Not a Web Build");
            GetData(roomID);
            ready = true;
        }
    }

    void Start()
    {
      GameObject advGameObject = GameObject.FindGameObjectWithTag("GameController");
      if(advGameObject != null)
      {
            game = advGameObject.GetComponent<AdventureGame>();
      }
    }

    // Update is called once per frame
    void Update ()
    {
        if(webBuild)
        {
            if (set && !GlobalVariables.reset)
            {
                GetData(roomID);
                Console.WriteLine("Actual ID on 1nd build " + roomID);
                Console.WriteLine("Saved ID on 1nd build " + GlobalVariables.savedRoomID);
                set = false;
                ready = true;
            }
            else if(!set && GlobalVariables.reset)
            {
                Console.WriteLine("Getting data from right place");
                Console.WriteLine("ID on 2nd build " + GlobalVariables.savedRoomID);
                GetData(GlobalVariables.savedRoomID);
                set = true;
                ready = true;
            }
        }

        if (game.RoomComplete() && !gameFinished)
        {
            GetCountData();
            gameFinished = true;
        }
    }

    public void SetFirebase(string firebaseID)
    {
        this.firebaseID = firebaseID;
    }

    public void SetRoomID(string room)
    {
        this.roomID = room;
        set = true;
        GlobalVariables.savedRoomID = room;
    }

    void GetData(string roomID)
    {
        Firebase firebase = Firebase.CreateNew(firebaseID, "");
        //Firebase user = firebase.Child("rooms").Child(roomID);
        Firebase user = firebase;
        user.OnGetSuccess += GetDataHandler;
        user.GetValue("print=pretty");
        user.OnGetSuccess -= GetDataHandler;

    }

    void GetCountData()
    {
        Firebase firebase = Firebase.CreateNew(firebaseID, "");
        Firebase user = firebase.Child("rooms");

        FirebaseQueue firebaseQueue = new FirebaseQueue(true, 1, 1.0f);
        count++;

        firebaseQueue.AddQueueUpdate(user.Child(roomID), "{ \"finishCount\": \""+ count + "\"}");
    }

    void GetDataHandler(Firebase sender, DataSnapshot snapshot)
    {
        Dictionary<string, object> dict = snapshot.Value<Dictionary<string, object>>();
        List<string> keys = snapshot.Keys;

        dict = (Dictionary<string, object>) dict["rooms"];
        dict = (Dictionary<string, object>)dict[roomID];

        // Get Room Name
        string roomName = dict["name"].ToString();
        startState.SetRoomTitle(roomName);

        // Get Count Keys
        foreach (string key in dict.Keys)
        {
            if(key == "finishCount")
            {
                count = int.Parse(dict[key].ToString());
            }
        }
        // Get Puzzle ID
        try
        {
            dict = (Dictionary<string, object>)dict["puzzles"];
        }
        catch
        {
            // If no puzzle is in room, use blank state
            puzzleStates.Add(blankState);
        }

        foreach (string key in dict.Keys)
        {
            State newState = new State();
            newState.SetID(key);
            newState.SetRoomTitle(startState.GetRoomTitle());
            puzzleStates.Add(newState);
        }

        Dictionary<string, object> saveDict = snapshot.Value<Dictionary<string, object>>();
        saveDict = (Dictionary<string, object>)saveDict["puzzles"];
        Dictionary<string, object> puzzleDict;
        List<string> answers = new List<string>();
        List<string> hints = new List<string>();
        List<string> transitions = new List<string>();
        List<State> stateList = new List<State>();

        for (int i = 0; i < puzzleStates.Count; i++)
        {
            puzzleDict = (Dictionary<string, object>)saveDict[puzzleStates[i].GetID()];

            // Get Questions
            puzzleStates[i].SetStoryText(puzzleDict["question"].ToString());

            // Get Answers
            dict = (Dictionary<string, object>)puzzleDict["answers"];
            puzzleStates[i].SetCorrectAnswer(dict["correct"].ToString());
            answers.Add(dict["correct"].ToString());
            if (dict.ContainsKey("wrong1"))
            {
                answers.Add(dict["wrong1"].ToString());
            }
            if (dict.ContainsKey("wrong2"))
            {
                answers.Add(dict["wrong2"].ToString());
            }
            if (dict.ContainsKey("wrong3"))
            {
                answers.Add(dict["wrong3"].ToString());
            }
            puzzleStates[i].SetAnswers(answers.ToArray());
            answers.Clear();

            // Get Hints
            foreach (string key in puzzleDict.Keys)
            {
                if(key == "hints")
                {
                    dict = (Dictionary<string, object>)puzzleDict["hints"];
                }
            }

            hints.Add("");
            if (dict.ContainsKey("hint1"))
            {
                hints.Add(dict["hint1"].ToString());
            }
            if (dict.ContainsKey("hint2"))
            {
                hints.Add(dict["hint2"].ToString());
            }
            if (dict.ContainsKey("hint3"))
            {
                hints.Add(dict["hint3"].ToString());
            }
            puzzleStates[i].SetHints(hints.ToArray());
            hints.Clear();

            //Get Transitions
            foreach (string key in puzzleDict.Keys)
            {
                if (key == "transitions")
                {
                    dict = (Dictionary<string, object>)puzzleDict["transitions"];
                }
            }

            //Correct Transition
            if (dict.ContainsKey("right"))
            {
                transitions.Add(dict["right"].ToString());
            }
            else
            {
                transitions.Add("null");
            }

            //Wrong Transition
            if (dict.ContainsKey("left"))
            {
                transitions.Add(dict["left"].ToString());
            }
            else
            {
                transitions.Add("null");
            }

            puzzleStates[i].SetNextStateID(transitions.ToArray());

            for (int k = 0; k < 2; k++)
            {
                for (int j = 0; j < puzzleStates.Count; j++)
                {
                    if (puzzleStates[i].GetNextStateID()[k] == puzzleStates[j].GetID())
                    {
                        stateList.Add(puzzleStates[j]);
                        break;
                    }
                    else if(puzzleStates[i].GetNextStateID()[k] == "null")
                    {
                        stateList.Add(puzzleStates[i]);
                        break;
                    }
                }
            }
            puzzleStates[i].SetNextState(stateList.ToArray());
            transitions.Clear();
            stateList.Clear();
        }
    }

    public State GetStartState()
    {
        return puzzleStates[0];
    }

    public bool DataRetrieved()
    {
        return ready;
    }
}
