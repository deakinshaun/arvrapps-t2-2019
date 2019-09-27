using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GoogleARCore; 	
#if UNITY_EDITOR
using input=GoogleARCore.InstantPreviewInput;
#endif
    public class ARController:MonoBehaviour{
	private List<DetectedPlane> p=new List<DetectedPlane>();
	public GameObject GridPrefab;
	public GameObject Portal;
	public GameObject ARCamera;
	void Start(){
		}
	void Update(){
		if(Session.Status!=SessionStatus.Tracking){
			return;
			}
			Session.GetTrackables<DetectedPlane>(p,TrackableQueryFilter.New);
			for(int i=0;i<p.Count;++i)
			{
			GameObject grid= Instantiate(GridPrefab, Vector3.zero, Quaternion.identity, transform);
			grid.GetComponent<GridVisualiser>().Initialize(p[i]);
			}
			Touch t;
			if(Input.touchCount<1||(t=Input.GetTouch(0)).phase!=TouchPhase.Began){
				return;}
				TrackableHit hit;
	if(Frame.Raycast(t.position.x,t.position.y,TrackableHitFlags.PlaneWithinPolygon,out hit)){
	Portal.SetActive(true);
	Anchor a=hit.Trackable.CreateAnchor(hit.Pose);
	Portal.transform.position=hit.Pose.position;
	Portal.transform.rotation=hit.Pose.rotation;
	Vector3 cameraPosition=ARCamera.transform.position;
	cameraPosition.y=hit.Pose.position.y;
	Portal.transform.LookAt(cameraPosition,Portal.transform.up);
	Portal.transform.parent=a.transform;
	}		}}	
			
