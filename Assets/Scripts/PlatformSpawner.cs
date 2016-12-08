using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour {

    public Transform PlayerChar;
    public GameObject Platform;
    public float DistanceOffset;

    public bool SpawnPlatform;

	// Use this for initialization
	void Start () {
        SpawnPlatform = false;


    }
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(PlayerChar.transform.position.x, 0, PlayerChar.transform.position.z + DistanceOffset);
        if (SpawnPlatform)
        {
            GameObject newPlat = Instantiate(Platform);
            newPlat.transform.position = transform.position;
            SpawnPlatform = false;
        }
		
	}
}
