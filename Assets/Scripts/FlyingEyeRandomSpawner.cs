using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FlyingEyeRandomSpawner : MonoBehaviour
{

    public Transform[] spawnPoints;
    public FlyingEye enemyPrefabs;
    public List<Transform> waypoints;
    public float interval;
    public int n = 2;
    private float counter = 0;
    int randSpawnPoint;
    // Start is called before the first frame update
    void Start()
    {

    }

    void Awake()
    {
        waypoints = new List<Transform>(n);
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        counter += 1;
        while (waypoints == null){
            Debug.Log("waypoints is null");
        }
        if (counter >= interval && waypoints == null)
        {
            int randEnemy = Random.Range(0, 1);
            int randSpawnPoint = Random.Range(0, spawnPoints.Length);

            //Position of the point for enemy to spawn on
            Transform choosenPoint = spawnPoints[randSpawnPoint];
            for (int i = 0; i < waypoints.Capacity; i++)
            {
                GameObject newWaypointGo = new GameObject("Waypoint");
                newWaypointGo.transform.position = new Vector3(choosenPoint.transform.position.x + i, choosenPoint.transform.position.y + i, choosenPoint.transform.position.z);
                waypoints[i] = newWaypointGo.transform;
                
            }
            enemyPrefabs.waypoints = waypoints;

            Instantiate(enemyPrefabs, spawnPoints[randSpawnPoint].position, transform.rotation);
            counter = 0;
        }
    }

    
}
