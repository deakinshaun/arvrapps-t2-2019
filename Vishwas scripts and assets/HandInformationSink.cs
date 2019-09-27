using System.Collections;
using System.Collections.Generic;
using UnityEngine; 
using UnityEngine.SceneManagement; 

using UnityEngine.UI;
using Leap.Unity;
using Leap;
public class HandInformationSink : MonoBehaviour{
[Tooltip ("The hand model to work with.")]
public HandModelBase hand;
public GameObject fpsText;
[Tooltip("A cylindricalbeam object attached to the controller.")]
public GameObject laserBeam;
[Tooltip("An adjustment factor forhow fast objectsblow up.")]
public float inflationRate = 1.0f;
[Tooltip("A sound effect played during inflation.")]
public AudioSource hiss;
[Tooltip("A sound effect played when the object is destroyed.")]
public AudioSource pop;// The laser is switched on when the correct gesture is performed
public bool beamActive;// Link to the network functions.
public int flagz=0;

public Text message;
private DatagramCommunication dc;
public void setLaserActive (bool value){beamActive = value;}
public void setLaserActives (){   			fpsText.GetComponent<TextMesh>().text = "scenecalled";
 SceneManager.LoadScene("HelloAR");}
void Start(){
dc =new DatagramCommunication();
hand.BeginHand();}

void Update(){// decay hiss so it stops if no button is pressed.
	Debug.Log("gug");
	if(hiss !=null) { hiss.volume *= 0.9f; }
	HandDetails cd = dc.receiveHandDetails(message);
	if(cd !=null){
	message.text = "Got message " + Time.time + " " + cd.hand.Fingers[(int)Finger.FingerType.TYPE_INDEX].Bone(Bone.BoneType.TYPE_DISTAL).Center;
		hand.SetLeapHand(cd.hand);
		hand.UpdateHand();
		Debug.Log("werwr");
		laserBeam.SetActive(beamActive);
		if(beamActive)
		{ 
			Debug.Log("activeandgesture");
Finger f = hand.GetLeapHand().Fingers[(int)Finger.FingerType.TYPE_INDEX];// index finger.
			Bone b = f.Bone(Bone.BoneType.TYPE_DISTAL);
			Vector3 bCenter =new Vector3(b.Center.x, b.Center.y, b.Center.z);
			Vector3 bDirection =new Vector3(b.Direction.x, b.Direction.y, b.Direction.z);
			laserBeam.transform.position = bCenter;
			laserBeam.transform.forward = bDirection;// Raycast, inflate, explode.
			RaycastHit hit;
			if((Physics.Raycast(bCenter, bDirection,out hit, Mathf.Infinity)) &&(hit.collider.gameObject.tag =="Inflatable"))
			{// Inflate the objectby manipulating scale
				hit.collider.gameObject.transform.localScale *= 1.0f + (inflationRate * Time.deltaTime);
				// Play the inflation sound.
				if(hiss !=null) { hiss.volume = 1.0f;if(!hiss.isPlaying)hiss.Play(); }
		// Pop the object if it gets too big.
		if(hit.collider.gameObject.transform.localScale.magnitude >1.5)
		{
			Destroy(hit.collider.gameObject);
if(pop !=null)pop.Play();}}}}}}