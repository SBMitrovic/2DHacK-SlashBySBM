using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{
    public UnityEvent<int, Vector2> damageableHit;
    public UnityEvent damageableDeath;
    public UnityEvent<int, int> healthChanged;
    
    Animator animator;


    [SerializeField]
    private int _maxHealth = 100;

    public int maxHealth
    {
        get
        {
            return _maxHealth;
        }
        set
        {
            _maxHealth = value;
        }
    }

    [SerializeField]
    private int _health = 100;

    public int health
    {
        get
        {
            return _health;
        }
        set
        {
            _health = value;

            if (_health <= 0)
            {
                isAlive = false;
            }
        }
    }


    [SerializeField]
    private bool _isAlive = true;

    public bool isAlive
    {
        get
        {
            return _isAlive;
        }
        set
        {
            _isAlive = value;
            animator.SetBool(AnimationStrings.isAlive, _isAlive);
            Debug.Log("isalive set" + value);

        }
    }

    [SerializeField]
    private bool isInvincible = false;


        public bool LockVelocity
    {
        get
        {
            return animator.GetBool(AnimationStrings.lockVelocity);
        }
        set{
            animator.SetBool(AnimationStrings.lockVelocity, value);
        }
    }


    private float timeSinceHit = 0;
    public float invincibilityTime = 0.25f;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Update()
    {
        if (isInvincible)
        {
            if (timeSinceHit > invincibilityTime)
            {
                isInvincible = false;
                timeSinceHit = 0;
            }

            timeSinceHit += Time.deltaTime;
        }

    }

    public bool Hit(int damage, Vector2 knockback)
    {

        if (isAlive && !isInvincible)
        {
            health -= damage;
            isInvincible = true;
            LockVelocity = true;
            //Notify other subscribed components that the damagable was hit to handle the knockback nad such
            animator.SetTrigger(AnimationStrings.hitTrigger);
            damageableHit?.Invoke(damage, knockback);
            return true;
        }

        //Unable to hit
        return false;
    }

     
    public bool Heal(int healthRestore)
    {
        if(isAlive && health < maxHealth)
        {
            int maxHeal = Mathf.Max(maxHealth - health, 0);
            int actualHeal = Mathf.Min(maxHeal, healthRestore);
            health += actualHeal;
            CharacterEvents.characterHealed(gameObject, actualHeal);
            return true;
        }

        return false;
    }
}
