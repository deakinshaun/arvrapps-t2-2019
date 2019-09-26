﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ArduinoBluetoothAPI;
using System;
using TMPro;

public class manager : MonoBehaviour
{

    // Use this for initialization
    BluetoothHelper bluetoothHelper;
    string deviceName;

    public TextMeshPro text;

    public GameObject sphere;

    string received_message;

    void Start()
    {

 

        deviceName = "HC-05"; //bluetooth should be turned ON;
        try
        {
            bluetoothHelper = BluetoothHelper.GetInstance(deviceName);
            bluetoothHelper.OnConnected += OnConnected;
            bluetoothHelper.OnConnectionFailed += OnConnectionFailed;
            bluetoothHelper.OnDataReceived += OnMessageReceived; //read the data

           // bluetoothHelper.setFixedLengthBasedStream(1); //receiving every 3 characters together
                                                          // bluetoothHelper.setTerminatorBasedStream("\n"); //delimits received messages based on \n char
                                                          //if we received "Hi\nHow are you?"
                                                          //then they are 2 messages : "Hi" and "How are you?"


             bluetoothHelper.setLengthBasedStream();
            /*
			will received messages based on the length provided, this is useful in transfering binary data
			if we received this message (byte array) :
			{0x55, 0x55, 0, 3, 'a', 'b', 'c', 0x55, 0x55, 0, 9, 'i', ' ', 'a', 'm', ' ', 't', 'o', 'n', 'y'}
			then its parsed as 2 messages : "abc" and "i am tony"
			the first 2 bytes are the length data writted on 2 bytes
			byte[0] is the MSB
			byte[1] is the LSB

			on the unity side, you dont have to add the message length implementation.

			if you call bluetoothHelper.SendData("HELLO");
			this API will send automatically :
			 0x55 0x55    0x00 0x05   0x68 0x65 0x6C 0x6C 0x6F
			|________|   |________|  |________________________|
			 preamble      Length             Data

			
			when sending data from the arduino to the bluetooth, there's no preamble added.
			this preamble is used to that you receive valid data if you connect to your arduino and its already send data.
			so you will not receive 
			on the arduino side you can decode the message by this code snippet:
			char * data;
			char _length[2];
			int length;

			if(Serial.avalaible() >2 )
			{
				_length[0] = Serial.read();
				_length[1] = Serial.read();
				length = (_length[0] << 8) & 0xFF00 | _length[1] & 0xFF00;

				data = new char[length];
				int i=0;
				while(i<length)
				{
					if(Serial.available() == 0)
						continue;
					data[i++] = Serial.read();
				}


				...process received data...


				delete [] data; <--dont forget to clear the dynamic allocation!!!
			}
			*/

           // if (bluetoothHelper.isDevicePaired())
              //  sphere.GetComponent<Renderer>().material.color = Color.blue;

            if (!bluetoothHelper.isConnected())
            // if (GUI.Button(new Rect(Screen.width / 2 - Screen.width / 10, Screen.height / 10, Screen.width / 5, Screen.height / 10), "Connect"))
            {
                if (bluetoothHelper.isDevicePaired())
                    bluetoothHelper.Connect(); // tries to connect
                else
                    sphere.GetComponent<Renderer>().material.color = Color.magenta;
            }
            else
                sphere.GetComponent<Renderer>().material.color = Color.grey;
        }
        catch (Exception ex)
        {
            sphere.GetComponent<Renderer>().material.color = Color.yellow;
            Debug.Log("Exception " + ex.Message);
            text.text = ex.Message;
            //BlueToothNotEnabledException == bluetooth Not turned ON
            //BlueToothNotSupportedException == device doesn't support bluetooth
            //BlueToothNotReadyException == the device name you chose is not paired with your android or you are not connected to the bluetooth device;
            //								bluetoothHelper.Connect () returned false;
        }
    }

    IEnumerator blinkSphere()
    {
        sphere.GetComponent<Renderer>().material.color = Color.cyan;
        yield return new WaitForSeconds(0.5f);
        sphere.GetComponent<Renderer>().material.color = Color.green;
    }



    public float timeLeft = 3f;
    private int pulsesRecieved;
    private int pulseBPM;
    private int beatsPerMinute;
    public TextMeshPro BPMcounter;

    // Update is called once per frame
    void Update()
    {
        
        timeLeft -= Time.deltaTime;
        if (timeLeft <= 0)
        {
            pulseBPM = pulsesRecieved *  20;
            
            beatsPerMinute = pulseBPM; //multiply to make a minute
            timeLeft = 3f; //reset timer to 3 seconds
            pulsesRecieved = 0;

        }


        BPMcounter.text = beatsPerMinute.ToString(); //display BPM


        /*
        //Synchronous method to receive messages
        if(bluetoothHelper != null)
        if (bluetoothHelper.Available)
            received_message = bluetoothHelper.Read ();
        */
    }

    //Asynchronous method to receive messages




    void OnMessageReceived()
    {
        StartCoroutine(blinkSphere());
        received_message = bluetoothHelper.Read();
        int v = (int)(received_message[1]) + 256 * (int)(received_message[2]);
        Debug.Log("Get message " + "  "+ v);

        pulsesRecieved ++;
        




        //create timer make timer run for 5 seconds multiply the counted messages by 12
        //start again
        
    }

    void OnConnected()
    {
        sphere.GetComponent<Renderer>().material.color = Color.green;
        try
        {
            bluetoothHelper.StartListening();
        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message);
        }

    }

    void OnConnectionFailed()
    {
        sphere.GetComponent<Renderer>().material.color = Color.red;
        Debug.Log("Connection Failed");
    }


    //Call this function to emulate message receiving from bluetooth while debugging on your PC.
    void OnGUI()
    {
        if (bluetoothHelper != null)
            bluetoothHelper.DrawGUI();
        else
            return;
/*
        if (!bluetoothHelper.isConnected())
           // if (GUI.Button(new Rect(Screen.width / 2 - Screen.width / 10, Screen.height / 10, Screen.width / 5, Screen.height / 10), "Connect"))
           {
                if (bluetoothHelper.isDevicePaired())
                    bluetoothHelper.Connect(); // tries to connect
                else
                    sphere.GetComponent<Renderer>().material.color = Color.magenta;
            }
            */

      //  if (bluetoothHelper.isConnected())
       //     if (GUI.Button(new Rect(Screen.width / 2 - Screen.width / 10, Screen.height - 2 * Screen.height / 10, Screen.width / 5, Screen.height / 10), "Disconnect"))
         //   {
        //        bluetoothHelper.Disconnect();
        //        sphere.GetComponent<Renderer>().material.color = Color.blue;
        //    }

    //    if (bluetoothHelper.isConnected())
         //   if (GUI.Button(new Rect(Screen.width / 2 - Screen.width / 10, Screen.height / 10, Screen.width / 5, Screen.height / 10), "Send text"))
      //      {
            //    bluetoothHelper.SendData("This is a very long long long long text");
       //   }
    }

    void OnDestroy()
    {
        if (bluetoothHelper != null)
            bluetoothHelper.Disconnect();
    }
}