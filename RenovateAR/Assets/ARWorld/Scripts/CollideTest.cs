using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollideTest : MonoBehaviour {

    GameObject controllerParent;
    GameObject controller;
    GameObject furniture;
    public GameObject optionColor;
    public GameObject optionPosition;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
       	
	}

    void OnCollisionEnter(Collision collision)
    {
        controller = this.gameObject;
        controllerParent = GameObject.Find("ControllerParent");

        //Output the Collider's GameObject's name
        //this.gameObject.GetComponent<Renderer>().material.color = Color.red;
        Debug.Log(collision.collider.name);
        
    }

    //If your GameObject keeps colliding with another GameObject with a Collider, do something
    void OnCollisionStay(Collision collision)
    {
        Debug.Log("Coll stay");
        //Check to see if the Collider's name is "Sofa_01"
        if (collision.collider.name == "Sofa_01")
        {
            //toDestroy = GameObject.Find("Ball(Clone)");
            //Output the message
            this.gameObject.GetComponent<Renderer>().material.color = Color.white;
            furniture = GameObject.Find("Sofa_01");
            //furniture.transform.SetParent(controller.transform, true);
            SpeechRecognitionREST sp = furniture.AddComponent<SpeechRecognitionREST>() as SpeechRecognitionREST;
            //Destroy(this.gameObject);


            //Debug.Log("Chest is here!");
        }

        if (collision.collider.name == "Sphere")
        {
            //toDestroy = GameObject.Find("Ball(Clone)");
            //Output the message
            this.gameObject.GetComponent<Renderer>().material.color = Color.yellow;
            furniture = GameObject.Find("Sphere");
            furniture.GetComponent<Renderer>().material.color = Color.red;
            SpeechRecognitionREST sr = furniture.AddComponent<SpeechRecognitionREST>() as SpeechRecognitionREST;
            //furniture.transform.position = controller.transform.position;
            //Destroy(this.gameObject);


            //Debug.Log("Chest is here!");
        }

        if (collision.collider.name == "CornerTable")
        {
            //toDestroy = GameObject.Find("Ball(Clone)");
            //Output the message
            this.gameObject.GetComponent<Renderer>().material.color = Color.yellow;
            furniture = GameObject.Find("CornerTable");
            furniture.GetComponent<Renderer>().material.color = Color.red;
            SpeechRecognitionREST sr = furniture.AddComponent<SpeechRecognitionREST>() as SpeechRecognitionREST;
            //furniture.transform.position = controller.transform.position;
            //Destroy(this.gameObject);


            //Debug.Log("Chest is here!");
        }

        if (collision.collider.name == "Table")
        {
            //toDestroy = GameObject.Find("Ball(Clone)");
            //Output the message
            this.gameObject.GetComponent<Renderer>().material.color = Color.yellow;
            furniture = GameObject.Find("Table");
            furniture.GetComponent<Renderer>().material.color = Color.red;
            SpeechRecognitionREST sr = furniture.AddComponent<SpeechRecognitionREST>() as SpeechRecognitionREST;
            //furniture.transform.position = controller.transform.position;
            //Destroy(this.gameObject);


            //Debug.Log("Chest is here!");
        }

        if (collision.collider.name == "Cube")
        {
            //toDestroy = GameObject.Find("Ball(Clone)");
            //Output the message
            this.gameObject.GetComponent<Renderer>().material.color = Color.white;
            furniture = GameObject.Find("Cube");
            //furniture.transform.SetParent(controllerParent.transform, true);
            furniture.GetComponent<Renderer>().material.color = Color.yellow;
            //furniture.transform.position = controller.transform.position;
            SpeechRecognitionREST st = furniture.AddComponent<SpeechRecognitionREST>() as SpeechRecognitionREST;
            //optionColor.transform.position = controller.transform.position + new Vector3(-1, 0, 0);
            //Destroy(this.gameObject);


            //Debug.Log("Chest is here!");
        }
    }
}
