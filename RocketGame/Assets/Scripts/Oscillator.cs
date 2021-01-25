using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    [SerializeField] Vector3 movementVector;
    [SerializeField] float movementFactorSpeed = 0.1f;

    float movementFactor;
    bool goingForward = true;

    Vector3 startingPosition;
    
    void Start()
    {
        startingPosition = transform.position;
        movementFactor = 0;
        
    }

    
    void Update()
    {
        

        if (goingForward)
        {
            movementFactor += movementFactorSpeed * Time.deltaTime;
            Debug.Log(movementFactor);
            if(movementFactor > 1)
            {
                goingForward = false;
            }
        }
        else if (!goingForward)
        {
            movementFactor -= movementFactorSpeed * Time.deltaTime;
            Debug.Log(movementFactor);
            if(movementFactor < 0)
            {
                goingForward = true;
            }
        }

        Vector3 newPosition = startingPosition + movementVector * movementFactor;
        transform.position = newPosition;
    }

    
}
