using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
public class FlyingEyeEnemySpawner : MonoBehaviour
{
    public FlyingEye enemyPrefab;
    public float interval = 100;
    private float counter = 0;
    GameObject[] waypoints;
    public void Awake()
    {
        waypoints = GetWaypointsChildren();
        if (waypoints != null)
        {
            Debug.Log("tooo");
            Debug.Log(waypoints.Length);

        }
        else
        {
            Debug.Log("nooo");
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        counter += 1;

        if (counter >= interval)
        {
            counter = 0;
            for (int i = 0; i < waypoints.Length; i++)
            {
                enemyPrefab.waypoints.Add(waypoints[i].transform);

            }

            Instantiate(enemyPrefab, transform.position += new Vector3(0, 0, counter) * counter, transform.rotation);
            enemyPrefab.waypoints.Clear();


        }
    }


    public GameObject[] GetWaypointsChildren()
    {
        GameObject waypointsObject = GameObject.Find("Waypoints");
        Transform[] waypointsChildren = waypointsObject.GetComponentsInChildren<Transform>();
        GameObject[] childrenObjects = new GameObject[waypointsChildren.Length - 1]; // excluding the parent
        for (int i = 1; i < waypointsChildren.Length; i++) // starting from 1 to exclude the parent object
        {
            childrenObjects[i - 1] = waypointsChildren[i].gameObject;
        }
        return childrenObjects;
    }
}


*/