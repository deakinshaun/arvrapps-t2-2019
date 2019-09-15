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
    public GameObject collectionPanel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchToImageTrackerMode()
    {
        imageTracker.GetComponent<AugmentedImageDisplay>().TurnOn();
        imageTrackerButton.GetComponent<Image>().enabled = true;
        furniturePlacementButton.GetComponent<Image>().enabled = false;
    }

    public void SwitchToFurniturePlacementMode()
    {
        imageTracker.GetComponent<AugmentedImageDisplay>().TurnOff();
        imageTrackerButton.GetComponent<Image>().enabled = false;
        furniturePlacementButton.GetComponent<Image>().enabled = true;
    }

    public void ToggleItemCollection()
    {
        if (collectionPanel.activeSelf == true)
            collectionPanel.SetActive(false);
        else collectionPanel.SetActive(true);
    }
}
