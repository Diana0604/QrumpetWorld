using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
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
            rigidBody.AddRelativeForce(Vector3.forward);
            //rigidBody.position = rigidBody.position + Vector3.forward* 0.5f;
            //rigidBody.velocity = rigidBody.velocity + Vector3.forward*0.1f;
        } else{
            if(Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) {
                rigidBody.AddRelativeForce(-Vector3.forward);
                //rigidBody.position = rigidBody.position - Vector3.forward;
                //rigidBody.velocity = rigidBody.velocity - Vector3.forward*0.1f;
            } else rigidBody.velocity = rigidBody.velocity * 0f;
        }
    }

    private void ManageRotation()
    {
        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(Vector3.down);
        }
        else if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(Vector3.up);
        }
    }
    // Update is called once per frame
    void Update()
    {
        ManageVelocity();
        ManageRotation();
        
    }
}