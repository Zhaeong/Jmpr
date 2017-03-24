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
        //Remove the sphere spin
        other.GetComponent<Rigidbody>().velocity = Vector3.zero;
        other.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        GameObject icoSphere = GameObject.FindGameObjectWithTag("IcoSphere");
        icoSphere.transform.rotation = Quaternion.Euler(0, 0, 0);
        //indicate ground so that new spin can be applied
        other.GetComponent<DirectionForceController>().bGrounded = true;

        //Spawn other platform
        GameObject PlatSpwn = GameObject.FindGameObjectWithTag("PlatformSpawner");
        PlatSpwn.GetComponent<PlatformSpawner>().SpawnPlatform = true;
        //Add score        
        other.GetComponent<GameController>().addScore();

        //Moving Platform handler

        string tag = gameObject.tag;
        if (gameObject.tag == "MovingPlatform")
        {
            gameObject.GetComponent<MovementPlatformController>().isPlayerOn = true;
        }

    }

    void OnTriggerExit(Collider other)
    {
        Destroy(gameObject);
        GameObject PlatSpwn = GameObject.FindGameObjectWithTag("PlatformSpawner");
        PlatSpwn.GetComponent<PlatformSpawner>().SpawnPlatform = false;
    }


}
