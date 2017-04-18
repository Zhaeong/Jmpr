using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPlatformController : MonoBehaviour {

    public GameObject Platform;
    public GameObject Anchor1;
    public GameObject Anchor2;
    public bool isPlayerOn;

    public bool moveRight;

    // Use this for initialization
    void Start () {
        int random = Random.Range(0, 2);        
        moveRight = (random == 1);
        
    }
	
	// Update is called once per frame
	void Update () {
        if (!isPlayerOn)
        {
            float speed = GameObject.FindGameObjectWithTag("PlatformSpawner").GetComponent<PlatformSpawner>().MovingPlatSpeed;
            if (gameObject.tag == "MovingPlatform")
            {
                speed = GameObject.FindGameObjectWithTag("PlatformSpawner").GetComponent<PlatformSpawner>().MovingPlatSpeed;
            }
            else if (gameObject.tag == "BarrierMovingPlatform")
            {
                speed = GameObject.FindGameObjectWithTag("PlatformSpawner").GetComponent<PlatformSpawner>().BarrierMovingPlatSpeed;
            }
            
            float step = speed * Time.deltaTime;

            if (moveRight)
            {
                Platform.transform.position = Vector3.MoveTowards(Platform.transform.position, Anchor1.transform.position, step);
                if (Platform.transform.position == Anchor1.transform.position)
                {
                    moveRight = false;
                }
            }
            else
            {
                Platform.transform.position = Vector3.MoveTowards(Platform.transform.position, Anchor2.transform.position, step);
                if (Platform.transform.position == Anchor2.transform.position)
                {
                    moveRight = true;
                }
            }
        }
    }
}
