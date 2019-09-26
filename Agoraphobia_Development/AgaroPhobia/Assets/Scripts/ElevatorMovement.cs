using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ElevatorMovement : MonoBehaviour
{
    public GameObject elevator;

     
   
    private void Start()
    {

    }
    

    public void goUp()
    {

     elevator.transform.position = new Vector3(0, 3, 3.7f);
                           
    }

    public void goDown()
    {
        
        elevator.transform.position = new Vector3(0, 0, 3.7f);
        

    }
}

