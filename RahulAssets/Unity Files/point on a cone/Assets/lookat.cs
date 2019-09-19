using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lookat : MonoBehaviour
{
    public Transform target;
    
   
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("b")) {
            target = GameObject.FindWithTag("Sphere").transform;
            transform.LookAt(target, Vector3.up);
            transform.position = Vector3.Lerp(transform.position, target.position, 0.01f);
            string name = target.tag;
            Debug.Log(name);
        }
        else if (Input.GetKey("n")) {
            target = GameObject.FindWithTag("Cube").transform;
            transform.LookAt(target, Vector3.up);
            transform.position = Vector3.Lerp(transform.position, target.position, 0.01f);

        }
        else if (Input.GetKey("m")) {
            target = GameObject.FindWithTag("Capsule").transform;
            transform.LookAt(target, Vector3.up);
            transform.position = Vector3.Lerp(transform.position, target.position, 0.01f);
        }
           
       
    }
}
