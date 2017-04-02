using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSpawnerController : MonoBehaviour {

    public GameObject BackgroundObj;

    public float y_offset;

    private GameObject player;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");

    }
	
	// Update is called once per frame
	void Update () {

        transform.position = new Vector3(player.transform.position.x, player.transform.position.y - y_offset, player.transform.position.z);
		
	}
}
