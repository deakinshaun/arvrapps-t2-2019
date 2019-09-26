//Make an empty GameObject and call it "Door"
//Drag and drop your Door model into Scene and rename it to "Body"
//Move the "Body" Object inside "Door"
//Add a Collider (preferably SphereCollider) to "Door" Object and make it much bigger then the "Body" model, mark it as Trigger
//Assign this script to a "Door" Object (the one with a Trigger Collider)
//Make sure the main Character is tagged "Player"
//Upon walking into trigger area the door should Open automatically

using UnityEngine;

public class DoorScript : MonoBehaviour
{
    // Sliding door
    public enum OpenDirection { x, y, z }
    public OpenDirection direction = OpenDirection.x;
    public float openDistance = 3f; //How far should door slide (change direction by entering either a positive or a negative value)
    public float openSpeed = 2.0f; //Increasing this value will make the door open faster
    public Transform doorBody; //Door body Transform

    bool open = false;
    bool close = false;

    Vector3 defaultDoorPosition;

    void Start()
    {
        if (doorBody)
        {
            // defaultDoorPosition = doorBody.localPosition;
            defaultDoorPosition = doorBody.position;
        }
    }

    // Main function
    void Update()
    {

      //  openDoor();
        //    closeDoor();

        if (open == true)
        {
            openDoorAnimation();
        }

        if (close == true)
        {
            closeDoorAnimation();
        }
    }




   public void openDoor()  //replace with button overrides
    {
        
            open = true;
            close = false;

        //
        
    }

  public void closeDoor() //replace with button overrides
    {
        
            open = false;
            close = true;
        
    }

    // Activate the Main function when Player enter the trigger area
    void openDoorAnimation()
    {
        //  doorBody.localPosition = new Vector3(Mathf.Lerp(doorBody.localPosition.x, defaultDoorPosition.x + (open ? openDistance : 0), Time.deltaTime * openSpeed), doorBody.localPosition.y, doorBody.localPosition.z);
        //doorBody.position = new Vector3(Mathf.Lerp(doorBody.position.x, defaultDoorPosition.x + (open ? openDistance : 0), Time.deltaTime * openSpeed), doorBody.position.y, doorBody.position.z);
        doorBody.position = new Vector3(doorBody.position.x, doorBody.position.y, Mathf.Lerp(doorBody.position.z, defaultDoorPosition.z + (open ? openDistance : 0), Time.deltaTime * openSpeed));
        //doorBody.position = new Vector3(Time)
    }

    void closeDoorAnimation()
    {
        // doorBody.localPosition = new Vector3(Mathf.Lerp(doorBody.localPosition.x, defaultDoorPosition.x - (open ? openDistance : 0), Time.deltaTime * openSpeed), doorBody.localPosition.y, doorBody.localPosition.z);
        // doorBody.position = new Vector3(Mathf.Lerp(doorBody.position.x, defaultDoorPosition.x - (open ? openDistance : 0), Time.deltaTime * openSpeed), doorBody.position.y, doorBody.position.z);
        doorBody.position = new Vector3(doorBody.position.x, doorBody.position.y, Mathf.Lerp(doorBody.position.z, (open ? openDistance : 0) - defaultDoorPosition.z, Time.deltaTime * openSpeed));
    }

}