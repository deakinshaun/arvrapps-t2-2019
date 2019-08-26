	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using UnityEngine.UI;

public class Retrieveloc : MonoBehaviour
{

    public GPS locationService;

    public Text displayText;

    void Update()
    {
        float latitude;
        float longitude;
        float altitude;
        double timestamp;
        if (locationService.retrieveLocation(out latitude, out
      longitude, out altitude, out timestamp))
        {
            displayText.text = "Lat:   " + latitude + "\n"+
            "Long:   " + longitude + "\n" +
            "Alt:   " + altitude + "\n" +
            "Timestamp:   " + timestamp;
        }
        else { 
            displayText.text = "No location";
    }
}
            
            }
