using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Net.Sockets;
using System.Net;
using System;
using Leap.Unity;
using Leap;

public class DatagramCommunication
{
private int port = 8082;
private UdpClient udpClient;
private HandDetails lastHand;
private bool controlValid =false;
private IPEndPoint remoteep;
private int l;
public DatagramCommunication()  {
udpClient =new UdpClient();
udpClient.Client.Bind(new IPEndPoint(IPAddress.Any, port));
IPAddress multicastaddress = IPAddress.Parse("239.0.0.222");
udpClient.JoinMulticastGroup(multicastaddress); 
remoteep =new IPEndPoint(multicastaddress, port);
udpClient.BeginReceive(new AsyncCallback(udpReceive),null);
}
// Retrieve themost recently receivedhand details.
public HandDetails receiveHandDetails(Text m) {
  if(controlValid) 
    { Debug.Log("datagram");
      controlValid =false;
	m.text = "Got length " + l + " " + Time.time;
	  return lastHand;
	}
  Debug.Log("datagramnl");
  return null;
}// Wait for a message toarrive. Set thatas last messagereceived.

private void udpReceive(IAsyncResult res) { 
IPEndPoint from =new IPEndPoint(0, 0);
byte[] buffer = udpClient.Receive(ref from);
l = buffer.Length;
HandDetails details = HandDetails.deserialize(buffer);
lastHand = details;
controlValid =true;
 Debug.Log("datagramnul");
udpClient.BeginReceive(new AsyncCallback(udpReceive),null);
}
public void sendHandDetails(string hostname,HandDetails details) {
if(hostname !=null){Debug.Log(remoteep);
remoteep =new IPEndPoint(IPAddress.Parse(hostname), port);
Debug.Log(remoteep);
}
	Debug.Log ("send " + Time.time + " " + details.hand.Fingers[(int)Finger.FingerType.TYPE_INDEX].Bone(Bone.BoneType.TYPE_DISTAL).Direction);

byte[] data = details.serialize();
Debug.Log(hostname);
int a = udpClient.Send(data,data.Length,remoteep);
Debug.Log ("Sent " + a + " " + data.Length);
}
}