using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionController : MonoBehaviour {

    GameController GC;
    // Use this for initialization
    void Start () {
        GC = GameObject.FindGameObjectWithTag("Player").GetComponent<GameController>();

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

        //Notify game controller that player is on platform        
        GC.onPlatform = true;

        //Add score        
        other.GetComponent<GameController>().addScore();

        //Moving Platform handler

        string tag = gameObject.tag;
        if (gameObject.tag == "MovingPlatform" || gameObject.tag == "BarrierMovingPlatform")
        {
            gameObject.GetComponent<MovementPlatformController>().isPlayerOn = true;
        }

    }

    void OnTriggerExit(Collider other)
    {
        if (gameObject.tag == "MovingPlatform")
        {
            GameObject PlatParent = gameObject.transform.parent.gameObject;            
            Destroy(PlatParent);
            
        }
        else
        {
            Destroy(gameObject);
        }

        GC.onPlatform = false;
    }


}
