using UnityEngine;
using UnityEngine.EventSystems;

public class TeleportationElevator : MonoBehaviour, IPointerClickHandler
{

    public GameObject player;
    public GameObject elevatorDoor;
    public GameObject Lvl1Door;
    public GameObject Lvl2Door;
    public ButlerScript butlerReady;
    // public GameObject lvl1FloorDoor;

    void Awake()
    {
        
       
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        Vector3 worldPos = eventData.pointerCurrentRaycast.worldPosition;
        Vector3 playerPos = new Vector3(worldPos.x, player.transform.position.y, worldPos.z);
        player.transform.position = playerPos;
        elevatorDoor.GetComponent<DoorScript>().closeDoor();
        Lvl1Door.GetComponent<DoorScript>().closeDoor();
        Lvl2Door.GetComponent<DoorScript>().closeDoor();
        butlerReady.GetComponent<ButlerScript>().playerInPosition = true;
    }
    
}


