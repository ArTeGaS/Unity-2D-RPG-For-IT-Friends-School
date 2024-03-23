using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_Controller : MonoBehaviour
{
    public GameObject projectilePrefab; // Префаб проєктиля

    private void Start()
    {
        StartCoroutine(SpawnProjectileEverySecond());
    }

    private IEnumerator SpawnProjectileEverySecond()
    {
        while (true)
        {
            SpawnProjectile();
            yield return new WaitForSeconds(0.9f);
        }
    }

    private void SpawnProjectile()
    {
        Instantiate(projectilePrefab, transform.position, Quaternion.identity);
    }
}
