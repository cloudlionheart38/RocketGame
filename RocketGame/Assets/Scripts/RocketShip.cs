using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketShip : MonoBehaviour
{
    
    [SerializeField] float rocketThrust = 10f;
    [SerializeField] float rotationSpeed = 2f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] AudioClip crash;
    [SerializeField] AudioClip success;
    [SerializeField] ParticleSystem thrustEffect;
    [SerializeField] ParticleSystem crashEffect;
   

    Rigidbody rocketrb;
    AudioSource rocketSound;
   

    LevelManager levelmanager;

    enum State { Alive,Dying,Respawning}
    State state = State.Alive;

    void Start()
    {
       
        rocketrb = GetComponent<Rigidbody>();
        rocketSound = GetComponent<AudioSource>();
        levelmanager = FindObjectOfType<LevelManager>();
    }

    
    void Update()
    {
        bool canControl = (state == State.Alive);

        if (canControl)
        {
            Thrust();
            Rotate();
        }
    }



    private void Rotate()
    {
        bool rightButtonPressed = Input.GetKey(KeyCode.RightArrow);
        bool leftButtonPressed = Input.GetKey(KeyCode.LeftArrow);

        rocketrb.freezeRotation = true;

        if (leftButtonPressed && !rightButtonPressed)
        {
            transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
        }

        if (rightButtonPressed && !leftButtonPressed)
        {
            transform.Rotate(Vector3.back * rotationSpeed * Time.deltaTime);
        }

        rocketrb.freezeRotation = false;
    }

    private void Thrust()
    {
        bool spacePressed = Input.GetKey(KeyCode.Space);

        if (spacePressed)
        {
            if (!thrustEffect.isPlaying)
            {
                thrustEffect.Play();
            }
            rocketrb.AddRelativeForce(Vector3.up * rocketThrust * Time.deltaTime);
            if (!rocketSound.isPlaying)
            {
                rocketSound.PlayOneShot(mainEngine);
            }

        }
        else
        {
            thrustEffect.Stop();
            rocketSound.Stop();
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (state != State.Alive)
        {
            return;
        }

        if (collision.gameObject.tag == "Friendly")
        {
            
        }
        else if(collision.gameObject.tag == "Finish")
        {
            rocketSound.Stop();
            rocketSound.PlayOneShot(success);
            state = State.Respawning;
            Invoke("loadNextScene",1f);
        }
        else
        {
            crashEffect.Play();
            rocketSound.Stop();
            rocketSound.PlayOneShot(crash);
            state = State.Dying;
            Invoke("loadCurrentScene",1f);
        }
    }

    private void loadCurrentScene()
    {
        levelmanager.resetLevel();
    }

    private void loadNextScene()
    {
        levelmanager.loadNextScene();
    }
}
