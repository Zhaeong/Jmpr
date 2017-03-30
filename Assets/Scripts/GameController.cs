using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using Firebase;
using Firebase.Unity.Editor;


public class GameController : MonoBehaviour {

    public int iScore;
    public bool onPlatform;
    public bool SpawnPlatform;
    public bool GameStart;

    private PlatformSpawner PC;


    // Use this for initialization
    void Start () {
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://jmpr-909dd.firebaseio.com/");
        iScore = 0;
        onPlatform = false;
        SpawnPlatform = false;
        GameStart = false;

        PC = GameObject.FindGameObjectWithTag("PlatformSpawner").GetComponent<PlatformSpawner>();
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

    public void pauseGame()
    {
        GameStart = false;
        Time.timeScale = 0;
    }

    public void resumeGame()
    {
        GameStart = true;
        iScore = 0;
        PC.RespawnGameStart(); 
    }
}
