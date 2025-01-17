using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Astronaut : Faller
{
    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Player"))
        {
            GameManager.Instance.IncrementScore();
        }
        if (other.CompareTag("Border") || other.CompareTag("Player"))
        {
            GameManager.Instance.SpawnAstronaut();
            Destroy(gameObject);
        }
    }
}
