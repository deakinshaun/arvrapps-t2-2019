using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;
using GoogleARCore.Examples.Common;

#if UNITY_EDITOR
using input = GoogleARCore.InstantPreviewInput;
#endif

public class ARController : MonoBehaviour
{
    private List<DetectedPlane> m_trackedPlanes = new List<DetectedPlane>();
    public GameObject GridPrefab;
    public GameObject Portal;
    public GameObject ARCamera;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        //check ARCore session status
        if(Session.Status != SessionStatus.Tracking)
        {
            return;
        }

        //following line will fill new tracked planes with the planes that arcore detected in current frame
        Session.GetTrackables<DetectedPlane>(m_trackedPlanes, TrackableQueryFilter.New);

        for (int i = 0; i < m_trackedPlanes.Count; i++)
        {
            GameObject grid = Instantiate(GridPrefab, Vector3.zero, Quaternion.identity, transform);
            grid.GetComponent<GridVisualiser>().Initialize(m_trackedPlanes[i]);
        }

        //check if user touched a screen
        Touch touch;
        if(Input.touchCount < 1 || (touch = Input.GetTouch(0)).phase != TouchPhase.Began)
        {
            return;
        }

        //check if user touched any of the tracked planes
        TrackableHit hit;
        TrackableHitFlags raycastFilter = TrackableHitFlags.PlaneWithinPolygon |
                TrackableHitFlags.FeaturePointWithSurfaceNormal;
        if (Frame.Raycast(touch.position.x, touch.position.y, raycastFilter, out hit))
        {
            //place the portal on top of tracked plane that touched
            //Since portal is disabled from unity, enable through the script
            Portal.SetActive(true);

            //create a new achor
            Anchor anchor = hit.Trackable.CreateAnchor(hit.Pose);

            //ser the position of portal at hit position
            Portal.transform.position = hit.Pose.position;
            Portal.transform.rotation = hit.Pose.rotation;

            if ((hit.Flags & TrackableHitFlags.PlaneWithinPolygon) != TrackableHitFlags.None)
            {
                Vector3 cameraPosition = ARCamera.transform.position;

                cameraPosition.y = hit.Pose.position.y;

                Portal.transform.LookAt(cameraPosition, Portal.transform.up);
            }

            Portal.transform.parent = anchor.transform;
        }

    }
}
