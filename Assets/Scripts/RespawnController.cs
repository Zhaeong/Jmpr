using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RespawnController : MonoBehaviour {

    private GameObject PlayerChar;

    private float RespawnHeight;
    private GameController GC;

    
    private Transform PlatformStartingTransform;


    // Use this for initialization
    void Start () {
        PlayerChar = GameObject.FindGameObjectWithTag("Player");
        //RespawnPoint = GameObject.FindGameObjectWithTag("RespawnPoint").transform;
        GC = PlayerChar.GetComponent<GameController>();
        RespawnHeight = -5;

    }
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(PlayerChar.transform.position.x, RespawnHeight, PlayerChar.transform.position.z);
	}
    void OnTriggerEnter(Collider other)
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        if (other.tag == "Player")
        {

            GC.pauseGame();
        }
        
    }




}
