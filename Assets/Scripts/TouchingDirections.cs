using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchingDirections : MonoBehaviour
{   

  

    public ContactFilter2D castFilter;
    CapsuleCollider2D touchingColider; 
    Animator animator;

    RaycastHit2D[] groundHits = new RaycastHit2D[5];
    RaycastHit2D[] wallHits = new RaycastHit2D[5];
    RaycastHit2D[] ceilingHits = new RaycastHit2D[5];

    public float groundDistance = 0.05f;
    public float wallDistance = 0.2f;
    public float ceilingDistance = 0.05f;
    //check if character is grounded
    [SerializeField]
    private bool _isGrounded = true;
    public bool isGrounded{
        get{    
            return _isGrounded;
        }
        set{
            _isGrounded = value;
            animator.SetBool(AnimationStrings.isGrounded, value);
        }
    }
    
    //check if is on wall to stop character from moving when touched the wall in air etc
    [SerializeField]

    private bool _isOnWall;
    public bool isOnWall{
        get{    
            return _isOnWall;
        }
        private set{
            _isOnWall = value;
            animator.SetBool(AnimationStrings.isOnWall, value);
        }
    }

    //check if is on wall to stop character from moving when touched the ceiling in air etc
    private bool _isOnCeiling = true;
    public bool isOnCeiling{
        get{    
            return _isOnCeiling;
        }
        set{
            _isOnCeiling = value;
            animator.SetBool(AnimationStrings.isOnCeiling, value);
        }
    }

    private Vector2 wallCheckDirection => gameObject.transform.localScale.x  > 0 ? Vector2.right : Vector2.left; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake(){
        touchingColider = GetComponent<CapsuleCollider2D>();
        animator = GetComponent<Animator>();
    }

    //Update is called once per frame
    void FixedUpdate(){
         isGrounded  = touchingColider.Cast(Vector2.down , castFilter, groundHits, groundDistance) > 0;
         isOnWall = touchingColider.Cast(wallCheckDirection, castFilter, wallHits, wallDistance) > 0;
         isOnCeiling = touchingColider.Cast(Vector2.up, castFilter, ceilingHits, ceilingDistance) > 0;

    }
}
