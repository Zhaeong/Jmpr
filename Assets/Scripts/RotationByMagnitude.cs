using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationByMagnitude : MonoBehaviour {

    public float MagnitudeofVelocity;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.rotation = Quaternion.Euler(MagnitudeofVelocity, 0, 0 );
	}
}
