using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lvl2Camera : MonoBehaviour
{
    [SerializeField] float positionBuffer = 5f;

    RocketShip rocket;
    
    void Start()
    {
        rocket = FindObjectOfType<RocketShip>();
    }

    
    void Update()
    {
        adjustPosition();
    }

    private void adjustPosition()
    {
        float rocketYPos = rocket.transform.position.y;
        float cameraYPos = rocketYPos - positionBuffer;

        transform.position = new Vector3(transform.position.x, cameraYPos, transform.position.z);
    }
}
