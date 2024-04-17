using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // [] after below this makes it into a LENGTH
    public GameObject[] ObsticlePrefab;
    private Vector3 spawnPos = new Vector3(25, 0, 0);
    private float startDelay = 2f;
    private float spawnInterval = 2f;
    private PlayerController playerControllerScript;
    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        InvokeRepeating("SpawnRandomObsticle", startDelay, spawnInterval);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnRandomObsticle()
    {
        if (playerControllerScript.gameOver == false)
        {
            int randomObsticleIndex = Random.Range(0, ObsticlePrefab.Length);
            Vector3 SpawnManager = spawnPos;
            Instantiate(ObsticlePrefab[randomObsticleIndex], spawnPos, ObsticlePrefab[randomObsticleIndex].transform.rotation);
        }
       
    }
}
