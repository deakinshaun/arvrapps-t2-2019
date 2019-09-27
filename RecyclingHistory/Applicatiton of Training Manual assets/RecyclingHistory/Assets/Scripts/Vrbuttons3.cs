using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
using UnityEngine.SceneManagement;
public class Vrbuttons3 : MonoBehaviour, IVirtualButtonEventHandler
{

    public GameObject control;// The virtual button
    // Start is called before the first frame update
    void Start()
    {
        control.GetComponent<VirtualButtonBehaviour>().RegisterEventHandler(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnButtonPressed(VirtualButtonBehaviour vb)
    {

        SceneManager.LoadScene("GestureRecognition");


    }

    public void OnButtonReleased(VirtualButtonBehaviour vb)
    {

    }
}

