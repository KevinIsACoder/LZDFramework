using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Author : #AUTHOR#
//CreateDate : #DATETIME#
//DESC : #DESC#
public class RaycastHitTest : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void FixedUpdate()
	{
		int layerMask = 1 << 9;
		RaycastHit hit;
		if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
		{
			Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
			Debug.Log("Did Hit");
		}
	}
}
