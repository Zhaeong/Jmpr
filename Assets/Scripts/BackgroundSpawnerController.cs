using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSpawnerController : MonoBehaviour {

    public float ObjGapSize;
    public GameObject BackgroundObj;

    public Transform topLeft, topRight, botLeft, botRight;

    private Vector3 Objsize;

    private float TL_x, TL_z;
    private float TR_x, TR_z;
    private float BL_x, BL_z;
    private float BR_x, BR_z;

    public float y_offset;

    private GameObject player;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y - y_offset, player.transform.position.z);

        Objsize = BackgroundObj.GetComponent<Renderer>().bounds.size;

        TL_x = topLeft.position.x;
        TL_z = topLeft.position.z;

        TR_x = topRight.position.x;
        TR_z = topRight.position.z;

        BL_x = botLeft.position.x;
        BL_z = botLeft.position.z;

        BR_x = botRight.position.x;
        BR_z = botRight.position.z;

        float initial_z = TL_z;
        float initial_x = TL_x;

        for (float z = initial_z; z > BL_z; z -= Objsize.z + ObjGapSize)

        {
            for (float x = initial_x; x < TR_x; x += Objsize.x + ObjGapSize)
            {
                float DistanceScaled = Random.Range(1, 3);

                GameObject bckOBj = BackgroundObj;
                bckOBj.transform.localScale = new Vector3(1, DistanceScaled, 1);

                Instantiate(bckOBj, new Vector3(x, transform.position.y, z), Quaternion.Euler(0, 0, 0));
            }
        }

        




    }
	
	// Update is called once per frame
	void Update () {

        transform.position = new Vector3(player.transform.position.x, player.transform.position.y - y_offset, player.transform.position.z);
		
	}
}
