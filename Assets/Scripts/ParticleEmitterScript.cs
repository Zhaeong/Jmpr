using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEmitterScript : MonoBehaviour {
    public float offset;
    private GameObject PC;
	// Use this for initialization
	void Start () {
        PC = GameObject.FindGameObjectWithTag("Player");
    }
	
	// Update is called once per frame
	void Update () {

        Vector3 PCPosition = PC.transform.position;

        transform.position = new Vector3(PCPosition.x, 0, PCPosition.z + offset);

    }
}
