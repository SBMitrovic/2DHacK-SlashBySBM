using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float interval = 100;
    private float counter = 0;
    public GameObject WayPoints;
    public void Awake(){
        WayPoints = GameObject.Find("WayPoints");
        if(WayPoints != null){
            Debug.Log("tooo");    
        }
        
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        counter += 1;

        if(counter >= interval){
            counter = 0;
            Instantiate(enemyPrefab, transform.position,transform.rotation);

        }
    }
}