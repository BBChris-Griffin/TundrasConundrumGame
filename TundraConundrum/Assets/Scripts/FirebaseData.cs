using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SimpleFirebaseUnity;
using SimpleFirebaseUnity.MiniJSON;

public class FirebaseData : MonoBehaviour {

    //public string userID;
    public string roomID;

    private State startState;
    private List<State> puzzleStates;


    // Use this for initialization
    void Awake () {
        puzzleStates = new List<State>();
        startState = new State();
        GetData(roomID);
	}

	// Update is called once per frame
	void Update () {

	}

    void GetData(string roomID)
    {
        Firebase firebase = Firebase.CreateNew("https://tundrasconundrum-31ea5.firebaseio.com", "");
        //Firebase user = firebase.Child("rooms").Child(roomID);
        Firebase user = firebase;
        user.OnGetSuccess += GetDataHandler;
        user.GetValue("print=pretty");
        user.OnGetSuccess -= GetDataHandler;

    }

    void GetDataHandler(Firebase sender, DataSnapshot snapshot)
    {
        //Debug.Log("[OK] Get from key: <" + sender.FullKey + ">");
        //Debug.Log("[OK] Raw Json: " + snapshot.RawJson);

        Dictionary<string, object> dict = snapshot.Value<Dictionary<string, object>>();
        List<string> keys = snapshot.Keys;

        dict = (Dictionary<string, object>) dict["rooms"];
        dict = (Dictionary<string, object>)dict[roomID];

        // Get Room Name
        string roomName = dict["name"].ToString();
        startState.SetRoomTitle(roomName);

        // Get Puzzle ID
        dict = (Dictionary<string, object>)dict["puzzles"];
        foreach (string key in dict.Keys)
        {
            State newState = new State();
            newState.SetID(key);
            newState.SetRoomTitle(startState.GetRoomTitle());
            puzzleStates.Add(newState);
        }

        dict = snapshot.Value<Dictionary<string, object>>();
        dict = (Dictionary<string, object>)dict["puzzles"];
        Dictionary<string, object> puzzleDict;
        List<string> answers = new List<string>();
        List<string> hints = new List<string>();

        for (int i = 0; i < puzzleStates.Count; i++)
        {
            puzzleDict = (Dictionary<string, object>)dict[puzzleStates[i].GetID()];

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
        }
    }

    void RetrieveData(DataSnapshot snapshot)
    {
        Dictionary<string, object> dict = snapshot.Value<Dictionary<string, object>>();
        List<string> keys = snapshot.Keys;

        string roomName = dict["name"].ToString();
        startState.SetRoomTitle(roomName);
        for (int i = 0; i < 2; i++)
        {
            dict = (Dictionary<string, object>)dict["puzzles"];
            //startState.SetID(dict.);
        }
    }

    void ParseDate(string snapShotData)
    {
        bool getPuzzleID = false;
        bool getAnswers = false;
        bool getHints = false;
        bool getQuestion = false;
        bool getStartPuzzle = true;
        bool getNewPuzzle = false;
        string[] firebaseData = snapShotData.Split('\n');
        for(int i = 0; i < firebaseData.Length; i++)
        {
            if (getStartPuzzle)
            {
                if (getPuzzleID)
                {
                    string[] temp = firebaseData[i].Split(' ');
                    startState.SetID(temp[1]);
                    Debug.Log("Id " + temp.Length);
                    getPuzzleID = false;
                }
            }
            if (firebaseData[i] == "  \"puzzles\" : {")
            {
                getPuzzleID = true;
            }
            else if (firebaseData[i] == "\"answers\" : {")
            {
                getAnswers = true;
            }
            else if (firebaseData[i] == "\"hints\" : {")
            {
                getHints = true;
            }
            else if (firebaseData[i] == "\"question\" : {")
            {
                getQuestion = true;
            }
        }
    }

    public State GetStartState()
    {
        return puzzleStates[0];
    }
}
