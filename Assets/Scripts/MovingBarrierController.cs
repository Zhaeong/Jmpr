using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBarrierController : MonoBehaviour {

    public GameObject Barrier;

    public GameObject Anchor1;
    public GameObject Anchor2;
    public GameObject Platform;

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
            if (Platform != null)
            {
                if (Platform.GetComponent<MovementPlatformController>().moveRight)
                {
                    moveRight = false;
                }
                else
                {
                    moveRight = true;
                }
            }

            float speed = GameObject.FindGameObjectWithTag("PlatformSpawner").GetComponent<PlatformSpawner>().MovingPlatSpeed;            

            float step = speed * Time.deltaTime;

            if (moveRight)
            {
                Barrier.transform.position = Vector3.MoveTowards(Barrier.transform.position, Anchor1.transform.position, step);
                if (Barrier.transform.position == Anchor1.transform.position)
                {
                    moveRight = false;
                }
            }
            else
            {
                Barrier.transform.position = Vector3.MoveTowards(Barrier.transform.position, Anchor2.transform.position, step);
                if (Barrier.transform.position == Anchor2.transform.position)
                {
                    moveRight = true;
                }
            }
        }

    }
}
