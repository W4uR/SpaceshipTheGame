using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Astronaut : MonoBehaviour
{
    public GameObject element;
    public GameObject ScoreAndHealth;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI healthText;
    float speed = -1.75f;
    public GameObject endGameDialog;
    public GameObject rockAndAstronaut;
    public GameObject UIElements;
    public TextMeshProUGUI endGameScoreText;
    public GameObject ship;
    public GameObject another;

    void Start()
    {   
        element.transform.position = new Vector2((float)GetRandomNumber(-2.3, 2.3), 3f);
        healthText.text = ScoreAndHealth.GetComponent<ScoreAndHealth>().health.ToString();
        endGameDialog.SetActive(false);
    }

    void Update()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(0f, speed);
    }

    public double GetRandomNumber(double minimum, double maximum)
    {
        System.Random random = new System.Random();
        return random.NextDouble() * (maximum - minimum) + minimum;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        string score = ScoreAndHealth.GetComponent<ScoreAndHealth>().score;
        string health = ScoreAndHealth.GetComponent<ScoreAndHealth>().health;
        if(name == "astronaut" && other.name == "Ship")
        {
            int tmp = int.Parse(score) + 1;
            ScoreAndHealth.GetComponent<ScoreAndHealth>().score = tmp.ToString();
            scoreText.text = tmp.ToString();

            if (tmp % 10 == 0)
            {
                speed -= 0.5f;
                another.GetComponent<Astronaut>().speed -= 0.5f;
            }
            element.transform.position = new Vector2((float)GetRandomNumber(-2.3, 2.3), 5.52f);
        } else if (name == "rock" && other.name == "Ship") {
            int tmp = int.Parse(health) - 1;
            ScoreAndHealth.GetComponent<ScoreAndHealth>().health = tmp.ToString();
            healthText.text = tmp.ToString();

            if(tmp == 0)
            {
                ShowEndGameDialog();
            }
            element.transform.position = new Vector2((float)GetRandomNumber(-2.3, 2.3), 5.52f);
        } else if (other.name == "BottomBorder")
        {
            element.transform.position = new Vector2((float)GetRandomNumber(-2.3, 2.3), 5.52f);
        }
    }

    private void ShowEndGameDialog()
    {
        string scoreTmp = ScoreAndHealth.GetComponent<ScoreAndHealth>().score;
        UIElements.SetActive(false);
        rockAndAstronaut.SetActive(false);
        endGameDialog.SetActive(true);
        ship.SetActive(false);
        endGameScoreText.text = scoreTmp;
        if(int.Parse(LoadSaveSystem.Load())<int.Parse(scoreTmp))
        {
            LoadSaveSystem.Save(scoreTmp);
        }
    }
}
