using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RespawnController : MonoBehaviour {

    public Transform RespawnPoint;
    public GameObject PlayerChar;

    public float RespawnHeight;
    private GameController GC;

    
    private Transform PlatformStartingTransform;


    // Use this for initialization
    void Start () {
        GC = GameObject.FindGameObjectWithTag("Player").GetComponent<GameController>();

    }
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(PlayerChar.transform.position.x, RespawnHeight, PlayerChar.transform.position.z);
	}
    void OnTriggerEnter(Collider other)
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        GC.pauseGame();
    }




}
