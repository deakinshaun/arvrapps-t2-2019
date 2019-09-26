using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{

    public GameObject elevatorDoor;
    public GameObject lvl1Door;
    public GameObject lvl2Door;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void onTriggerEnter(Collider other)
    {

        if (other.tag == "Elevator")
        {
            elevatorDoor.GetComponent<DoorScript>().openDoor();
            lvl1Door.GetComponent<DoorScript>().openDoor();
            lvl2Door.GetComponent<DoorScript>().openDoor();

        }


    }






}
