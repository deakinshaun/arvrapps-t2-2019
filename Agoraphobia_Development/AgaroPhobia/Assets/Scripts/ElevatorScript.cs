using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorScript : MonoBehaviour
{

    public float openDistance = 3f; //How far should door slide (change direction by entering either a positive or a negative value)
    public float openSpeed = 2.0f; //Increasing this value will make the door open faster
    public Transform elevator; //Door body Transform

    bool up = false;
    bool down = false;

    Vector3 defaultDoorPosition;

  
   
    
    
    void Start()
    {
        
        if (elevator)
        {
            defaultDoorPosition = elevator.localPosition;
        }
    }

    // Main function
    void Update()
    {
               
        if (up == true)
        {          
            goingUpAnimation();
        }

        if (down == true)
        {
            goingDownAnimation();
        }
    }

       public void goingUp()  //replace with button overrides
    {
       
      
           up = true;
           down = false;
            
        
    }



  public  void goingDown() //replace with button overrides
    {
       
            up = false;
            down = true;
        
    }

    // Activate the Main function when Player enter the trigger area
    void goingUpAnimation()
    {
        elevator.localPosition = new Vector3(elevator.localPosition.x, Mathf.Lerp(elevator.localPosition.y, defaultDoorPosition.y + (up ? openDistance : 0), Time.deltaTime * openSpeed), elevator.localPosition.z);
    }


    void goingDownAnimation()
    {
        elevator.localPosition = new Vector3(elevator.localPosition.x, Mathf.Lerp(elevator.localPosition.y, defaultDoorPosition.y + (up ? openDistance : 0), Time.deltaTime * openSpeed), elevator.localPosition.z);
    }

}