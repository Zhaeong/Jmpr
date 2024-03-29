﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationByMagnitude : MonoBehaviour {

    //public float MagnitudeofVelocity;
    public float In_air_rotation_damping;
    public float Grounded_rotation_damping;
    public Vector3 rotationAngle;

    private float x_rot, y_rot, z_rot;

	// Use this for initialization
	void Start () {
        y_rot = 0;
        x_rot = 0;
    }
	
	// Update is called once per frame
	void Update () {
        GameObject Sphere = GameObject.FindGameObjectWithTag("IcoSphere");

        

        GameObject player = GameObject.FindGameObjectWithTag("Player");

        //In air rotation after button released
        if (!player.GetComponent<DirectionForceController>().bGrounded)
        {
            //Sphere.transform.Rotate(y_rot += rotationAngle.y / In_air_rotation_damping, 0, x_rot -= rotationAngle.x / In_air_rotation_damping);

            Sphere.transform.rotation = Quaternion.Euler(y_rot += rotationAngle.y / In_air_rotation_damping, 0, x_rot -= rotationAngle.x / In_air_rotation_damping);
            //Debug.Log(rotationAngle);
        }
        else
        {
            Sphere.transform.rotation = Quaternion.Euler(rotationAngle.y / Grounded_rotation_damping, 0, -rotationAngle.x / Grounded_rotation_damping);
            y_rot = rotationAngle.y;
            x_rot = rotationAngle.x;
        }
        
    }
}
