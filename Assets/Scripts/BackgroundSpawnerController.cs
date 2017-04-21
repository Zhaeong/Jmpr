using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSpawnerController : MonoBehaviour {

    public float ObjGapSize;
    public GameObject BackgroundObj;

    public float SpawnSize_x, SpawnSize_z;
    
    public Material BlockMaterial;

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

    private int Score;

    public List<GameObject> PillarsA, PillarsB;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");

        y_spawnposition = player.transform.position.y - y_offset;
        transform.position = new Vector3(player.transform.position.x, y_spawnposition, player.transform.position.z);

        Objsize = BackgroundObj.GetComponent<Renderer>().bounds.size;

        Vector3 SpawnerPosit = transform.position;

        PillarsA = new List<GameObject>();
        PillarsB = new List<GameObject>();

        SpawnBlocks(SpawnerPosit, "BckListA", PillarsA);

        SpawnBlocks(new Vector3(SpawnerPosit.x, SpawnerPosit.y, SpawnerPosit.z + (2* SpawnSize_z)), "BckListB", PillarsB);


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
                MoveBlocks(spawnPosition, PillarsA);

                SpawnA = false;
            }
            else
            {
                MoveBlocks(spawnPosition, PillarsB);
                SpawnA = true;
            }

            z_boundary = newPosition.z + SpawnSize_z*2;
        }
        
		
	}

    void SpawnBlocks(Vector3 spawnPosit, string objtag, List<GameObject> PillarsList)
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

        int PillerNumber = 0;

        for (float z = initial_z; z > BL_z; z -= Objsize.z + ObjGapSize)

        {
            for (float x = initial_x; x < TR_x; x += Objsize.x + ObjGapSize)
            {
                float DistanceScaled = Random.Range(1.0f, 2.0f);

                float DistanceScaled_y = Random.Range(1.0f, 3.0f);

                GameObject bckOBj = BackgroundObj;
                bckOBj.name = "Piller" + PillerNumber;
                PillerNumber++;
                bckOBj.transform.localScale = new Vector3(DistanceScaled, DistanceScaled_y, DistanceScaled);
                
                bckOBj.tag = objtag;
                Objsize = BackgroundObj.GetComponent<Renderer>().bounds.size;

                PillarsList.Add(Instantiate(bckOBj, new Vector3(x, spawnPosit.y, z), Quaternion.Euler(-90, 0, 0)));
                
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

    //Move the blocks to location to improve performance
    void MoveBlocks(Vector3 spawnPosit, List<GameObject> PillarsList)
    {
        GetBlockMaterial();

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

        int ObjNum = 0;

        for (float z = initial_z; z > BL_z; z -= Objsize.z + ObjGapSize)

        {
            for (float x = initial_x; x < TR_x; x += Objsize.x + ObjGapSize)
            {
                if (ObjNum < PillarsList.Count)
                {

                    float randomY = Random.Range(spawnPosit.y -1.0f, spawnPosit.y + 1.0f);

                    Objsize = PillarsList[ObjNum].GetComponent<Renderer>().bounds.size;

                    PillarsList[ObjNum].transform.position = new Vector3(x, randomY, z);

                    PillarsList[ObjNum].GetComponent<Renderer>().material = BlockMaterial;
                    ObjNum++;
                }                
            }
        }
    }

    public void GetBlockMaterial()
    {
        if (GameObject.FindGameObjectWithTag("Player").GetComponent<GameController>().getScore() > 3)
        {

            BlockMaterial.color = new Color(1, 0, 0);
        }
    }


    public void Restart()
    {
        DeleteBlocks("BckListA");
        DeleteBlocks("BckListB");

        PillarsA = new List<GameObject>();
        PillarsB = new List<GameObject>();

        player = GameObject.FindGameObjectWithTag("Player");

        y_spawnposition = player.transform.position.y - y_offset;
        transform.position = new Vector3(player.transform.position.x, y_spawnposition, player.transform.position.z);

        Objsize = BackgroundObj.GetComponent<Renderer>().bounds.size;

        Vector3 SpawnerPosit = transform.position;

        SpawnBlocks(SpawnerPosit, "BckListA", PillarsA);

        SpawnBlocks(new Vector3(SpawnerPosit.x, SpawnerPosit.y, SpawnerPosit.z + (2 * SpawnSize_z)), "BckListB", PillarsB);

        z_boundary = SpawnerPosit.z + SpawnSize_z;

        SpawnA = true;

    }
}
