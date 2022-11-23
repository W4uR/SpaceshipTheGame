using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOverUIManager : MonoBehaviour
{
    [SerializeField] TMP_Text score;
    private void OnEnable()
    {
        score.text = GameManager.Instance.Score.ToString();
    }

    public void OnMenuButtonClicked()
    {
        SceneManager.LoadScene("Menu");
    }

    public void OnRestartButtonClicked()
    {
        SceneManager.LoadScene("GameScene");
    }

}
