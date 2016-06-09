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
    private float timeBetweenQuestions = 4f;

    [SerializeField]
    private Text trueAnswerText;
    [SerializeField]
    private Text falseAnswerText;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private Text scoreText;

    [SerializeField]
    private Text healthText;

    [SerializeField]
    private static int healthLives;

    [SerializeField]
    private static int resetScorePoints;

    [SerializeField]
    private static int scorePoints;


    void Start()
    {
        UpdateScore();

        UpdateHealth();

        if (healthLives == 0)
            HealthLoss(3);
        else {
            HealthLoss(0);
        }

        if (unansweredQuestions == null || unansweredQuestions.Count == 0)
        {
            unansweredQuestions = questions.ToList<Question>();
        }

        SetCurrentQuestion();

    }

    void UpdateHealth()
    {
        healthText.text = "Lives: " + healthLives;

    }

    public void HealthLoss(int newHealth)
    {
        healthLives += newHealth;
        UpdateHealth();
        if (healthLives == 0) SceneManager.LoadScene("Menu");
        {
        }
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


    void UpdateText()
    {
        trueAnswerText.text = "TRUE";
        falseAnswerText.text = "FALSE";
    }



        void SetCurrentQuestion ()
    {
        int randomQuestionIndex = Random.Range(0, unansweredQuestions.Count);
        currentQuestion = unansweredQuestions[randomQuestionIndex];

        factText.text = currentQuestion.fact;

    }

    //This transitions between questions.
    IEnumerator TransitionToNextQuestion ()
    {
        unansweredQuestions.Remove(currentQuestion);

        yield return new WaitForSeconds(timeBetweenQuestions);

        SetCurrentQuestion();

        UpdateText();

        factText.text = currentQuestion.fact;
        
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

    public void UserSelectTrue()
    {
        if (currentQuestion.isTrue)
        {
            AddScore(1);
            trueAnswerText.text = "CORRECT";
            Debug.Log("CORRECT!");
        } else
        {
            trueAnswerText.text = "INCORRECT";
            AddScore(-1);
            HealthLoss(-1);
            Debug.Log("WRONG!");
        }
        StartCoroutine(TransitionToNextQuestion());
    }

    public void UserSelectFalse()
    {
        if (!currentQuestion.isTrue)
        {
            falseAnswerText.text = "CORRECT!";
            AddScore(1);
            Debug.Log("CORRECT!");
        }
        else
        {
            AddScore(-1);
            HealthLoss(-1);
            falseAnswerText.text = "INCORRECT";
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
