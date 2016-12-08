using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        other.GetComponent<Rigidbody>().velocity = Vector3.zero;
        other.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        other.GetComponent<DirectionForceController>().bGrounded = true;
        GameObject PlatSpwn = GameObject.FindGameObjectWithTag("PlatformSpawner");
        GameObject icoSphere = GameObject.FindGameObjectWithTag("IcoSphere");
        icoSphere.transform.rotation = Quaternion.Euler(0,0,0);        
        PlatSpwn.GetComponent<PlatformSpawner>().SpawnPlatform = true;
    }

    void OnTriggerExit(Collider other)
    {
        Destroy(gameObject);
    }


}
