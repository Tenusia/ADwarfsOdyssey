using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
      
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] float destroyObjectSpeed = 3f;
    [SerializeField] int pointValue = 100;
    [SerializeField] float hitPoints = 3f;
    
        
    Rigidbody2D myRigidbody;
    CapsuleCollider2D myBodyCollider;
    BoxCollider2D myScopeCollider; 
    Animator myAnimator;
    AudioPlayer audioPlayer;
    bool isAlive;
    public bool bossIsAlive;

    void Awake()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
        myScopeCollider = GetComponent<BoxCollider2D>();
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }
    
    void Start()
    {
        isAlive = true;
    }

    void Update()
    {
        myRigidbody.velocity = new Vector2 (moveSpeed, 0f);
    }

    void OnTriggerExit2D(Collider2D other) 
    {
        moveSpeed = -moveSpeed;
        FlipEnemyFacing();
    }

    void FlipEnemyFacing()
    {
        transform.localScale = new Vector2 (-(Mathf.Sign(myRigidbody.velocity.x)), 1f);
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == "Weapon")
        {
            if(!isAlive){return;}
            if(hitPoints > 1)
            {
                hitPoints -= 1;
            }
            else
            {
                if(bossIsAlive)
                {
                    bossIsAlive=false;
                }
                myAnimator.SetTrigger("gotHit");
                audioPlayer.PlayEnemyDeathClip();
                FindObjectOfType<GameSession>().ProcessPickup(pointValue);
                myRigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
                myBodyCollider.enabled = false;
                myScopeCollider.enabled = false;
                isAlive = false;
                Destroy(gameObject,destroyObjectSpeed);
            }
        }
    }
}
