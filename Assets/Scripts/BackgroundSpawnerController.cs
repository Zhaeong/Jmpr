using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSpawnerController : MonoBehaviour {

    public float ObjGapSize;
    public GameObject BackgroundObj;

    public float SpawnSize_x, SpawnSize_z;

    private Vector3 Objsize;

    private float TL_x, TL_z;
    private float TR_x, TR_z;
    private float BL_x, BL_z;
    private float BR_x, BR_z;

    public float y_offset;

    private float y_spawnposition;

    private float z_boundary;

    private GameObject player;

    private bool SpawnA;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");

        y_spawnposition = player.transform.position.y - y_offset;
        transform.position = new Vector3(player.transform.position.x, y_spawnposition, player.transform.position.z);

        Objsize = BackgroundObj.GetComponent<Renderer>().bounds.size;

        Vector3 SpawnerPosit = transform.position;

        SpawnBlocks(SpawnerPosit, "BckListA");

        SpawnBlocks(new Vector3(SpawnerPosit.x, SpawnerPosit.y, SpawnerPosit.z + (2* SpawnSize_z)), "BckListB");

        z_boundary = SpawnerPosit.z + SpawnSize_z;

        SpawnA = true;
    }
	
	// Update is called once per frame
	void Update () {

        if (player.transform.position.z >= z_boundary)
        {
            Vector3 newPosition = new Vector3(player.transform.position.x, y_spawnposition, z_boundary);

            gameObject.transform.position = newPosition;
            

            Vector3 spawnPosition = new Vector3(newPosition.x, newPosition.y, newPosition.z + (3 * SpawnSize_z) );

            if (SpawnA)
            {
                DeleteBlocks("BckListA");
                SpawnBlocks(spawnPosition, "BckListA");
                
                SpawnA = false;
            }
            else
            {
                DeleteBlocks("BckListB");
                SpawnBlocks(spawnPosition, "BckListB");
                
                SpawnA = true;
            }

            z_boundary = newPosition.z + SpawnSize_z*2;


        }
        
		
	}

    void SpawnBlocks(Vector3 spawnPosit, string objtag)
    {

        TL_x = spawnPosit.x - SpawnSize_x;
        TL_z = spawnPosit.z + SpawnSize_z;

        TR_x = spawnPosit.x + SpawnSize_x;
        TR_z = spawnPosit.z + SpawnSize_z;

        BL_x = spawnPosit.x - SpawnSize_x;
        BL_z = spawnPosit.z - SpawnSize_z;

        BR_x = spawnPosit.x + SpawnSize_x;
        BR_z = spawnPosit.z - SpawnSize_z;


        float initial_z = TL_z;
        float initial_x = TL_x;

        for (float z = initial_z; z > BL_z; z -= Objsize.z + ObjGapSize)

        {
            for (float x = initial_x; x < TR_x; x += Objsize.x + ObjGapSize)
            {
                float DistanceScaled = Random.Range(1.0f, 2.0f);

                float DistanceScaled_y = Random.Range(1.0f, 3.0f);

                GameObject bckOBj = BackgroundObj;
                bckOBj.transform.localScale = new Vector3(DistanceScaled, DistanceScaled_y, DistanceScaled);
                
                bckOBj.tag = objtag;
                Objsize = BackgroundObj.GetComponent<Renderer>().bounds.size;
                Instantiate(bckOBj, new Vector3(x, spawnPosit.y, z), Quaternion.Euler(-90, 0, 0));
            }
        }

    }

    void DeleteBlocks(string objtag)
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag(objtag);

        for (var i = 0; i < gameObjects.Length; i++)
        {
            Destroy(gameObjects[i]);
        }
            
    }

    public void Restart()
    {
        DeleteBlocks("BckListA");
        DeleteBlocks("BckListB");


        player = GameObject.FindGameObjectWithTag("Player");

        y_spawnposition = player.transform.position.y - y_offset;
        transform.position = new Vector3(player.transform.position.x, y_spawnposition, player.transform.position.z);

        Objsize = BackgroundObj.GetComponent<Renderer>().bounds.size;

        Vector3 SpawnerPosit = transform.position;

        SpawnBlocks(SpawnerPosit, "BckListA");

        SpawnBlocks(new Vector3(SpawnerPosit.x, SpawnerPosit.y, SpawnerPosit.z + (2 * SpawnSize_z)), "BckListB");

        z_boundary = SpawnerPosit.z + SpawnSize_z;

        SpawnA = true;

    }
}
