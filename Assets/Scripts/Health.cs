using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour {

    [SerializeField]
    private Text healthText;

    [SerializeField]
    private static int healthLives;

    // Use this for initialization
    void Start () {

        UpdateHealth();

        HealthLoss(3);
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


    // Update is called once per frame
    void Update () {
	
	}
}
