using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public int iScore;
    public bool onPlatform;
    public bool SpawnPlatform;


    // Use this for initialization
    void Start () {
        iScore = 0;
        onPlatform = false;
        SpawnPlatform = false;
    }
	
	// Update is called once per frame
	void Update () {



    }
    public void addScore()
    {
        iScore += 1;
    }
    public int getScore()
    {
        return iScore;
    }
}
