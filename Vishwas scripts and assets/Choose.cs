using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Choose : MonoBehaviour {
	public GameObject three;
		public GameObject eight;

		public GameObject threed;
				public GameObject eightd;


	public bool thr=false;

	public bool eig=false;


void Update () {
        if(thr){  three.SetActive(true);
		 three.transform.Translate(0,0,5*Time.deltaTime);
		}
		
	if(three.transform.position.x<=-18.58f){thr=false;
		eig=true;
		 threed.SetActive(true);
		 if(threed.transform.position.z>=-1)threed.transform.Translate(0,0,5*Time.deltaTime);

		}
				if(eight.transform.position.x>=-4.58f){eig=false; eightd.SetActive(true);
		 if(eightd.transform.position.z>=-0.8f)eightd.transform.Translate(0,0,-5*Time.deltaTime);}

		 if(eig){  eight.SetActive(true);
		 eight.transform.Translate(0,0,-5*Time.deltaTime);
		}
}
	public void dd(){
		thr=true;
		 Debug.Log("caled");
}
public int modcalc(int primemodulus, int generator, int privatenumber){
	int result=0;
result=((int)(Mathf.Pow(generator,privatenumber)))%primemodulus;
	return result;
}
}