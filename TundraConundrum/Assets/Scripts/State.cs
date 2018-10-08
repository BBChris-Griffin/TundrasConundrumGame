using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "State")]
public class State : ScriptableObject {

    [TextArea(14, 10)] [SerializeField] string storyText;
    [SerializeField] string[] answers;
    [SerializeField] string correctAnswer;
    [SerializeField] State[] nextState;
    [SerializeField] bool isTransition;
    [SerializeField] string[] hints;

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
