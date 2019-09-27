using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PhysicalCameraTexture : MonoBehaviour {

  public GameObject webCameraPlane; 


  // Use this for initialization
  void Start () {

if(Application.isMobilePlatform){
	GameObject camp=new GameObject("camParent");
	camp.transform.position=this.transform.position;
	this.transform.parent=camp.transform;
	camp.transform.Rotate(Vector3.left,-90);
}
Input.gyro.enabled=true;
    WebCamTexture webCameraTexture = new WebCamTexture();
    webCameraPlane.GetComponent<MeshRenderer>().material.mainTexture = webCameraTexture;
    webCameraTexture.Play();




  }


	
  // Update is called once per frame
  void Update () {
Quaternion camr=new Quaternion(Input.gyro.attitude.x,Input.gyro.attitude.y,-Input.gyro.attitude.z,-Input.gyro.attitude.w);
this.transform.localRotation=camr;
  }
}
