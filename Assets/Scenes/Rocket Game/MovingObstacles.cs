
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObstacles : MonoBehaviour
{
    Rigidbody rigidbody;
    float dir = 1f;
    float timeLastChange = 0f;
    [SerializeField] float speed = 5f;
    [SerializeField] float changeDirEach = 3.0f;
    Vector3 userDirection = Vector3.up;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        print(Mathf.Floor(Time.time));

        if(Mathf.Floor(Time.time) != timeLastChange && Mathf.Floor(Time.time)%changeDirEach == 0f)
        {
            print("'changing dir'");
            timeLastChange = Mathf.Floor(Time.time);
            dir = dir*-1f;
        }
        transform.Translate(userDirection * dir * speed * Time.deltaTime); 
    }
}
