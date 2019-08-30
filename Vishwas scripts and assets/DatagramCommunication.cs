using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System.Net;
using System;
public class DatagramCommunication
{
private int port = 8082;
private UdpClient udpClient;
private HandDetails lastHand;
private bool controlValid =false;
private IPEndPoint remoteep;
public DatagramCommunication()  {
udpClient =new UdpClient();
udpClient.Client.Bind(new IPEndPoint(IPAddress.Any, port));
IPAddress multicastaddress = IPAddress.Parse("239.0.0.222");
udpClient.JoinMulticastGroup(multicastaddress); 
remoteep =new IPEndPoint(multicastaddress, port);
udpClient.BeginReceive(new AsyncCallback(udpReceive),null);
}
// Retrieve themost recently receivedhand details.
public HandDetails receiveHandDetails() {
if(controlValid) { Debug.Log("datagram");
controlValid =false;
return lastHand;} Debug.Log("datagramnl");
return null;}// Wait for a message toarrive. Set thatas last messagereceived.
private void udpReceive(IAsyncResult res) { 
IPEndPoint from =new IPEndPoint(0, 0);
byte[] buffer = udpClient.Receive(ref from);
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
byte[] data = details.serialize();
Debug.Log(hostname);
udpClient.Send(data,data.Length,remoteep);
}
}