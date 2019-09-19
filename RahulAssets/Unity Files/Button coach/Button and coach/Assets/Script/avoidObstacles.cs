using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class avoidObstacles : MonoBehaviour
{
    public Run move;
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
            move.moveleftright();
            Debug.Log("collided");
        }
}
