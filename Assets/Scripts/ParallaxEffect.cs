using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//POMJERANJE KAMERE EFEKTI
public class ParallaxEffect : MonoBehaviour
{


    public Camera cam;
    public Transform followTarget;


    //Starting position for parallax game object
    Vector2 startingPosition;

    //Starting X value of the parallax game object
    float startingZ;

    Vector2 camMoveSinceStart => (Vector2)cam.transform.position - startingPosition;

    float distanceFromTarget => transform.position.z - followTarget.transform.position.z;

    float clippingPane => (cam.transform.position.z + (distanceFromTarget > 0 ? cam.farClipPlane : cam.nearClipPlane));
    float parallaxFactor => Mathf.Abs(distanceFromTarget) / clippingPane;
    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
        startingZ = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        //When tarrget moves, move the parallax object the same distance as a multiplier
        Vector2 newPosition = startingPosition + camMoveSinceStart * parallaxFactor;

        // The x/y position chanes based on target travel speed times the parallax factor but Z stays consistent
        transform.position = new Vector3(newPosition.x, newPosition.y, startingZ);
    }

}
