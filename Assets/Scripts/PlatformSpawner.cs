using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour {

    public Transform PlayerChar;
    public GameObject Platform;
    public GameObject MovingPlatform;
    public float DistanceOffset;

    private Vector3 PlayerStartingPosit;

    public float MovingPlatSpeed;

    public bool SpawnPlatform;
    

	// Use this for initialization
	void Start () {
        PlayerStartingPosit = PlayerChar.position;
        SpawnPlatform = false;
        MovingPlatSpeed = 2;
    }
	
	// Update is called once per frame
	void Update () {
        //Moves the spawner postion to the player's position + DistanceOffset in the z axis
        
        transform.position = new Vector3(PlayerChar.transform.position.x, 0, PlayerChar.transform.position.z + DistanceOffset);
        if (SpawnPlatform)
        {
            int Score = GameObject.FindGameObjectWithTag("Player").GetComponent<GameController>().getScore();

    
            if (Score > 6)
            {
                MovingPlatSpeed = Score/2;
                SpawnPlatType(MovingPlatform);

            }
            else if (Score > 3)
            {
                SpawnPlatType(MovingPlatform);

            }            
            else
            {
                SpawnPlatType(Platform);
            }            

        }
        
		
	}


    void SpawnPlatType(GameObject platFormType)
    {
        GameObject newPlat = Instantiate(platFormType);
        newPlat.transform.position = transform.position;
        SpawnPlatform = false;
    }

    public void RespawnGameStart()
    {
        GameObject[] platforms = GameObject.FindGameObjectsWithTag("Platform");
        foreach (GameObject plat in platforms)
        {
            Destroy(plat);
        }
        GameObject newPlat = Instantiate(Platform);
        newPlat.transform.position = new Vector3(PlayerStartingPosit.x, PlayerStartingPosit.y - 1, PlayerStartingPosit.z);

        PlayerChar.transform.position = PlayerStartingPosit;

    }
}
