using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GoogleARCore;

public class PhysicalMeasurement : MonoBehaviour
{
    public Button MeasureConfirm;
    public Text distanceText;
    public LineRenderer measureLine;
    private TrackableHit startPoint;
    private TrackableHit endPoint;
    private TrackableHitFlags raycastFilter;
    private bool isMeasuring = false;
    // Start is called before the first frame update
    void Start()
    {
        raycastFilter = TrackableHitFlags.PlaneWithinPolygon |
            TrackableHitFlags.FeaturePointWithSurfaceNormal;
    }

    // Update is called once per frame
    void Update()
    {
        if (isMeasuring)
        {
            Frame.Raycast(Screen.width * 0.5f, Screen.height * 0.5f, raycastFilter, out endPoint);
            measureLine.SetPosition(1, endPoint.Pose.position);
            //get the distance
            float dx = startPoint.Pose.position.x - endPoint.Pose.position.x;
            float dy = startPoint.Pose.position.y - endPoint.Pose.position.y;
            float dz = startPoint.Pose.position.z - endPoint.Pose.position.z;
            float distanceMeters = (float)Mathf.Sqrt(dx * dx + dy * dy + dz * dz);
            distanceText.text = distanceMeters + "m";
        }
    }
    
    public void Measure()
    {
        if (!isMeasuring)
        {
            //cast a ray from center screen to the detected plane
            Frame.Raycast(Screen.width * 0.5f, Screen.height * 0.5f, raycastFilter, out startPoint);
            measureLine.SetPosition(0, startPoint.Pose.position);
            isMeasuring = true;
            MeasureConfirm.GetComponentInChildren<Text>().text = "End";
        }
        else
        {
            isMeasuring = false;
            MeasureConfirm.GetComponentInChildren<Text>().text = "Start";
        }
    }

    public void Toggle()
    {
        if (transform.gameObject.activeSelf == true)
        {
            transform.gameObject.SetActive(false);
            MeasureConfirm.gameObject.SetActive(false);
            distanceText.gameObject.SetActive(false);
        }

        else
        {
            transform.gameObject.SetActive(true);
            MeasureConfirm.gameObject.SetActive(true);
            distanceText.gameObject.SetActive(true);
        }
    }
}
