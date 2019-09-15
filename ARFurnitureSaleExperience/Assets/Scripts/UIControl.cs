using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIControl: MonoBehaviour
{
    public GameObject imageTracker;
    public Button imageTrackerButton;
    public GameObject furniturePlacement;
    public Button furniturePlacementButton;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchToImageTracker()
    {
        imageTracker.GetComponent<AugmentedImageDisplay>().TurnOn();
        imageTrackerButton.GetComponent<Image>().enabled = true;
    }
}
