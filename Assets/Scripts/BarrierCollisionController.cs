using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierCollisionController : MonoBehaviour {

    private GameController GC;
    // Use this for initialization
    void Start () {
        GC = GameObject.FindGameObjectWithTag("Player").GetComponent<GameController>();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {        
        if (other.tag == "Player")
        {
            GC.pauseGame();
        }

    }
}
