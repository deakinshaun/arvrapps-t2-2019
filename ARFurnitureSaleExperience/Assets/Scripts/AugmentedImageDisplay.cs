using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;

public class AugmentedImageDisplay : MonoBehaviour
{
    //public GameObject [] displayObjectPrefabs = new GameObject[2];
    public GameObject[] displayObjects = new GameObject[2];
    private List<AugmentedImage> trackedImages = new List<AugmentedImage>();
    //public AugmentedImageDatabase augmentedimagedatabase;
    //private List<AugmentedImage> updatedTrackedImages = new List<AugmentedImage>();
    //private Dictionary<int, GameObject> m_Visualizers = new Dictionary<int, GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void Awake()
    {
        // Enable ARCore to target 60fps camera capture frame rate on supported devices.
        // Note, Application.targetFrameRate is ignored when QualitySettings.vSyncCount != 0.
        Application.targetFrameRate = 60;
    }
    // Update is called once per frame
    void Update()
    {
        //Session.GetTrackables<AugmentedImage>(updatedTrackedImages, TrackableQueryFilter.Updated);
        //foreach (var image in updatedTrackedImages)
        //{
        //    //check if the tracked image already has a visualizer
        //    GameObject visualizer = null;
        //    int index = image.DatabaseIndex;
        //    m_Visualizers.TryGetValue(index, out visualizer);
        //    if (image.TrackingState == TrackingState.Tracking && visualizer == null)
        //    {
        //        // Create an anchor to ensure that ARCore keeps tracking this augmented image.
        //        Anchor anchor = image.CreateAnchor(image.CenterPose);
        //        visualizer = Instantiate(displayObjectPrefabs[index], anchor.transform);
        //        m_Visualizers.Add(index, visualizer);
        //    }
        //    else if (image.TrackingState == TrackingState.Stopped && visualizer != null)
        //    {
        //        m_Visualizers.Remove(index);
        //        GameObject.Destroy(visualizer);
        //    }
        //}
        //Debug.Log("update 111111111111");
        //debug purpose
        //for (int i = 0; i < 2; i++)
        //{
        //    Debug.Log("Game Oject " + i + " position " + displayObjects[i].transform.position + " rotation " + displayObjects[i].transform.rotation);
        //}
        Session.GetTrackables<AugmentedImage>(trackedImages, TrackableQueryFilter.All);
        foreach (var image in trackedImages)
        {
            int index = image.DatabaseIndex;
            //Debug.Log("Image index" + index);
            //Debug.Log("TrackingState " + image.TrackingState);
            if (image.TrackingState == TrackingState.Tracking)
            {
                //int index = image.DatabaseIndex;
                //Debug.Log("Image index" + index);
                //Debug.Log("Image pose: " + image.CenterPose.position + " rotation " + image.CenterPose.rotation);
           
                displayObjects[index].transform.position = image.CenterPose.position;
                displayObjects[index].transform.rotation = image.CenterPose.rotation;
                displayObjects[index].SetActive(true);
            }
        }
    }

    public void TurnOn()
    {
        Debug.Log("TurnOn");
        transform.gameObject.SetActive(true);
    }

    public void TurnOff()
    {
        Debug.Log("TurnOff");
        foreach (var displayObject in displayObjects)
        {
            displayObject.SetActive(false);
        }
        transform.gameObject.SetActive(false);
    }
}
