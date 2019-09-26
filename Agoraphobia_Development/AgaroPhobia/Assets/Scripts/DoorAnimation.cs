using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class DoorAnimation : MonoBehaviour
{
    public GameObject door;



    private void Start()
    {

    }

    public void open()
    {


        //open door after a certain ammount of time


      door.transform.position = door.transform.position - new Vector3(-1.6f, 0, 0);
                           
    }
    
    public void close()
    {

        door.transform.position = door.transform.position + new Vector3(-1.6f, 0, 0);

        //door.transform.position = new Vector3(0, 1.136f, 2.629f);
        
    }
}
