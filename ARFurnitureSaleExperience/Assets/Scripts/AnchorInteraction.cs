using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GoogleARCore;
using GoogleARCore.CrossPlatform;
using System.IO;

public class AnchorInteraction : MonoBehaviour
{

    public AnchorList anchorList;
    public Camera firstPersonCamera;
    //public Text updateMessage;

    void Update()
    {
        //Touch touch;
        //if (Input.touchCount < 1 || (touch = Input.GetTouch(0)).phase !=TouchPhase.Began)
        //{
        //    return;
        //}
  
        
    }

    public void CreateObject()
    {
        // Screen touched , find a trackable in the image under the touch point.
        TrackableHit hit;
        if (Frame.Raycast(Screen.width/2, Screen.height/2, TrackableHitFlags.PlaneWithinPolygon | TrackableHitFlags.FeaturePointWithSurfaceNormal, out hit))
        {
            //Debug.Log("11111111111111111");
            if (!(hit.Trackable is DetectedPlane) || Vector3.Dot(firstPersonCamera.transform.position - hit.Pose.position,
                hit.Pose.rotation * Vector3.up) >= 0)
            {
                //Debug.Log("222222222222222222");
                // Hit a point or front of a plane.
                // Create a local anchor on that trackable.
                Component anchor = hit.Trackable.CreateAnchor(hit.Pose);
                if (anchorList != null)
                {
                    //Debug.Log("333333333333333333");
                    string label = anchorList.getLabel();
                    // Attach object to the anchor.
                    anchorList.addInstanceToAnchor(label, anchor.transform, hit.Pose.position, hit.Pose.rotation);
                    // Instantiate(markerPrefab , hit.Pose.position , hit.Pose.rotation);

                    // Copy the anchor to the cloud.
                    anchorList.createCloudAnchor(label, (Anchor)anchor);
                }
            }
        }
    }
}