using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    Vector2 startingPosition;
    [SerializeField] Vector2 movementVector;
    float movementFactor;
    [SerializeField] float period = 2f;

    void Start()
    {
        startingPosition = transform.position;
    }

    void Update()
    {
        StartMovement();
    }

    void StartMovement()
    {
        if (period <= Mathf.Epsilon) {return;} 
        float cycles = Time.time / period;                  // grows over time
        const float tau = Mathf.PI * 2;
        float rawSinWave = Mathf.Sin(cycles * tau);         // constant value of 6.28
        movementFactor = (rawSinWave + 1f) / 2f;            // going from -1 to 1

        Vector2 offset = movementVector * movementFactor;   // recalculated to go from 0 to 1
        transform.position = startingPosition + offset;
    }
}
