using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GuiController : MonoBehaviour {

    public ArrayList LeaderboardScores;
    public Texture2D TextBckgrd;
    private GameController GC;

    private bool StartMenu, ScoreMenu, SubmitScoreMenu, LeaderboardMenu;

    private string stringToEdit = "enter name";

    private Vector2 scrollPosition = Vector2.zero;

    private GUIStyle style;

    // Use this for initialization
    void Start () {
        GC = GameObject.FindGameObjectWithTag("Player").GetComponent<GameController>();

        StartMenu = true;
        ScoreMenu = false;
        SubmitScoreMenu = false;
        LeaderboardMenu = false;


        //GuiStyle params
        style = new GUIStyle();
        style.fontSize = 40;
        style.border = new RectOffset(3, 3, 3, 3);
        style.normal.background = TextBckgrd;
        
        
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
            if (GUI.Button(new Rect(Screen.width / 2 - (Button_x_width/2), Screen.height / 2, Button_x_width, Button_y_width), "Start", style))
            {
                Time.timeScale = 1;
                Invoke("StartGameFunc", 0.1f);
                
            }

            if (GUI.Button(new Rect(Screen.width / 2 - (Button_x_width / 2), Screen.height / 2 + Button_y_width, Button_x_width, Button_y_width), "Scores", style))
            {
                ScoreMenu = true;
                StartMenu = false;
                SubmitScoreMenu = false;
                LeaderboardMenu = false;
            }

        }

        if (ScoreMenu)
        {        
            if (GUI.Button(new Rect(Screen.width / 2 - (Button_x_width / 2), Screen.height / 2, Button_x_width, Button_y_width), "Submit Score", style))
            {
                SubmitScoreMenu = true;
                StartMenu = false;
                ScoreMenu = false;
                LeaderboardMenu = false;
                
            }

            if (GUI.Button(new Rect(Screen.width / 2 - (Button_x_width / 2), Screen.height / 2 + Button_y_width, Button_x_width, Button_y_width), "Leaderboard", style))
            {
                LeaderboardMenu = true;
                ScoreMenu = false;
                SubmitScoreMenu = false;
                StartMenu = false;


               
            }

            if (GUI.Button(new Rect(Screen.width / 2 - (Button_x_width / 2), Screen.height / 2 + (Button_y_width *2), Button_x_width, Button_y_width), "Back", style))
            {
                StartMenu = true;
                ScoreMenu = false;
                SubmitScoreMenu = false;

            }

        }

        if (SubmitScoreMenu)
        {
            int textSubmitbox = 100;

            stringToEdit = GUI.TextField(new Rect(Screen.width / 2 - (Button_x_width / 2), Screen.height / 2, 200, textSubmitbox), stringToEdit, 25, style);

            

            if (GUI.Button(new Rect(Screen.width / 2 - (Button_x_width / 2), Screen.height / 2 + textSubmitbox + 20, Button_x_width, Button_y_width), "Submit", style))
            {

                GC.SubmitScore(stringToEdit);


            }

            if (GUI.Button(new Rect(Screen.width / 2 - (Button_x_width / 2), Screen.height / 2 + (textSubmitbox * 2) + 40, Button_x_width, Button_y_width), "Back", style))
            {
                ScoreMenu = true;
                StartMenu = false;                
                SubmitScoreMenu = false;

            }

        }

        if (LeaderboardMenu)
        {
            GC.GetLeaderboard();

            int Screen_x_name_width = 300;
            int Screen_x_name_height = 50;

            int Screen_x_scor_width = 50;
            int Screen_x_scor_height = 50;

            int Screen_x_name_posit = 0;
            int Screen_x_score_posit = Screen_x_name_width + 10;

            

            int Screen_y = 0;
            
            if (LeaderboardScores != null)
            {                

                int numScores = LeaderboardScores.Count;
                scrollPosition = GUI.BeginScrollView(new Rect(Screen.width / 2 - (Button_x_width / 2), Screen.height / 2, 400, 200), scrollPosition, new Rect(0, 0, 400, numScores*50),false,true);

                for (int i = LeaderboardScores.Count -1 ; i >= 0; i--)
                {
                    SendScoreObj SSO = (SendScoreObj)LeaderboardScores[i];

                    

                    GUI.Label(new Rect(Screen_x_name_posit, Screen_y, Screen_x_name_width, Screen_x_name_height), SSO.PersonAlias, style);
                    GUI.Label(new Rect(Screen_x_score_posit, Screen_y, Screen_x_scor_width, Screen_x_scor_height), SSO.Score.ToString(), style);
                    Screen_y += 50;
                }
                GUI.EndScrollView();

            }


            if (GUI.Button(new Rect(Screen.width / 2 - (Button_x_width / 2), Screen.height / 2 + 300, Button_x_width, Button_y_width), "Back", style))
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
