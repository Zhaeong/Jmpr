using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GuiController : MonoBehaviour {

    public ArrayList LeaderboardScores;
    private GameController GC;

    private bool StartMenu, ScoreMenu, SubmitScoreMenu, LeaderboardMenu;

    private string stringToEdit = "enter";

    private Vector2 scrollPosition = Vector2.zero;

    // Use this for initialization
    void Start () {
        GC = GameObject.FindGameObjectWithTag("Player").GetComponent<GameController>();

        StartMenu = true;
        ScoreMenu = false;
        SubmitScoreMenu = false;
        LeaderboardMenu = false;
    }
	
	// Update is called once per frame
	void Update () {

		
	}

    void OnGUI()
    {
        int Button_x_width = 300;
        int Button_y_width = 150;
        if (!GC.GameStart && StartMenu)
        {
            if (GUI.Button(new Rect(Screen.width / 2, Screen.height / 2, Button_x_width, Button_y_width), "Start"))
            {
                Time.timeScale = 1;
                Invoke("StartGameFunc", 0.1f);
                
            }

            if (GUI.Button(new Rect(Screen.width / 2, Screen.height / 2 + Button_y_width, Button_x_width, Button_y_width), "Scores"))
            {
                ScoreMenu = true;
                StartMenu = false;
                SubmitScoreMenu = false;
                LeaderboardMenu = false;
            }

        }

        if (ScoreMenu)
        {        
            if (GUI.Button(new Rect(Screen.width / 2, Screen.height / 2, Button_x_width, Button_y_width), "Submit Score"))
            {
                SubmitScoreMenu = true;
                StartMenu = false;
                ScoreMenu = false;
                LeaderboardMenu = false;
                
            }

            if (GUI.Button(new Rect(Screen.width / 2 , Screen.height / 2 + Button_y_width, Button_x_width, Button_y_width), "Leaderboard"))
            {
                LeaderboardMenu = true;
                ScoreMenu = false;
                SubmitScoreMenu = false;
                StartMenu = false;


               
            }

            if (GUI.Button(new Rect(Screen.width / 2, Screen.height / 2 + (Button_y_width *2), Button_x_width, Button_y_width), "Back"))
            {
                StartMenu = true;
                ScoreMenu = false;
                SubmitScoreMenu = false;

            }

        }

        if (SubmitScoreMenu)
        {
            stringToEdit = GUI.TextField(new Rect(Screen.width / 2, Screen.height / 2, 200, 20), stringToEdit, 25);

            if (GUI.Button(new Rect(Screen.width / 2, Screen.height / 2 + Button_y_width, Button_x_width, Button_y_width), "Submit"))
            {

                GC.SubmitScore(stringToEdit);


            }

            if (GUI.Button(new Rect(Screen.width / 2, Screen.height / 2 + (Button_y_width*2), Button_x_width, Button_y_width), "Back"))
            {
                ScoreMenu = true;
                StartMenu = false;                
                SubmitScoreMenu = false;

            }

        }

        if (LeaderboardMenu)
        {
            GC.GetLeaderboard();

            int Screen_x_name = 0;
            int Screen_x_score = 100;
            int Screen_y = 0;
            
            if (LeaderboardScores != null)
            {                

                int numScores = LeaderboardScores.Count;
                scrollPosition = GUI.BeginScrollView(new Rect(Screen.width / 2, Screen.height / 2, 400, 300), scrollPosition, new Rect(0, 0, 400, numScores*50));

                for (int i = LeaderboardScores.Count -1 ; i >= 0; i--)
                {
                    SendScoreObj SSO = (SendScoreObj)LeaderboardScores[i];

                    

                    GUI.Label(new Rect(Screen_x_name, Screen_y, 150, 50), SSO.PersonAlias);
                    GUI.Label(new Rect(Screen_x_score, Screen_y, 25, 50), SSO.Score.ToString());
                    Screen_y += 20;
                }
                GUI.EndScrollView();

            }


            if (GUI.Button(new Rect(Screen.width / 2, Screen.height / 2 + 300, Button_x_width, Button_y_width), "Back"))
            {
                ScoreMenu = true;
                StartMenu = false;
                SubmitScoreMenu = false;
                LeaderboardMenu = false;

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
