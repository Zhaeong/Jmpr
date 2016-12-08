using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public Transform obj_tracked;
    public float x_offset, y_offset, z_offset;
	// Use this for initialization
	void Start () {
        transform.position = new Vector3(obj_tracked.position.x + x_offset, obj_tracked.position.y + y_offset, obj_tracked.position.z + z_offset);
        transform.LookAt(obj_tracked);
    }

    // Update is called once per frame
    void Update () {
        transform.position = new Vector3(obj_tracked.position.x + x_offset, obj_tracked.position.y + y_offset, obj_tracked.position.z + z_offset);
    }
}
