using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
public class VRbuttons2 : MonoBehaviour, IVirtualButtonEventHandler
{

    public GameObject control;// The virtual button
    public MeshRenderer object1;// The objects to be controlled using this button
    public MeshRenderer object2;
    public MeshRenderer object3;
    public MeshRenderer object4;
    public MeshRenderer object5;
   // public MeshRenderer object6;
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

        object1.enabled = false;

        object2.enabled = false;

        object3.enabled = false;

        object4.enabled = false;

        object5.enabled = false;

       // object6.enabled = false;


    }

    public void OnButtonReleased(VirtualButtonBehaviour vb)
    {
      
    }
}
