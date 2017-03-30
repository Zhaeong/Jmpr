using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GuiController : MonoBehaviour {

    private GameController GC;

    private bool StartMenu, ScoreMenu, SubmitScoreMenu;

    private string stringToEdit = "enter";

    // Use this for initialization
    void Start () {
        GC = GameObject.FindGameObjectWithTag("Player").GetComponent<GameController>();

        StartMenu = true;
        ScoreMenu = false;
        SubmitScoreMenu = false;
    }
	
	// Update is called once per frame
	void Update () {

		
	}

    void OnGUI()
    {
        if (!GC.GameStart && StartMenu)
        {
            if (GUI.Button(new Rect(Screen.width / 2, Screen.height / 2, 150, 100), "Start"))
            {
                Time.timeScale = 1;
                Invoke("StartGameFunc", 0.1f);
                
            }

            if (GUI.Button(new Rect(Screen.width / 2, Screen.height / 2 + 110, 150, 100), "Scores"))
            {
                ScoreMenu = true;
                StartMenu = false;
                SubmitScoreMenu = false;
            }

        }

        if (ScoreMenu)
        {        
            if (GUI.Button(new Rect(Screen.width / 2, Screen.height / 2, 150, 100), "Submit Score"))
            {
                StartMenu = false;
                ScoreMenu = false;
                SubmitScoreMenu = true;
            }

            if (GUI.Button(new Rect(Screen.width / 2 , Screen.height / 2 + 110, 150, 100), "Global Leaderboard"))
            {
                GC.GetLeaderboard();
            }

            if (GUI.Button(new Rect(Screen.width / 2, Screen.height / 2 + 220, 150, 100), "Back"))
            {
                StartMenu = true;
                ScoreMenu = false;
                SubmitScoreMenu = false;

            }

        }

        if (SubmitScoreMenu)
        {
            stringToEdit = GUI.TextField(new Rect(Screen.width / 2, Screen.height / 2, 200, 20), stringToEdit, 25);

            if (GUI.Button(new Rect(Screen.width / 2, Screen.height / 2 + 110, 150, 100), "Submit"))
            {

                GC.SubmitScore(stringToEdit);


            }

            if (GUI.Button(new Rect(Screen.width / 2, Screen.height / 2 + 220, 150, 100), "Back"))
            {
                ScoreMenu = true;
                StartMenu = false;                
                SubmitScoreMenu = false;

            }

        }




    }

    private void SubmitName(string arg0)
    {
        Debug.Log(arg0);
    }

    void StartGameFunc()
    {
        GC.resumeGame();        
    }
}
