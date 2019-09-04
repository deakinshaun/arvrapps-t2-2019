using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GoogleARCore;

public class ObjectController : MonoBehaviour
{
    public Camera ARCam;

    public GameObject gameOjbectPrefab;

    public Text camPosText;

    public Text objectPosText;

    public Text selectButtonText;
    private GameObject selectedObject;

    private Vector3 distanceFromSurface;

    // Start is called before the first frame update
    void Start()
    {
        distanceFromSurface = new Vector3(0, 0.2f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        camPosText.text = "Cam pos: " + ARCam.transform.position;
        if(selectedObject != null)
        {
            
            TrackableHit hit;
            TrackableHitFlags raycastFilter = TrackableHitFlags.PlaneWithinPolygon |
                TrackableHitFlags.FeaturePointWithSurfaceNormal;
            Frame.Raycast(Screen.width * 0.5f, Screen.height * 0.5f, raycastFilter, out hit);
            selectedObject.transform.position = hit.Pose.position + distanceFromSurface;

            if (Input.touchCount == 1)
            {
                Touch touch = Input.GetTouch(0);
                float deltaX = touch.deltaPosition.x;
                selectedObject.transform.Rotate(0, deltaX * 0.2f , 0);
            }

        }
        
    }

    public void onCreateObject()
    {
        TrackableHit hit;
        TrackableHitFlags raycastFilter = TrackableHitFlags.PlaneWithinPolygon |
            TrackableHitFlags.FeaturePointWithSurfaceNormal;
        //cast a ray from center screen to the detected plane
        Frame.Raycast(Screen.width * 0.5f, Screen.height * 0.5f, raycastFilter, out hit);
        objectPosText.text = "Object pos: " + hit.Pose.position;
        Vector3 objectPos = hit.Pose.position + distanceFromSurface;
        GameObject gameObject = Instantiate(gameOjbectPrefab, objectPos, Quaternion.identity);
    }

    public void onControlObject()
    {
        if (!selectedObject)
            SelectObject();
        else ReleaseObject();

    }
    public void SelectObject()
    {
        RaycastHit hit;
        Ray ray = ARCam.ScreenPointToRay(new Vector2(Screen.width * 0.5f, Screen.height * 0.5f));
        if (Physics.Raycast(ray, out hit, 30.0f))
        {
            if (hit.transform.gameObject.CompareTag("furniture"))
            {
                selectedObject = hit.transform.gameObject;
                Debug.Log("selected object name: " + selectedObject.name);
            }
            
        }
        selectButtonText.text = "Release";
    }
    public void ReleaseObject()
    {
        selectedObject = null;
        selectButtonText.text = "Select";
    }
}
