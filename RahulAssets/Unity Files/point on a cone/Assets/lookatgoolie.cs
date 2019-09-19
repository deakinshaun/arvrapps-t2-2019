using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lookatgoolie : MonoBehaviour
{
    public Transform target;
    public bool reset;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (reset == true)
        {
            Debug.Log("reset");
            transform.position = new Vector3(-3.71f, 0.0f, 1.19f);
            transform.Rotate(0, 0, 0);
            reset = false;

        }
        target = GameObject.FindWithTag("Sphere").transform;

        if (target.tag == "Sphere")
        {
            transform.LookAt(target, Vector3.up);
            transform.position = Vector3.Lerp(transform.position, target.position, 0.07f);

        }
       
        if (transform.position.x < -3.71f)
        {

            transform.position = new Vector3(-3.70f, transform.position.y, transform.position.z);
        }
        if (transform.position.x > -1.81f)
        {

            transform.position = new Vector3(-1.18f, transform.position.y, transform.position.z);
        }

     

        
    }

    public void ballgone() {
      reset = true;
    }

    
 
}
