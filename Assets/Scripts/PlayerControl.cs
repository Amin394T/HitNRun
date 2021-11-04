using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private Rigidbody playerRB;
    public GameObject mainCamera;

    public GameObject powerRing;
    public float speed;
    public float powerUpDelay;

    public AudioClip powerUpSound;
    private WaveSpawner waveSpawnerScript;


    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        waveSpawnerScript = GameObject.Find("Wave Spawner").GetComponent<WaveSpawner>();
    }

    void Update()
    {
        if (waveSpawnerScript.isGameActive)
        {
            float verticalInput = Input.GetAxis("Vertical");
            playerRB.AddForce(mainCamera.transform.forward * speed * verticalInput);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.name == "Power Up(Clone)" && powerRing.activeSelf == false)
        {
            Destroy(other.gameObject);
            waveSpawnerScript.eventSounds.PlayOneShot(powerUpSound, 3.0f);
            powerRing.gameObject.SetActive(true);
            StartCoroutine(PowerUpCoroutine());
        }
    }

    IEnumerator PowerUpCoroutine()
    {
        yield return new WaitForSeconds(powerUpDelay);
        powerRing.gameObject.SetActive(false);
    }
}
