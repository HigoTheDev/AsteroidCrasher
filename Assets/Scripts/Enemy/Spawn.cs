using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject thienthach;
    public GameObject hanhtinhxanh;
    public GameObject hanhtinhdo;

    public float spawnInterval = 1.5f;
    public float xRange = 8f;

    private void Start()
    {
        InvokeRepeating("SpawnEnemy", 1f, spawnInterval);
    }

    void SpawnEnemy()
    {
        float randX = Random.Range(-xRange, xRange);
        Vector3 spawnPos = new Vector3(randX, 6f, 0f);
        float r = Random.value;
        GameObject prefabToSpawn;

        if (r < 0.6f) prefabToSpawn = thienthach;
        else if (r < 0.9f) prefabToSpawn = hanhtinhxanh;
        else prefabToSpawn= hanhtinhdo;

        Instantiate(prefabToSpawn, spawnPos, Quaternion.identity);
    }
}
