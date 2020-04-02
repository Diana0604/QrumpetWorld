
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObstacles : MonoBehaviour
{
    [SerializeField] bool startingUpwards = true;
    float direction = 1f;
    Vector3 initialPos;
    Vector3 lastChangePos;
    [SerializeField] float speed = 5f;
    [SerializeField] float changeDirAfter = 3.0f;
    Vector3 userDirection = Vector3.up;
    // Start is called before the first frame update
    void Start()
    {
        initialPos = transform.position;
        lastChangePos = transform.position;
        if(!startingUpwards) direction = direction * -1f;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 currentPos = transform.position;
        if((currentPos - initialPos).magnitude > changeDirAfter && (currentPos - lastChangePos).magnitude > 0.5f)
        {
            direction = direction*-1f;
            lastChangePos = currentPos;
        }
        transform.Translate(userDirection * direction * speed * Time.deltaTime); 
    }
}
