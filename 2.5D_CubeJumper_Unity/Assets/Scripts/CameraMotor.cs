﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMotor : MonoBehaviour {


	public Transform lookAt;
	private Vector3 offset = new Vector3(0,2,-10.0f);
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = lookAt.transform.position + offset;
	}
}
