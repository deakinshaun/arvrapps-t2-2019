using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;

public class AugmentedImageDisplay : MonoBehaviour
{
    public GameObject [] displayObjectPrefabs = new GameObject[2];
    //public List<GameObject> displayObjects = new List<GameObject>();
    //private List<AugmentedImage> trackedImages = new List<AugmentedImage>();
    private List<AugmentedImage> updatedTrackedImages = new List<AugmentedImage>();
    private Dictionary<int, GameObject> m_Visualizers = new Dictionary<int, GameObject>();
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
        Session.GetTrackables<AugmentedImage>(updatedTrackedImages, TrackableQueryFilter.Updated);
        foreach (var image in updatedTrackedImages)
        {
            //check if the tracked image already has a visualizer
            GameObject visualizer = null;
            int index = image.DatabaseIndex;
            m_Visualizers.TryGetValue(index, out visualizer);
            if (image.TrackingState == TrackingState.Tracking && visualizer == null)
            {
                // Create an anchor to ensure that ARCore keeps tracking this augmented image.
                Anchor anchor = image.CreateAnchor(image.CenterPose);
                visualizer = Instantiate(displayObjectPrefabs[index], anchor.transform);
                m_Visualizers.Add(index, visualizer);
            }
            else if (image.TrackingState == TrackingState.Stopped && visualizer != null)
            {
                m_Visualizers.Remove(index);
                GameObject.Destroy(visualizer);
            }
        }
        //Session.GetTrackables<AugmentedImage>(trackedImages, TrackableQueryFilter.All);
        //foreach (var image in trackedImages)
        //{
        //    if (image.TrackingState == TrackingState.Tracking)
        //    {
        //        int index = image.DatabaseIndex;
        //        Debug.Log("Image index" + index);
        //        displayObjects[index].transform.position = image.CenterPose.position;
        //        displayObjects[index].transform.rotation = image.CenterPose.rotation;
        //        displayObjects[index].SetActive(true);
        //    }
        //}
    }
}
