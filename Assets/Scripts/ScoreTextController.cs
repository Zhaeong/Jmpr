using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreTextController : MonoBehaviour {


    private TextMesh TM;
    private GameObject Player;


    // Use this for initialization
    void Start () {
        Player = GameObject.FindGameObjectWithTag("Player");
        TM = gameObject.GetComponent<TextMesh>();
        TM.text = Player.GetComponent<GameController>().getScore().ToString();
    }
	
	// Update is called once per frame
	void Update () {
        
    }
    
}
