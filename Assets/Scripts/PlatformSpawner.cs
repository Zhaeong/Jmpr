using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour {

    public Transform PlayerChar;

    //Platforms
    public GameObject Platform;
    public GameObject MovingPlatform;
    public GameObject BarrierPlatform;
    public GameObject BarrierMovingPlatform;


    public float DistanceOffsetMin, DistanceOffsetMax;

    private float DistanceOffsetMaxAug;

    private Vector3 PlayerStartingPosit;

    public float MovingPlatSpeed, BarrierMovingPlatSpeed;

    public bool SpawnPlatform;

    private int GameScore;
    private int RangesAvailSpawn;
    

	// Use this for initialization
	void Start () {
        PlayerStartingPosit = PlayerChar.position;
        SpawnPlatform = false;
        MovingPlatSpeed = 2;
        BarrierMovingPlatSpeed = 2;
        RangesAvailSpawn = 1;

        DistanceOffsetMaxAug = DistanceOffsetMax;
    }
	
	// Update is called once per frame
	void Update () {

        
        if (SpawnPlatform)
        {
            //Moves the spawner postion to the player's position + DistanceOffset in the z axis

            GameScore = GameObject.FindGameObjectWithTag("Player").GetComponent<GameController>().getScore();
            float DistanceOffset = Random.Range(DistanceOffsetMin, DistanceOffsetMaxAug);
            transform.position = new Vector3(PlayerChar.transform.position.x, 0, PlayerChar.transform.position.z + DistanceOffset);



            if (GameScore == 3)
            {
                RangesAvailSpawn += 1;
                SpawnPlatByIndex(1);
            }
            else if (GameScore == 10)
            {
                RangesAvailSpawn += 1;
                SpawnPlatByIndex(2);
            }
            else if (GameScore == 20)
            {
                RangesAvailSpawn += 1;
                SpawnPlatByIndex(3);
            }
            else
            {
                //Determine which platform to spawn
                int whichPlattoSpawn = Random.Range(0, RangesAvailSpawn);
                SpawnPlatByIndex(whichPlattoSpawn);

                //Add distance by which platform can spawn
                DistanceOffsetMaxAug += GameScore / 8;                       

            }            
            
        } 
	}


    void SpawnPlatByIndex(int PlatNum)
    {
        switch (PlatNum)
        {
            case 0:
                SpawnPlatType(Platform);
                break;
            case 1:
                SpawnPlatType(MovingPlatform);
                MovingPlatSpeed +=1;
                break;
            case 2:
                SpawnPlatType(BarrierPlatform);
                break;
            case 3:
                SpawnPlatType(BarrierMovingPlatform);
                BarrierMovingPlatSpeed +=1;
                break;
            default:
                SpawnPlatType(Platform);
                break;
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
        RangesAvailSpawn = 1;
        DistanceOffsetMaxAug = DistanceOffsetMax;

    }

}
