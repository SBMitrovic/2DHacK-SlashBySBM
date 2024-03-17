using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

[RequireComponent(typeof(TouchingDirections), typeof(Rigidbody2D))]
public class KnightScript : MonoBehaviour
{
    public float walkSpeed = 3f;

    Rigidbody2D rb;
    TouchingDirections touchingDirections;
    private Vector2 walkDirectionVector = Vector2.right;
    private WalkDirection _walkDirection = WalkDirection.Right;

    public enum WalkDirection
    {
        Right, Left
    }

    public WalkDirection walkDirection
    {
        get
        {
            return _walkDirection;
        }
        set
        {
            if (_walkDirection != value)
            { //Direction needs to be flipped
                gameObject.transform.localScale = new Vector2 (gameObject.transform.localScale.x * -1, gameObject.transform.localScale.y);

                if(value == WalkDirection.Left){
                    walkDirectionVector = Vector2.left;
                }else if(value == WalkDirection.Right){
                    walkDirectionVector = Vector2.right;
                }
            }
            _walkDirection = value;
        }
    }
    // Start is called before the first frame update
    void Start()
    {

    }
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        touchingDirections = GetComponent<TouchingDirections>();
    }

    void FixedUpdate()
    {
        if (touchingDirections.isOnWall && touchingDirections.isGrounded)
        {
            ChangeWalkingDirection();
        }
        rb.velocity = new Vector2(walkSpeed * walkDirectionVector.x, rb.velocity.y);
    }

    private void ChangeWalkingDirection()
    {
        if(walkDirection == WalkDirection.Right){
            walkDirection = WalkDirection.Left;
        } else if(walkDirection == WalkDirection.Left){
            walkDirection = WalkDirection.Right;
        }else{
            Debug.LogError("Invalid walk direction (not set to left or right)");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
