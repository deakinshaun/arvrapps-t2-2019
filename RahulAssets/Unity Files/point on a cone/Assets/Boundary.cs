using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundary : MonoBehaviour
{
    public GameObject ball;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       

    }
    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Capsule") {

            Destroy(ball);

            Debug.Log("collided");
        }
       
       
    }
}
