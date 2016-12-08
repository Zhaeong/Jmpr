using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public Transform obj_tracked;
    public float x_offset, y_offset, z_offset;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.LookAt(obj_tracked);
        transform.position = obj_tracked.position - new Vector3(x_offset, y_offset, z_offset);
		
	}
}
