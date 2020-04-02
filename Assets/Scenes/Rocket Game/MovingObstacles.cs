
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObstacles : MonoBehaviour
{
    float dir = 1f;
    Vector3 initialPos;
    [SerializeField] float speed = 5f;
    [SerializeField] float changeDirAfter = 3.0f;
    Vector3 userDirection = Vector3.up;
    // Start is called before the first frame update
    void Start()
    {
        initialPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        print(Mathf.Floor(Time.time));
        Vector3 currentPos = transform.position;
        if((currentPos - initialPos).magnitude > changeDirAfter) 
        //if(Mathf.Floor(Time.time) != timeLastChange && Mathf.Floor(Time.time)%changeDirAfter == 0f)
        {
            print("'changing dir'");
            //timeLastChange = Mathf.Floor(Time.time);
            dir = dir*-1f;
        }
        transform.Translate(userDirection * dir * speed * Time.deltaTime); 
    }
}
