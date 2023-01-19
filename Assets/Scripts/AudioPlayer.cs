using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] AudioClip playerWalking;
    [SerializeField] [Range(0f,1f)] float playerWalkingVolume = 1f;
    [SerializeField] AudioClip playerDeath;
    [SerializeField] [Range(0f,1f)] float playerDeathVolume = 1f;
    [SerializeField] AudioClip playerJump;
    [SerializeField] [Range(0f,1f)] float playerJumpVolume = 1f;

    [Header("Axe")]
    [SerializeField] AudioClip axeThrowClip;
    [SerializeField] [Range(0f,1f)] float axeThrowVolume = 1f;
    [SerializeField] AudioClip axeHitClip;
    [SerializeField] [Range(0f,1f)] float axeHitVolume = 1f;

    [Header("Enemy")]
    [SerializeField] AudioClip enemyDeathClip;
    [SerializeField] [Range(0f,1f)] float enemyDeathVolume = 1f;

    [Header("Pickups")]
    [SerializeField] AudioClip coinPickupClip;
    [SerializeField] [Range(0f,1f)] float coinPickupVolume = 1f;
    [SerializeField] AudioClip bounceClip;
    [SerializeField] [Range(0f,1f)] float bounceVolume = 1f;

    public void PlayPlayerWalkingClip()
    {
        if(playerWalking != null)
        {
            AudioSource.PlayClipAtPoint(playerWalking, 
                                        Camera.main.transform.position, 
                                        playerWalkingVolume);
        }
    }

    public void PlayPlayerDeathClip()
    {
        if(playerDeath != null)
        {
            AudioSource.PlayClipAtPoint(playerDeath, 
                                        Camera.main.transform.position, 
                                        playerDeathVolume);
        }
    }

    public void PlayPlayerJumpClip()
    {
        if(playerJump != null)
        {
            AudioSource.PlayClipAtPoint(playerJump, 
                                        Camera.main.transform.position, 
                                        playerJumpVolume);
        }
    }

    public void PlayAxeThrowClip()
    {
        //STOP AUDIO WHEN HIT
        if(axeThrowClip != null)
        {
            AudioSource.PlayClipAtPoint(axeThrowClip, 
                                        Camera.main.transform.position, 
                                        axeThrowVolume);
        }
    }

    public void PlayAxeHitClip()
    {
        if(axeHitClip != null)
        {
            AudioSource.PlayClipAtPoint(axeHitClip, 
                                        Camera.main.transform.position, 
                                        axeHitVolume);
        }
    }

    public void PlayEnemyDeathClip()
    {
        if(enemyDeathClip != null)
        {
            AudioSource.PlayClipAtPoint(enemyDeathClip, 
                                        Camera.main.transform.position, 
                                        enemyDeathVolume);
        }
    }

    public void PlayCoinPickupClip()
    {
        if(coinPickupClip != null)
        {
            AudioSource.PlayClipAtPoint(coinPickupClip, 
                                        Camera.main.transform.position, 
                                        coinPickupVolume);
        }
    }

    public void PlayBounceClip()
    {
        if(bounceClip != null)
        {
            AudioSource.PlayClipAtPoint(bounceClip, 
                                        Camera.main.transform.position, 
                                        bounceVolume);
        }
    }
}
