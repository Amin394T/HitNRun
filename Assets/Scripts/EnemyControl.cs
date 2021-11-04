using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    private Rigidbody enemyRB;
    public float speed;

    public GameObject player;
    public GameObject powerRing;
    
    private AudioSource eventSounds;
    public AudioClip playerDeathSound;

    private WaveSpawner waveSpawnerScript;


    void Start()
    {
        enemyRB = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
        powerRing = player.transform.Find("Power Ring").gameObject;

        eventSounds = GetComponent<AudioSource>();
        waveSpawnerScript = GameObject.Find("Wave Spawner").GetComponent<WaveSpawner>();
    }

    void Update()
    {
        if (player != null)
        {
            Vector3 lookDirection = (player.transform.position - transform.position).normalized;
            enemyRB.AddForce(lookDirection * speed);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject == player && powerRing.activeSelf == true)
        {
            Destroy(gameObject);
        }
        else if (other.gameObject == player && powerRing.activeSelf == false)
        {
            eventSounds.PlayOneShot(playerDeathSound);
            Destroy(other.gameObject);
            waveSpawnerScript.GameOver();
        }
    }
}