using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorAutoDoor : MonoBehaviour
{

    public GameObject elevatorDoor;
    public GameObject lvl2Door;
    public GameObject lvl1Door;
    bool atLvl1 = true;
    bool atLvl2 = false;



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update() { 
    
        if ((transform.position.y > 2.98) && (!atLvl2))       //new Vector3(transform.position.x, 3, transform.position.z))
            {
                elevatorDoor.GetComponent<DoorScript>().openDoor();
                lvl2Door.GetComponent<DoorScript>().openDoor();
            atLvl2 = true;
            atLvl1 = false;

        }

        if ((transform.position.y < 0.5) && (!atLvl1))       //new Vector3(transform.position.x, 3, transform.position.z))
        {
            elevatorDoor.GetComponent<DoorScript>().openDoor();
            lvl1Door.GetComponent<DoorScript>().openDoor();
            atLvl1 = true;
            atLvl2 = false;
        }


    }

}





