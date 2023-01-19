using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    Vector2 moveInput;
    Rigidbody2D myRigidbody;
    Animator myAnimator;
    CapsuleCollider2D myBodyCollider;
    BoxCollider2D myFeetCollider;
    AudioPlayer audioPlayer;
    SpriteScroller spriteScroller;
    

    [SerializeField] float runSpeed =10f;
    [SerializeField] float jumpSpeed =5f;
    [SerializeField] float climbSpeed =10f;
    [SerializeField] Vector2 deathKick = new Vector2 (10f,10f);
    [SerializeField] float waitTimeFreezeMovement = 1.5f;
    [SerializeField] float waitTimeReload = 1.8f;
    [SerializeField] ParticleSystem deathTrailEffect;
    [SerializeField] GameObject weapon;
    [SerializeField] Transform hand;
    
    float startGravityScale;

    bool isAlive = true;
    
    void Awake() 
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
        myFeetCollider = GetComponent<BoxCollider2D>();
        audioPlayer = FindObjectOfType<AudioPlayer>();
        spriteScroller = FindObjectOfType<SpriteScroller>();
    }

    void Start()
    {
        startGravityScale = myRigidbody.gravityScale;
        deathTrailEffect.Stop();
    }

    void Update()
    {
        if (!isAlive) {return;}
        Run();
        FlipSprite();
        ClimbLadder();
        Die();
    }

    void OnMove(InputValue value)
    {
        if (!isAlive) {return;}
        moveInput = value.Get<Vector2>();
        spriteScroller.ProcessPlayerInput(moveInput);
    }

    public Vector2 GetMoveInput()
    {
        return moveInput;
    }

    void OnJump(InputValue value)
    {
        if (!isAlive) {return;}
        if (!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))) {return;}
        // OPTIONAL. Add option to jump from ladder.
        // OPTIONAL. Add jump SFX
        
        if(value.isPressed)
        {
            myRigidbody.velocity += new Vector2 (0f, jumpSpeed);
        }
    }

    void OnFire(InputValue value)
    {
        if (!isAlive) {return;}
        
        if(value.isPressed)
        {
        Instantiate(weapon, hand.position, transform.rotation);
        }
    }

    void Run()
    {
        Vector2 playerVelocity = new Vector2 (moveInput.x * runSpeed, myRigidbody.velocity.y);
        myRigidbody.velocity = playerVelocity;

        bool playerIsRunning = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;   
        myAnimator.SetBool("isRunning", playerIsRunning);        
            //FIX Footstep SFX
            //audioPlayer.PlayPlayerWalkingClip();
    }

    void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;

        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2 (Mathf.Sign(myRigidbody.velocity.x), 1f);
        }
    }

    void ClimbLadder()
    {
        if (!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Climbing"))) 
        {
            myRigidbody.gravityScale = startGravityScale;
            myAnimator.SetBool("isClimbing", false);
            return;
        }
        
        Vector2 climbVelocity = new Vector2 (myRigidbody.velocity.x, moveInput.y * climbSpeed);
        myRigidbody.velocity = climbVelocity;
        myRigidbody.gravityScale = 0f;

        bool playerIsClimbing = Mathf.Abs(myRigidbody.velocity.y) > Mathf.Epsilon;   
        myAnimator.SetBool("isClimbing", playerIsClimbing);
    }

    void Die() 
    {
        if(myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemies", "Hazards")))
        {
            isAlive=false;
            myAnimator.SetTrigger("Dying");
            audioPlayer.PlayPlayerDeathClip();
            myRigidbody.velocity = deathKick;
            deathTrailEffect.Play();
            StartCoroutine(FreezeReload());
        }
    }

    IEnumerator FreezeReload()
    {
        yield return new WaitForSecondsRealtime(waitTimeFreezeMovement);
        FreezeConstraints();
        yield return new WaitForSecondsRealtime(waitTimeReload);
        FindObjectOfType<GameSession>().ProcessPlayerDeath();
    }

    void FreezeConstraints()
    {
        myRigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
        deathTrailEffect.Stop();
        //Destroy (gameObject);
    }
    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }  
}
