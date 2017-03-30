using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuiController : MonoBehaviour {

    private GameController GC;

	// Use this for initialization
	void Start () {
        GC = GameObject.FindGameObjectWithTag("Player").GetComponent<GameController>();
    }
	
	// Update is called once per frame
	void Update () {

		
	}

    void OnGUI()
    {
        if (!GC.GameStart)
        {
            if (GUI.Button(new Rect(Screen.width / 2, Screen.height / 2, 150, 100), "Start"))
            {
                Time.timeScale = 1;
                Invoke("StartGameFunc", 0.1f);
                
            }
                
        }
        

    }

    void StartGameFunc()
    {
        GC.resumeGame();        
    }
}
