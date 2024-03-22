using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

using UnityEngine;

public class MovingPlatform : MonoBehaviour
{

    
    [SerializeField]
    
    public Transform posA,posB; 
    public int speed; 
    Vector2 targetPosition;


void Start(){
    targetPosition = posB.position;
}

void Update(){
    if(Vector2.Distance(transform.position, posA.position) < .1f)targetPosition = posB.position;

    if(Vector2.Distance(transform.position, posB.position) < .1f)targetPosition = posA.position;

    transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
}
    
    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.tag == "Player"){
            collision.gameObject.transform.SetParent(transform);
        }
    }
    private void OnTriggerExit2D(Collider2D collision){
        if(collision.gameObject.tag == "Player"){
            collision.gameObject.transform.SetParent(transform);
        }
    }
   
  
    
   
}
