using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 
public class Choose : MonoBehaviour {
	
public GameObject fpsText;
		


	public bool thr=false;

	public bool eig=false;


void Update () {
      
}
	public void dd(){
		fpsText.GetComponent<TextMesh>().text = "scenecalled";
 SceneManager.LoadScene("HelloAR");
}

}