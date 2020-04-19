using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{
    [SerializeField] float rcsThrust = 100f;
    [SerializeField] float mainThrust = 100f;
    Rigidbody rigidBody;
    AudioSource audioData;
    Scene scene;

    enum State {Alive, Dying, Transcending};
    State state = State.Alive;
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        audioData = GetComponent<AudioSource>();
        scene = SceneManager.GetActiveScene();
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
        if(state != State.Alive) return;
        ThrustSound();
        Rotate();
    }
    
    void LoadNextScene()
    {
        SceneManager.LoadScene(scene.buildIndex + 1);
    }

    void LoadCurrentScene()
    {
        SceneManager.LoadScene(scene.buildIndex);
    }
    //on collision
    private void OnCollisionEnter(Collision collision) 
    {
        if(state != State.Alive) return;
        switch (collision.gameObject.tag)
        {
            case "Friendly" : 
                break;
            case "Finish" : 
                state = State.Transcending;
                if(scene.buildIndex < SceneManager.sceneCountInBuildSettings - 1)
                {
                    Invoke("LoadNextScene", 1f) ;
                }
                break;
            default:
            {
                state = State.Dying;
                Invoke("LoadCurrentScene", 1f);
            }
                break;
        }
    }
}
