using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpriteScroller : MonoBehaviour
{
    [SerializeField] Vector2 moveSpeed;
    Vector2 offset;
    Vector2 playerInput;
    
    Material material;
    PlayerMovement playerMovement;
    
    void Awake()
    {
        material = GetComponent<SpriteRenderer>().material;
        playerMovement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if (playerInput == null) {return;}
        
        offset = playerInput * moveSpeed * Time.deltaTime;
        material.mainTextureOffset += offset;
        Debug.Log(material.mainTextureOffset);
    }
    
    public void ProcessPlayerInput(Vector2 input)
    {
        playerInput = input;
    }
}
