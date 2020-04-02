using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] float forwardForce = 10f;
    [SerializeField] float rotation = 10f;
    Rigidbody rigidBody;
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();

    }

    private void ManageVelocity()
    {
        if(Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            rigidBody.AddRelativeForce(Vector3.forward * forwardForce);
            //rigidBody.position = rigidBody.position + Vector3.forward* 0.5f;
            //rigidBody.velocity = rigidBody.velocity + Vector3.forward*0.1f;
        } else{
            if(Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) {
                rigidBody.AddRelativeForce(-Vector3.forward * forwardForce);
                //rigidBody.position = rigidBody.position - Vector3.forward;
                //rigidBody.velocity = rigidBody.velocity - Vector3.forward*0.1f;
            } else rigidBody.velocity = rigidBody.velocity * 0f;
        }
    }

    private void ManageRotation()
    {
        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(Vector3.down * rotation * Time.deltaTime);
        }
        else if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(Vector3.up * rotation * Time.deltaTime);
        }
    }
    // Update is called once per frame
    void Update()
    {
        ManageVelocity();
        ManageRotation();
        
    }
}