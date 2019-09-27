using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;

	public class GPS : MonoBehaviour
    {
public GameObject g;
public GameObject fpsText;
private List<AugmentedImage> trackedImages=new List<AugmentedImage>();
void Start(){}
void Update(){	
	g.SetActive(false);
	Session.GetTrackables<AugmentedImage>(trackedImages,TrackableQueryFilter.All); 
	foreach(var image in trackedImages){		
fpsText.GetComponent<TextMesh>().text = "sdf";
	if(image.TrackingState==TrackingState.Tracking){ 
		g.transform.position=image.CenterPose.position;
		g.transform.rotation = image.CenterPose.rotation;
			fpsText.GetComponent<TextMesh>().text = g.transform.position.ToString();

		g.SetActive(true);}}
}
    /*
	*   Retrieve   the   location   from  the   location   service,   typically   using   GPS
,	Returns   true
	*   if   the   operation   succeeded,   or   false   if   location   is   not   available	at
the   current
10	*   time.
11	*/
    public bool retrieveLocation(out float latitude, out float longitude, out float altitude, out double timestamp)
    {
        latitude = 0.0f;
        longitude = 0.0f;
        altitude = 0.0f;
        timestamp = 0.0f;

        if (!Input.location.isEnabledByUser)
        {
            Debug.Log("Location   service   needs   to   be   enabled");
            return false;
        }
        if (Input.location.status != LocationServiceStatus.Running)
        {
            Debug.Log("Starting   location   service");
            if (Input.location.status == LocationServiceStatus.Stopped)
            { 
                Input.location.Start();
        }
        return false;
    }
	else
	{
	//   Valid data   is   available.
	latitude	=   Input.location.lastData.latitude;
	longitude	=   Input.location.lastData.longitude;
	altitude	=   Input.location.lastData.altitude;
    timestamp=Input.location.lastData.timestamp;
            return   true;
	}
}
}
