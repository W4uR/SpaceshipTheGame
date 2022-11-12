using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public TextMeshProUGUI score;
    // Start is called before the first frame update
    void Start()
    {
        score.text = LoadSaveSystem.Load().ToString();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void Description()
    {
        SceneManager.LoadScene("Description");
    }
}
