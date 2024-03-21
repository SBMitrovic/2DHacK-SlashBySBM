using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightRandomSpawner : MonoBehaviour
{   

    public Transform[] spawnPoints; 
    public GameObject enemyPrefabs;
    public float interval;
    private float counter = 0;
    int randSpawnPoint;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate(){
        counter += 1;

        if(counter >= interval){
            int randEnemy = Random.Range(0, 1);
            int randSpawnPoint = Random.Range(0, spawnPoints.Length);
            Instantiate(enemyPrefabs, spawnPoints[randSpawnPoint].position, transform.rotation);
            counter = 0;
        }
    }
}
