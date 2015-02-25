﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HideObjects : MonoBehaviour {
	
	private Transform WatchTarget;
	public LayerMask OccluderMask;
	//This is the material with the Transparent/Diffuse With Shadow shader
	public Material HiderMaterial;
	
	private Dictionary<Transform, Material> _LastTransforms;
	
	void Start () {
		_LastTransforms = new Dictionary<Transform, Material>();
	}
	
	void Update () {
		//reset and clear all the previous objects
		if(transform.GetComponent<CameraBoomController>().target != null){
			WatchTarget = transform.GetComponent<CameraBoomController>().target;
			hideObjectsInBetweenTargetAndCamera();
		}
		
	}
	
	void hideObjectsInBetweenTargetAndCamera(){
		if(_LastTransforms.Count > 0){
			foreach(Transform t in _LastTransforms.Keys){
				t.renderer.material = _LastTransforms[t];
			}
			_LastTransforms.Clear();
		}
		
		//Cast a ray from this object's transform the the watch target's transform.
		RaycastHit[] hits = Physics.RaycastAll(
			WatchTarget.transform.position,
			transform.position - WatchTarget.transform.position,
			Vector3.Distance(WatchTarget.transform.position, transform.position),
			OccluderMask
			);
		
		//Loop through all overlapping objects and disable their mesh renderer
		if(hits.Length > 0){
			foreach(RaycastHit hit in hits){
				if(hit.collider.gameObject.transform != WatchTarget && hit.collider.transform.root != WatchTarget && !_LastTransforms.ContainsKey(hit.collider.gameObject.transform)){
					_LastTransforms.Add(hit.collider.gameObject.transform, hit.collider.gameObject.renderer.material);
					hit.collider.gameObject.renderer.material = HiderMaterial;
				}
			}
		}
	}
}
