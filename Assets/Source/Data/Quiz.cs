using UnityEngine;
using System.Collections;

[System.Serializable]
public class Quiz
{
    public const int QUESTION_COUNT = 15;

    /// <summary>
    /// The questions.
    /// </summary>
    [SerializeField]
    private Question[] questions;

    public Quiz ()
    {
        questions = new Question[QUESTION_COUNT];
    }

    public Question GetQuestion(int index)
    {
        if (questions != null && index >= 0 && index < questions.Length - 1)
            return questions[index];

        return null;
    }

    public void SetQuestion(int index, Question question)
    {
        if (index >= 0 && index < QUESTION_COUNT)
        {
            questions[index] = question;
        }
        else
        {
            throw new UnityException("Wrong index.");
        }
    }

    public bool IsValid()
    {
        if (questions == null || questions.Length != QUESTION_COUNT)
            return false;

        foreach (var item in questions) {
            if (item == null)
                return false;
        }

        return true;
    }
}
