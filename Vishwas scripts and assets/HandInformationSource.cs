using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using Leap.Unity;
public class HandInformationSource : MonoBehaviour {

[Tooltip ("The hand that is transmitted over the network")]
public HandModelBase hand;
[Tooltip ("The device that receives the hand updates. Leave blank formulticast.")]
public string hostname;
private DatagramCommunication dc;
void Start()
{
dc =new DatagramCommunication ();
}
void Update () {
	Debug.Log ("Sending " + hostname + " ");
dc.sendHandDetails (hostname,new HandDetails (hand));
}}