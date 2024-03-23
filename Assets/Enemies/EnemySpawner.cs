using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    float height;
    float width;

    GameObject player;
    public GameObject enemy;
    private void Start()
    {
        player = GameObject.Find("Player");
        InvokeRepeating("SpawnEnemy", 0, 1);
    }
    private void SpawnEnemy()
    {
        Vector3 spawnPosition = player.transform.position;

        float h_or_w = Random.Range(0, 2);
        float p_or_m = Random.Range(0, 2);
        switch (h_or_w)
        {
            case 0:
                height = Random.Range(0f, 8.1f);
                width = 14f;
                break;
            case 1:
                height = 8f;
                width = Random.Range(0f, 14.1f);
                break;
        }
        switch (p_or_m)
        {
            case 0:
                spawnPosition.x = spawnPosition.x + width;
                spawnPosition.y = spawnPosition.y + height;
                break; 
            case 1:
                spawnPosition.x = spawnPosition.x - width;
                spawnPosition.y = spawnPosition.y - height;
                break;
        }
        Instantiate(enemy, spawnPosition, Quaternion.identity);
    }
}
