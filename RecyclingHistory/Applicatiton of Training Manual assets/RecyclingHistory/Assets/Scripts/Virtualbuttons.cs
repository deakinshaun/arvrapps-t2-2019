using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
using UnityEngine.Video;

public class Virtualbuttons : MonoBehaviour, IVirtualButtonEventHandler
{
    public GameObject videocontrol;// The virtual button
                                   
    public VideoPlayer videoPlayer;// The objects to be controlled using this button
    public GameObject video;
    public bool set = true;
    // Start is called before the first frame update
    void Start()
    {
        videocontrol.GetComponent<VirtualButtonBehaviour>().RegisterEventHandler(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnButtonPressed(VirtualButtonBehaviour vb)
    {
        if (set == true)
        {
            video.GetComponent<VideoPlayer>().Play();
        }
        else
        {
            video.GetComponent<VideoPlayer>().Pause();

        }
            
         
       
    }

    public void OnButtonReleased(VirtualButtonBehaviour vb)
    {
        if (set == true)
        {
            set = false;
        }
        else
        {
            set = true;
        }
    }
}

