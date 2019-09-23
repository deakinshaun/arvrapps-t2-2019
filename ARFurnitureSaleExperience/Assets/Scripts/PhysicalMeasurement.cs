using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GoogleARCore;

public class PhysicalMeasurement : MonoBehaviour
{
    //public Button MeasureConfirm;
    public GameObject distanceDisplayPrefab;
    public GameObject measureLinePrefab;
    private GameObject currentDisplay;
    private List<LineRenderer> lines = new List<LineRenderer>();
    private LineRenderer currentLine;
    private TrackableHit startPoint;
    private TrackableHit endPoint;
    private TrackableHitFlags raycastFilter;
    private bool isMeasuring = false;
    private float offset = 0.01f;
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
            currentLine.SetPosition(1, endPoint.Pose.position + new Vector3(0,offset,0));
            //get the distance
            float dx = startPoint.Pose.position.x - endPoint.Pose.position.x;
            float dy = startPoint.Pose.position.y - endPoint.Pose.position.y;
            float dz = startPoint.Pose.position.z - endPoint.Pose.position.z;
            float distanceMeters = (float)Mathf.Sqrt(dx * dx + dy * dy + dz * dz);
            currentDisplay.transform.position = new Vector3((startPoint.Pose.position.x + endPoint.Pose.position.x) / 2,
                (startPoint.Pose.position.y + endPoint.Pose.position.y) / 2 + offset,
                (startPoint.Pose.position.z + endPoint.Pose.position.z) / 2);
            currentDisplay.transform.rotation.SetFromToRotation(startPoint.Pose.position, endPoint.Pose.position);
            currentDisplay.GetComponentInChildren<Text>().text = $"{distanceMeters:F2} m";
        }
    }

    public void Measure()
    {
        if (!isMeasuring)
        {
            currentLine = Instantiate(measureLinePrefab).GetComponent<LineRenderer>();
            lines.Add(currentLine);            
            //cast a ray from center screen to the detected plane
            Frame.Raycast(Screen.width * 0.5f, Screen.height * 0.5f, raycastFilter, out startPoint);
            currentDisplay = Instantiate(distanceDisplayPrefab, startPoint.Pose.position, Quaternion.Euler(90,0,0));
            currentLine.SetPosition(0, startPoint.Pose.position + new Vector3(0,offset,0));
            isMeasuring = true;
            //MeasureConfirm.GetComponentInChildren<Text>().text = "End";
        }
        else
        {
            isMeasuring = false;
            //MeasureConfirm.GetComponentInChildren<Text>().text = "Start";
        }
    }

    public void Toggle()
    {
        if (transform.gameObject.activeSelf == true)
        {
            transform.gameObject.SetActive(false);
            //MeasureConfirm.gameObject.SetActive(false);
            //distanceText.gameObject.SetActive(false);
        }

        else
        {
            transform.gameObject.SetActive(true);
            //MeasureConfirm.gameObject.SetActive(true);
            //distanceText.gameObject.SetActive(true);
        }
    }
}