using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEditor;
using Random = UnityEngine.Random;
using static Unity.Burst.Intrinsics.X86.Avx;

public class GameManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] TMP_Text healthText;
    [SerializeField] TMP_Text scoreText;
    [SerializeField] GameOverUIManager gameOverUI;
    [Header("Prefabs")]
    [SerializeField] Faller astronautPrefab;
    [SerializeField] Faller rockPrefab;
    [Header("Gameplay")]
    [SerializeField] GameObject ship;
    [SerializeField] Transform spawnPoint;
    [Range(0f, 3f)][SerializeField] float spawnRadius = 1f;
    [Range(0f, 2f)][SerializeField] float minDistance = 1f;
    [Range(1f,3f)][SerializeField] float objectSpeed = 1.75f;
    [SerializeField] private int health;
    private int score;
    [HideInInspector]
    public bool IsGameOver { get; private set; }
    public static GameManager Instance;

    public float ObjectSpeed { get => objectSpeed; private set => objectSpeed = value; }
    [SerializeField] float SpeedIncrement = 0.05f;

    public int Health
    {
        get { return health; }
        private set
        {
            health = value;
            healthText.text = value.ToString();
        }
    }
    public int Score
    {
        get { return score; }
        private set
        {
            score = value;
            if (score % 10 == 0) ObjectSpeed += SpeedIncrement;
            scoreText.text = value.ToString();
        }
    }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        Health = health; // :)
    }

    private void Start()
    {
        objects[0] = Instantiate(astronautPrefab, GetSpawnpoint(), Quaternion.identity, spawnPoint);
        objects[1] = Instantiate(rockPrefab, GetSpawnpoint(), Quaternion.identity, spawnPoint);
    }

    public void IncrementScore()
    {
        Score++;
    }

    public void Damage()
    {
        Health--;
        if (Health <= 0)
        {
            GameOver();
        }
    }




    private Vector3 GetSpawnpoint()
    {
        return Random.Range(-spawnRadius, spawnRadius) * Vector3.right + spawnPoint.position;
    }
    Faller[] objects = new Faller[2]; //Not very happy with this solution
    public void SpawnAstronaut()
    {
        Vector3 _spawnPoint;
        do
        {
            _spawnPoint = GetSpawnpoint();
        } while ((objects[1].transform.position - _spawnPoint).sqrMagnitude <= Math.Pow(minDistance, 2));
        objects[0] = Instantiate(astronautPrefab, _spawnPoint, Quaternion.identity, spawnPoint);
    }
    public void SpawnRock()
    {
        Vector3 _spawnPoint;
        do
        {
            _spawnPoint = GetSpawnpoint();
        } while ((objects[0].transform.position - _spawnPoint).sqrMagnitude <= Math.Pow(minDistance, 2));
        objects[1] = Instantiate(rockPrefab, _spawnPoint, Quaternion.identity, spawnPoint);
    }

    private void OnDrawGizmos()
    {
        //Show green line where objects can spawn ( Editor only )
        Gizmos.color = Color.green;
        Gizmos.DrawLine(spawnPoint.position - spawnRadius * Vector3.right, spawnPoint.position + spawnRadius * Vector3.right);
        //Show minimum required distance for a successful spawn attempt
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(spawnPoint.position, minDistance);

    }

    private void GameOver()
    {
        IsGameOver = true;
        //Show gameover screen
        gameOverUI.gameObject.SetActive(true);
        //Disable gameplay elements
        healthText.gameObject.SetActive(false);
        scoreText.gameObject.SetActive(false);
        spawnPoint.gameObject.SetActive(false);
        ship.SetActive(false);
        //Save record..if it is a record
        if (LoadSaveSystem.Load() < Score)
        {
            LoadSaveSystem.Save(Score);
        }
    }
}
