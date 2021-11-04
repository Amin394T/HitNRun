using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Experimental.UIElements;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject powerUpPrefab;
    public int enemyCount;
    public int powerUpCount;
    public float enemyInterval;
    public float powerUpInterval;

    public int waveNumber;
    public AudioSource eventSounds;
    public TextMeshProUGUI waveNumberText;
    public AudioClip newWaveSound;

    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI titleScreenText;
    public bool isGameActive = false;

    private void Start()
    {
        eventSounds = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (isGameActive)
        {
            enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
            if (enemyCount == 0)
            {
                SpawnEnemyWave(waveNumber + 1);
                waveNumber++;
                waveNumberText.text = "WAVE COUNT: " + waveNumber;
                eventSounds.PlayOneShot(newWaveSound, 3.0f); ;
            }

            powerUpCount = GameObject.FindGameObjectsWithTag("Power Up").Length;
            if (powerUpCount == 0)
            {
                SpawnPowerUp(waveNumber / 5 + 1);
            }
        }
    }


    void SpawnEnemyWave(int spawnedEnemyCount)
    {
        for (int i = 0; i < spawnedEnemyCount; i++)
        {
            Instantiate(enemyPrefab, GenerateSpawnPos(), enemyPrefab.transform.rotation);
        }
    }

    void SpawnPowerUp (int powerUpCount)
    {
        for (int i = 0; i < powerUpCount; i++)
        {
            Instantiate(powerUpPrefab, GenerateSpawnPos(), powerUpPrefab.transform.rotation);
        }
    }

    Vector3 GenerateSpawnPos()
    {
        float spawnRange = 90.0f;
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);
        return new Vector3(spawnPosX, 1, spawnPosZ);
    }


    public void StartGame()
    {
        titleScreenText.gameObject.SetActive(false);
        isGameActive = true;
    }

    public void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
