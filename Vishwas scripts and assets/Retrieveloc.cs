	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using UnityEngine.UI;

public class Retrieveloc : MonoBehaviour
{

    public GPS locationService;

    public GameObject displayText;

    void Update()
    {
        float latitude;
        float longitude;
        float altitude;
        double timestamp;
        if (locationService.retrieveLocation(out latitude, out
      longitude, out altitude, out timestamp))
        {
            displayText.GetComponent<TextMesh>().text = "Lat:   " + latitude;
        }
        else { 
            displayText.GetComponent<TextMesh>().text = "No location";
    }
}
            
            }
