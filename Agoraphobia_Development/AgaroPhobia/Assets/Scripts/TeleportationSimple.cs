using UnityEngine;
using UnityEngine.EventSystems;

public class TeleportationSimple : MonoBehaviour, IPointerClickHandler
{

    public GameObject player;
    public ButlerScript butlerReady;

    public GameObject elevatorDoor;
    public GameObject LvlDoor;


    public void OnPointerClick(PointerEventData eventData)
    {
        Vector3 worldPos = eventData.pointerCurrentRaycast.worldPosition;
        Vector3 playerPos = new Vector3(worldPos.x, player.transform.position.y, worldPos.z);
        player.transform.position = playerPos;

        elevatorDoor.GetComponent<DoorScript>().closeDoor();
        LvlDoor.GetComponent<DoorScript>().closeDoor();
        butlerReady.GetComponent<ButlerScript>().playerInPosition = false;
    }
}