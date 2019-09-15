using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GoogleARCore;

public class ObjectController : MonoBehaviour
{
    public Camera ARCam;

    public GameObject[] gameOjbectPrefabs = new GameObject[2];

    public GameObject selectObjectButton;

    private int prefabIndex; //to decide what object prefab will be created

    //public Text camPosText;

    //public Text objectPosText;

    //public Text selectButtonText;

    private GameObject selectedObject;

    private Vector3 distanceFromSurface;

    private RaycastHit hit; //to detect if the red dot is pointing at the furniture

    // Start is called before the first frame update
    void Start()
    {
        distanceFromSurface = new Vector3(0, 0.2f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (selectedObject == null)
        {
            Ray ray = ARCam.ScreenPointToRay(new Vector2(Screen.width * 0.5f, Screen.height * 0.5f));
            if (Physics.Raycast(ray, out hit, 30.0f))
            {
                if (hit.transform.gameObject.CompareTag("furniture"))
                {
                    selectObjectButton.SetActive(true);
                }
                else selectObjectButton.SetActive(false);
            }
            else selectObjectButton.SetActive(false);
        }
        else 
        {
            selectObjectButton.SetActive(true);
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
        //objectPosText.text = "Object pos: " + hit.Pose.position;
        Vector3 objectPos = hit.Pose.position + distanceFromSurface;
        GameObject gameObject = Instantiate(gameOjbectPrefabs[prefabIndex], objectPos, Quaternion.identity);
				//GameObject gameObject = Instantiate(gameOjbectPrefab, new Vector3(0,0,2), Quaternion.identity);
    }

    public void onControlObject()
    {
        if (!selectedObject)
            SelectObject();
        else ReleaseObject();

    }
    void SelectObject()
    {
        selectedObject = hit.transform.gameObject;
        Debug.Log("selected object name: " + selectedObject.name);
    }
    void ReleaseObject()
    {
        selectedObject = null;
        //selectButtonText.text = "Select";
    }

    public void SetObjectPrefabIndex(int index)
    {
        prefabIndex = index;
    }
}
