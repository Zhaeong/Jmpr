using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public Transform obj_tracked;
    public float x_offset, y_offset, z_offset;
    public float x_rotation, y_rotation, z_rotation;
    public float smoothSpeed;

    private Vector3 velocity = Vector3.zero;

    private Vector3 specificVector;
    // Use this for initialization
    void Start () {
        transform.position = new Vector3(obj_tracked.position.x + x_offset, obj_tracked.position.y + y_offset, obj_tracked.position.z + z_offset);        
        transform.LookAt(obj_tracked);
        transform.rotation = Quaternion.Euler(new Vector3(x_rotation, y_rotation, z_rotation));
    }

    // Update is called once per frame
    void Update () {
        //transform.position = new Vector3(obj_tracked.position.x + x_offset, obj_tracked.position.y + y_offset, obj_tracked.position.z + z_offset);

        specificVector = new Vector3(obj_tracked.position.x + x_offset, obj_tracked.position.y + y_offset, obj_tracked.position.z + z_offset);
        //transform.position = Vector3.Slerp(transform.position, specificVector, smoothSpeed * Time.deltaTime);

        transform.position = Vector3.SmoothDamp(transform.position, specificVector, ref velocity, smoothSpeed);

    }
}
