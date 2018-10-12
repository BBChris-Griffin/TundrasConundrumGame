using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "State")]
public class State : ScriptableObject {

    [TextArea(14, 10)] [SerializeField] string storyText;
    [SerializeField] string roomName;
    [SerializeField] string[] answers;
    [SerializeField] string correctAnswer;
    [SerializeField] State[] nextState;
    [SerializeField] bool isTransition;
    [SerializeField] string[] hints;
    private string id;
    private string[] nextStateID;

    public State(string roomName, string question, string[] answers, string correctAnswer, string[] nextStateID,
                  bool isTransition, string[] hints)
    {
        this.roomName = roomName;
        this.storyText = question;
        this.answers = answers;
        this.correctAnswer = correctAnswer;
        this.nextStateID = nextStateID;
        this.isTransition = isTransition;
        this.hints = hints;
    }


    public string GetRoomTitle()
    {
        return roomName;
    }

    public void SetNextState(State[] nextState)
    {
        this.nextState = nextState;
    }

    public void SetNextStateID(string[] nextID)
    {
        this.nextStateID = nextID;
    }

    public void SetID(string id)
    {
        this.id = id;
    }

    public string GetStoryText()
    {
        return storyText;
    }

    public string[] GetAnswers()
    {
        return answers;
    }

    public string GetCorrectAnswer()
    {
        return correctAnswer;
    }

    public State[] GetNextState()
    {
        return nextState;
    }

    public bool GetIsTransition()
    {
        return isTransition;
    }

    public string[] GetHints()
    {
      return hints;
    }
}
