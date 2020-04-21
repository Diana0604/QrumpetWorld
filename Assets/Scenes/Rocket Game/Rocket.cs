using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{
    public delegate void Callback();
    [SerializeField] float rcsThrust = 100f;
    [SerializeField] float mainThrust = 100f;
    [SerializeField] AudioClip mainEngineClip;
    [SerializeField] AudioClip deathClip;
    [SerializeField] AudioClip finishLevelClip;
    [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] ParticleSystem deathParticles;
    [SerializeField] ParticleSystem finishLevelParticles;
    
    Rigidbody rigidBody;
    AudioSource audioSource;
    Scene scene;

    enum State {Alive, Dying, Transcending};
    State state = State.Alive;
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        scene = SceneManager.GetActiveScene();
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
    
    private void Thrust()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            rigidBody.AddRelativeForce(Vector3.up * mainThrust);
            if(!audioSource.isPlaying) audioSource.PlayOneShot(mainEngineClip);
            mainEngineParticles.Play();
        } 
        else {
            mainEngineParticles.Stop();
            audioSource.Stop();
        }
    }
    //check key pressed and move accordingly
    private void ProcessInput()
    {
        if(state != State.Alive) return;
        Thrust();
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
    void playSoundAndRunMethod(AudioClip clip, Callback method)
    {
        audioSource.Stop();
        audioSource.PlayOneShot(clip);
        StartCoroutine(runAfterSound(method));
    }
    IEnumerator runAfterSound(Callback method)
    {
        while (audioSource.isPlaying)
        {
            yield return null;
        }
        method();
    }
    private void OnCollisionEnter(Collision collision) 
    {
        if(state != State.Alive) return;
        switch (collision.gameObject.tag)
        {
            case "Friendly" : 
                break;
            case "Finish" : 
                state = State.Transcending;
                playSoundAndRunMethod(finishLevelClip, LoadNextScene);
                break;
            default:
            {
                state = State.Dying;
                playSoundAndRunMethod(deathClip, LoadCurrentScene);
            }
                break;
        }
    }
}
