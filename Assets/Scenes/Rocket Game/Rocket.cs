using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    [SerializeField] float rcsThrust = 100f;
    [SerializeField] float mainThrust = 100f;
    Rigidbody rigidBody;
    AudioSource audioData;
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        audioData = GetComponent<AudioSource>();
        audioData.Play(0);
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInput();
    }
    private void Rotate()
    {
        float rotationThisFrame = rcsThrust * Time.deltaTime;
        rigidBody.freezeRotation = true;
        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(Vector3.forward * rotationThisFrame);
        }
        else if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(-Vector3.forward * rotationThisFrame);
        }
        rigidBody.freezeRotation = false ;
    }
    private void ThrustSound()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            rigidBody.AddRelativeForce(Vector3.up * mainThrust);
            audioData.UnPause();
        } else {
            audioData.Pause();
        }
    }
    //check key pressed and move accordingly
    private void ProcessInput()
    {
        ThrustSound();
        Rotate();
    }
    //on collision
    private void OnCollisionEnter(Collision collision) 
    {
        //print(collision.gameObject.tag);
        switch (collision.gameObject.tag)
        {
            case "Friendly" : 
                //print("Friendly Collider");
                break;
            case "Fuel" : 
                break;
            default:
                //print("Dead");
                break;
        }
    }
}
