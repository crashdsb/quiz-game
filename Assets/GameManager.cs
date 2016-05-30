using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public Question[] questions;
    private static List<Question> unansweredQuestions;

    private Question currentQuestion;

    [SerializeField]
    private Text factText;

    [SerializeField]
    private float timeBetweenQuestions = 2f;

    [SerializeField]
    private Text trueAnswerText;
    [SerializeField]
    private Text falseAnswerText;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private Text scoreText;

    [SerializeField]
    private static int scorePoints;

    //This is the ResetScorePoints
    [SerializeField]
    private int resetScorePoints;


    void Start()
    {
        UpdateScore();

        if (unansweredQuestions == null || unansweredQuestions.Count == 0)
        {
            unansweredQuestions = questions.ToList<Question>();
        }

        SetCurrentQuestion();

    }

    //This void method calls for Score: to = 0
    public void resetPoints()
    {
        scorePoints = resetScorePoints;
        UpdateScore();
    }

    //This void calls for the Update Score Method.
    void UpdateScore()
    {
        scoreText.text = "Score: " + scorePoints;
    }

    void SetCurrentQuestion ()
    {
        int randomQuestionIndex = Random.Range(0, unansweredQuestions.Count);
        currentQuestion = unansweredQuestions[randomQuestionIndex];

        factText.text = currentQuestion.fact;

        if (currentQuestion.isTrue)
        {
            trueAnswerText.text = "CORRECT";
            falseAnswerText.text = "WRONG";
        } else
        {
            trueAnswerText.text = "WRONG";
            falseAnswerText.text = "CORRECT";
        }
    }

    //This transitions between questions.
    IEnumerator TransitionToNextQuestion ()
    {
        unansweredQuestions.Remove(currentQuestion);

        yield return new WaitForSeconds(timeBetweenQuestions);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

    public void UserSelectTrue()
    {
        animator.SetTrigger("True");
        if (currentQuestion.isTrue)
        {
            AddScore(1);
            Debug.Log("CORRECT!");
        } else
        {
            AddScore(-1);
            Debug.Log("WRONG!");
        }
        StartCoroutine(TransitionToNextQuestion());

    }

    public void UserSelectFalse()
    {
        animator.SetTrigger("False");
        if (!currentQuestion.isTrue)
        {
            AddScore(1);
            Debug.Log("CORRECT!");
        }
        else
        {
            AddScore(-1);
            Debug.Log("WRONG!");
        }

        StartCoroutine(TransitionToNextQuestion());
    }

    //This is the UpdateScore method, which updates score +1 or -1 per question.
    public void AddScore(int newScore)
    {
        scorePoints += newScore;
        UpdateScore();
    }

    //This calls for Menu Button to Send Player to Main Menu, and Reset Points.
    public void LoadScene(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
    }

    public void MenuButtonClicked(string SceneName)
    {
        resetPoints();
        LoadScene(SceneName);
    }

}
