using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections), typeof(Damageable))]


public class PlayerController : MonoBehaviour
{
    [SerializeField]
    public float walkSpeed = 5f;
    [SerializeField]
    public float runSpeed = 9f;
    public float jumpSpeedImpulse = 10f;
    public float airWalkSpeed = 3f;
    Vector2 moveInput;
    TouchingDirections touchingDirections;
    Damageable damageable;

    public bool _isFacingRight = true;

    public bool isFacingRight
    {
        get { return _isFacingRight; }
        private set
        {
            if (_isFacingRight != value)
            {
                //Flip the local scale to make the player face the opposite direction 
                transform.localScale *= new Vector2(-1, 1);
            }
            _isFacingRight = value;
        }
    }

    //moving

    private bool _isMoving = false;

    public bool isMoving
    {
        get
        {
            return _isMoving;
        }
        private set
        {
            _isMoving = value;
            animator.SetBool(AnimationStrings.isMoving, value);
        }
    }


    //running
    [SerializeField]
    private bool _isRunning = false;

    public bool isRunning
    {
        get
        {
            return _isRunning;
        }
        private set
        {
            _isRunning = value;
            animator.SetBool(AnimationStrings.isRunning, value);
        }
    }

    //determine current speed
    public float currentSpeed
    {
        get
        {
            if (canMove)
            {
                if (isMoving && !touchingDirections.isOnWall)
                {
                    if (touchingDirections.isGrounded)
                    {
                        if (isRunning)
                        {
                            return runSpeed;
                        }
                        else
                        {
                            return walkSpeed;
                        }

                    }
                    else
                    {
                        return airWalkSpeed;
                    }
                }
                else
                {
                    //not moving
                    return 0;
                }
            }

            //Movement locked
            else
            {
                return 0;
            }

        }
    }

    public bool canMove
    {
        get
        {
            return animator.GetBool(AnimationStrings.canMove);
        }
    }

    public bool isAlive
    {
        get
        {
            return animator.GetBool(AnimationStrings.isAlive);
        }
    }

    
    
    Rigidbody2D rb;
    Animator animator;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        touchingDirections = GetComponent<TouchingDirections>();
        damageable = GetComponent<Damageable>();
    }

    private void FixedUpdate()
    {
        if (!damageable.LockVelocity)
            rb.velocity = new Vector2(moveInput.x * currentSpeed, rb.velocity.y);

        animator.SetFloat(AnimationStrings.yVelocity, rb.velocity.y);

    }


    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        if (isAlive)
        {
            isMoving = moveInput != Vector2.zero;
            SetFacingDirection(moveInput);
        }
        else
        {
            isMoving = false;
        }
    }

    //Facing direction
    public void SetFacingDirection(Vector2 moveInput)
    {
        if (moveInput.x > 0 && !isFacingRight)
        {
            //face right
            isFacingRight = true;
        }
        else if (moveInput.x < 0 && isFacingRight)
        {
            //face left
            isFacingRight = false;
        }
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Debug.Log("OnRun is called");
            isRunning = true;
        }
        else if (context.canceled)
        {
            isRunning = false;
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        Debug.Log("OnRun before if clause");
        if (context.started && touchingDirections.isGrounded && canMove)
        {
            Debug.Log("OnJump is called");
            animator.SetTrigger(AnimationStrings.jumpTrigger);
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeedImpulse);
        }
    }
    //attack method
    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            animator.SetTrigger(AnimationStrings.attackTrigger);
        }
    }

    public void OnHit(int damage, Vector2 knockback)
    {
      
        rb.velocity = new Vector2(knockback.x, rb.velocity.y + knockback.y);
    }
}
