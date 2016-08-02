using UnityEngine;
using System.Collections;

public struct Setup
{
    private static Quiz quiz;

    public static Quiz GetQuiz()
    {
        if (quiz == null)
            quiz = CreateDefaultQuiz();

        return quiz;
    }

    public static void SetQuiz(Quiz quiz)
    {
        Setup.quiz = quiz;
    }

    public static bool IsValid()
    {
        return quiz != null && quiz.IsValid();
    }

    private static Quiz CreateDefaultQuiz()
    {
        Quiz quiz = new Quiz();
        for (int i = 0; i < Quiz.QUESTION_COUNT; i++) {
            Question question = new Question();
            question.Text = "Question " + (i + 1);
            quiz.SetQuestion(i, question);
        }

        return quiz;
    }
}
