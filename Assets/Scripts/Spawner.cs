using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private Enemy enemyPrefab;

    private float nextSpawnTime;

    [SerializeField]
    private float respawnRate = 5;

    // Start is called before the first frame update
    void Start()
    {
        Spawn();
    }

    private void Spawn()
    {
        var enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        
        
    }


    // Update is called once per frame
    void Update()
    {
        
        if (Time.time >= nextSpawnTime)
        {
            //Spawn();
            nextSpawnTime = Time.time + respawnRate;
        }
    }
}
