using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Threading;
using Unity.VisualScripting;

using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections), typeof(Damageable))]
public class GoblinEnemy : MonoBehaviour
{
    public float walkAcceleration = 9f;
    public float maxSpeed = 4f;
    public float walkStopRate = 0.05f;
    public DetectionZone attackZone;
    public DetectionZone cliffDetectionZone;

    GameObject player;
    Rigidbody2D rb;
    TouchingDirections touchingDirections;
    Animator animator;
    Damageable damageable;

    public enum WalkableDirection { Right, Left }

    private WalkableDirection _walkDirection;
    private UnityEngine.Vector2 walkDirectionVector;


    //aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa    
    private UnityEngine.Vector2 _walkDirectionEnemy;
    public UnityEngine.Vector2 direction
    {

        get { return _walkDirectionEnemy; }
        set { _walkDirectionEnemy = value; }
    }

    //aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa
    public WalkableDirection WalkDirection
    {
        get { return _walkDirection; }
        set
        {
            if (_walkDirection != value)
            {
                // Direction flipped
                gameObject.transform.localScale = new UnityEngine.Vector2(gameObject.transform.localScale.x * -1, gameObject.transform.localScale.y);

                if (value == WalkableDirection.Right)
                {
                    walkDirectionVector = UnityEngine.Vector2.right;
                }
                else if (value == WalkableDirection.Left)
                {
                    walkDirectionVector = UnityEngine.Vector2.left;
                }
            }

            _walkDirection = value;
        }
    }

    public bool _hasTarget = false;
    public UnityEngine.Vector2 velocityOnHit;

    public bool HasTarget
    {
        get { return _hasTarget; }
        private set
        {
            _hasTarget = value;
            animator.SetBool(AnimationStrings.hasTarget, value);
        }
    }

    public bool CanMove
    {
        get
        {
            return animator.GetBool(AnimationStrings.canMove);
        }
    }

    public float AttackCooldown
    {
        get
        {
            return animator.GetFloat(AnimationStrings.attackCooldown);
        }
        private set
        {
            animator.SetFloat(AnimationStrings.attackCooldown, Mathf.Max(value, 0));
        }
    }

    public float EnemyDistance
    {
        get
        {
            return animator.GetFloat(AnimationStrings.enemyDistance);
        }
        private set
        {
            animator.SetFloat(AnimationStrings.enemyDistance, value);
        }
    }




    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        touchingDirections = GetComponent<TouchingDirections>();
        animator = GetComponent<Animator>();
        damageable = GetComponent<Damageable>();
        player = GameObject.Find("Player");
        
        // walkDirectionVector = transform.position.x < player.transform.position.x ? UnityEngine.Vector2.right : UnityEngine.Vector2.left;

    }
    public bool isOnRightSideEnemy = true;


    // Update is called once per frame
    void Update()
    {

        if ((transform.position - player.transform.position).magnitude > 0)
        {
           WalkDirection = WalkableDirection.Right;
}
        else
        {
            WalkDirection = WalkableDirection.Left;
        }
        if (HasTarget)
        {
            if (direction.x >= 0)
            {
                WalkDirection = WalkableDirection.Right;
            }
            else if (direction.x < 0)
            {
                WalkDirection = WalkableDirection.Left;
            }

        }
    }
    private void FixedUpdate()
    {
        EnemyDistance = UnityEngine.Vector2.Distance(transform.position, player.transform.position);
        direction = player.transform.position - transform.position;

     
        bool something = direction.x > 0;

        HasTarget = attackZone.detectedColliders.Count > 0;

        if (AttackCooldown > 0 && EnemyDistance < 1.5f)
        {
            AttackCooldown -= Time.deltaTime;
        }
        if (touchingDirections.isOnWall)
        {
            FlipDirection();
        }
        if (touchingDirections.isGrounded && touchingDirections.isOnWall)
        {
            FlipDirection();
        }


        if (CanMove && touchingDirections.isGrounded && HasTarget)
        {
            // Accelerate towards max Speed
            transform.position = UnityEngine.Vector2.MoveTowards(transform.position, player.transform.position, 0.05f * Time.deltaTime);
            if (direction.x > 0)
            {
                /*
                if (WalkDirection != WalkableDirection.Right)
                    WalkDirection = WalkableDirection.Right;*/
                walkDirectionVector = UnityEngine.Vector2.right;
                rb.velocity = new UnityEngine.Vector2(walkDirectionVector.x * maxSpeed, rb.velocity.y);

            }
            else if (direction.x < 0)
            {
                /*
                if (WalkDirection != WalkableDirection.Left)
                    WalkDirection = WalkableDirection.Left;   */
                walkDirectionVector = UnityEngine.Vector2.left;

                rb.velocity = new UnityEngine.Vector2(walkDirectionVector.x * maxSpeed, rb.velocity.y);

            }

        }
        if (rb.velocity.x == 0)
        {
            animator.SetBool(AnimationStrings.lockVelocity, false);
        }
    }
    private void FlipDirection()
    {
        if (WalkDirection == WalkableDirection.Right)
        {
            WalkDirection = WalkableDirection.Left;
        }
        else if (WalkDirection == WalkableDirection.Left)
        {
            WalkDirection = WalkableDirection.Right;
        }
        else
        {
            Debug.LogError("Current walkable direction is not set to legal values of right or left");
        }
    }

    public void OnHit(int damage, UnityEngine.Vector2 knockback)
    {
        velocityOnHit = rb.velocity;
        rb.velocity = new UnityEngine.Vector2(knockback.x, rb.velocity.y + knockback.y);


    }

    public void OnCliffDetected()
    {
        if (touchingDirections.isGrounded)
        {
            FlipDirection();
        }
    }

    private float getSpeed()
    {

        rb.velocity = new UnityEngine.Vector2(
                    Mathf.Clamp(rb.velocity.x + (walkAcceleration * walkDirectionVector.x * Time.fixedDeltaTime), -maxSpeed, maxSpeed),
                    rb.velocity.y);
        return rb.velocity.x;
    }


    void direcitonHasChanged()
    {

    }
}
