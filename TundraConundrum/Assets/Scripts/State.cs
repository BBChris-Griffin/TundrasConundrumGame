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

    public State()
    {
        this.roomName = "";
        this.storyText = "";
        this.answers = new string[0];
        this.correctAnswer = "";
        this.nextStateID = new string[0];
        this.isTransition = false;
        this.hints = null;
    }

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

    public void SetRoomTitle(string room)
    {
        this.roomName = room;
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

    public string GetID()
    {
        return id;
    }

    public string GetStoryText()
    {
        return storyText;
    }

    public void SetStoryText(string question)
    {
        storyText = question;
    }

    public string[] GetAnswers()
    {
        return answers;
    }

    public void SetAnswers(string[] answers)
    {
        this.answers = answers;
    }

    public string GetCorrectAnswer()
    {
        return correctAnswer;
    }

    public void SetCorrectAnswer(string correctAnswer)
    {
        this.correctAnswer = correctAnswer;
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

    public void SetHints(string[] hints)
    {
        this.hints = hints;
    }

    public void ShuffleAnswers()
    {
        for(int i = 0; i < answers.Length; i++)
        {
            int rand = Random.Range(0, answers.Length);
            if(rand != i)
            {
                Swap(ref answers[i], ref answers[rand]);
            }         
        }
    }

    private void Swap(ref string a, ref string b)
    {
        string temp = a;
        a = b;
        b = temp;
    }
}
