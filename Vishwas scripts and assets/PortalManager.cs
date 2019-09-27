using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;


public class PortalManager : MonoBehaviour
{
	public GameObject mainCamera;
	public GameObject Sponza;
	private Material[] SponzaMaterials;
	void Start(){SponzaMaterials=Sponza.GetComponent<Renderer>().sharedMaterials;}
void OnTriggerStay(Collider collider){
	mainCamera=GameObject.Find("First Person Camera");
	Vector3 camPositionInPortalSpace=transform.InverseTransformPoint(mainCamera.transform.position);
	if(camPositionInPortalSpace.y<0.5f){
		for(int i =0;i<SponzaMaterials.Length;++i){
		SponzaMaterials[i].SetInt("_StencilComp",(int)CompareFunction.Always);
		}
		}
		else{for(int i =0;i<SponzaMaterials.Length;++i){
		SponzaMaterials[i].SetInt("_StencilComp",(int)CompareFunction.Equal);
		}
			}
}
}