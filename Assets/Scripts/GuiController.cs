using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GuiController : MonoBehaviour {

    public ArrayList LeaderboardScores;
    public Texture2D TextBckgrd;
    public GUISkin CustomGS;
    public bool DebugStats;

    private GameController GC;

    private PlatformSpawner PC;

    private bool StartMenu, ScoreMenu, SubmitScoreMenu, LeaderboardMenu;

    private string stringToEdit = "enter name";

    private Vector2 scrollPosition = Vector2.zero;

    private GUIStyle style;

    // Use this for initialization
    void Start () {
        GC = GameObject.FindGameObjectWithTag("Player").GetComponent<GameController>();
        PC = GameObject.FindGameObjectWithTag("PlatformSpawner").GetComponent<PlatformSpawner>();

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

        if (DebugStats)
        {
            GUI.Label(new Rect(0, 0, Screen.width, 100), string.Format("{0:N3}", "MovingPlat Speed: " + PC.MovingPlatSpeed), CustomGS.GetStyle("label"));
            GUI.Label(new Rect(0, 100, Screen.width, 100), string.Format("{0:N3}", "BarrierPlat Speed: " + PC.BarrierMovingPlatSpeed), CustomGS.GetStyle("label"));
        }  

        int Button_x_width = Screen.width / 2;
        int Button_y_width = Screen.height / 9;

        if (!GC.GameStart && StartMenu)
        {
            if (GUI.Button(new Rect(Screen.width / 2 - (Button_x_width/2), Screen.height / 2, Button_x_width, Button_y_width), "Start", CustomGS.GetStyle("button")))
            {
                Time.timeScale = 1;
                Invoke("StartGameFunc", 0.1f);
                
            }

            if (GUI.Button(new Rect(Screen.width / 2 - (Button_x_width / 2), Screen.height / 2 + Button_y_width, Button_x_width, Button_y_width), "Scores", CustomGS.GetStyle("button")))
            {
                ScoreMenu = true;
                StartMenu = false;
                SubmitScoreMenu = false;
                LeaderboardMenu = false;
            }

        }

        if (ScoreMenu)
        {        
            if (GUI.Button(new Rect(Screen.width / 2 - (Button_x_width / 2), Screen.height / 2, Button_x_width, Button_y_width), "Submit Score", CustomGS.GetStyle("button")))
            {
                SubmitScoreMenu = true;
                StartMenu = false;
                ScoreMenu = false;
                LeaderboardMenu = false;
                
            }

            if (GUI.Button(new Rect(Screen.width / 2 - (Button_x_width / 2), Screen.height / 2 + Button_y_width, Button_x_width, Button_y_width), "Leaderboard", CustomGS.GetStyle("button")))
            {
                LeaderboardMenu = true;
                ScoreMenu = false;
                SubmitScoreMenu = false;
                StartMenu = false;               
            }

            if (GUI.Button(new Rect(Screen.width / 2 - (Button_x_width / 2), Screen.height / 2 + (Button_y_width *2), Button_x_width, Button_y_width), "Back", CustomGS.GetStyle("button")))
            {
                StartMenu = true;
                ScoreMenu = false;
                SubmitScoreMenu = false;
            }

        }

        if (SubmitScoreMenu)
        {
            stringToEdit = GUI.TextField(new Rect(Screen.width / 2 - (Button_x_width / 2), Screen.height / 2, Button_x_width, Button_y_width), stringToEdit, 25, CustomGS.GetStyle("textfield"));
            
            if (GUI.Button(new Rect(Screen.width / 2 - (Button_x_width / 2), Screen.height / 2 + Button_y_width + 20, Button_x_width, Button_y_width), "Submit", CustomGS.GetStyle("button")))
            {
                GC.SubmitScore(stringToEdit);
            }

            if (GUI.Button(new Rect(Screen.width / 2 - (Button_x_width / 2), Screen.height / 2 + (Button_y_width * 2) + 40, Button_x_width, Button_y_width), "Back", CustomGS.GetStyle("button")))
            {
                ScoreMenu = true;
                StartMenu = false;                
                SubmitScoreMenu = false;
            }
        }

        if (LeaderboardMenu)
        {
            GC.GetLeaderboard();

            float Screen_x_name_width = Screen.width / 2;
            int Screen_x_name_height = Screen.height / 12;

            int Screen_x_scor_width = Screen.width / 8;
            int Screen_x_scor_height = Screen.height / 12;

            int Screen_x_name_posit = 0;
            float Screen_x_score_posit = Screen_x_name_width + 10;            

            int Screen_y = 0;
            
            if (LeaderboardScores != null)
            {           
                int numScores = LeaderboardScores.Count;

                float scrollwidth = Screen.width / 1.5f;
                int scrollheight = Screen.height / 4;
                
                scrollPosition = GUI.BeginScrollView(
                    new Rect(Screen.width / 2 - (scrollwidth / 2), Screen.height / 2 - (scrollheight / 2), scrollwidth, scrollheight), 
                    scrollPosition, 
                    new Rect(0, 0, 400, numScores*50),
                    false,
                    true, 
                    CustomGS.GetStyle("horizontalscrollbar"), 
                    CustomGS.GetStyle("verticalscrollbar"));


                GUI.skin.verticalScrollbarThumb.fixedWidth = CustomGS.GetStyle("verticalscrollbar").fixedWidth;

                for (int i = LeaderboardScores.Count -1 ; i >= 0; i--)
                {
                    SendScoreObj SSO = (SendScoreObj)LeaderboardScores[i];
                    
                    GUI.Label(new Rect(Screen_x_name_posit, Screen_y, Screen_x_name_width, Screen_x_name_height), SSO.PersonAlias, CustomGS.GetStyle("label"));
                    GUI.Label(new Rect(Screen_x_score_posit, Screen_y, Screen_x_scor_width, Screen_x_scor_height), SSO.Score.ToString(), CustomGS.GetStyle("label"));
                    Screen_y += 50;
                }
                GUI.EndScrollView();
            }

            if (GUI.Button(new Rect(Screen.width / 2 - (Button_x_width / 2), Screen.height / 2 + 300, Button_x_width, Button_y_width), "Back", CustomGS.GetStyle("button")))
            {
                ScoreMenu = true;
                StartMenu = false;
                SubmitScoreMenu = false;
                LeaderboardMenu = false;
            }
        }
    }

    void StartGameFunc()
    {
        GC.resumeGame();        
    }
}
