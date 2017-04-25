using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GuiController : MonoBehaviour {

    public ArrayList LeaderboardScores;
    public Texture2D TextBckgrd;
    public GUISkin CustomGS;
    public bool DebugMenu;

    private string stringToEdit = "enter name";

    

    //Messages
    public bool showMessage;
    public string MsgPrompt;

    //Projectile Images
    public Texture IcoSphereImg;
    public Texture ColoredBallImg;
    public Texture SpikedSphereImg;
    public Texture AppleImg;

    private GameController GC;

    private PlatformSpawner PC;

    private bool StartMenu, ScoreMenu, SubmitScoreMenu, LeaderboardMenu, ProjectileMenu, PurchasingMenu;

    //Purchasing menu variables
    public int itemPrice;
    private string itemLabel, itemToPurchase;
    private Texture itemImg;

    private Vector2 scrollPosition = Vector2.zero;

    private GUIStyle style;

    

    private int Button_width = Screen.width / 2;
    private int Button_height = Screen.height / 9;



    // Use this for initialization
    void Start () {
        GC = GameObject.FindGameObjectWithTag("Player").GetComponent<GameController>();
        PC = GameObject.FindGameObjectWithTag("PlatformSpawner").GetComponent<PlatformSpawner>();

        TurnOffEverything();
        StartMenu = true;


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

        

        if (DebugMenu)
        {
            GUI.Label(new Rect(0, 0, Screen.width, 100), string.Format("{0:N3}", "MovingPlat Speed: " + PC.MovingPlatSpeed), CustomGS.GetStyle("label"));
            GUI.Label(new Rect(0, 100, Screen.width, 100), string.Format("{0:N3}", "BarrierPlat Speed: " + PC.BarrierMovingPlatSpeed), CustomGS.GetStyle("label"));

            if (GUI.Button(new Rect(0, 200, Button_width, Button_height), "Delete PlayerPrefs", CustomGS.GetStyle("button")))
            {
                PlayerPrefs.DeleteAll();

            }

            if (GUI.Button(new Rect(0, 200 + Button_height, Button_width, Button_height), "Give points", CustomGS.GetStyle("button")))
            {
                PlayerPrefs.SetInt("DeviceScorePouch", 1000);

            }

        }

        if (showMessage)
        {
            GUI.Label(new Rect(0, Screen.height / 2 - (Screen.height / 4 / 2) - 110, Screen.width, 100), MsgPrompt, CustomGS.GetStyle("MsgText"));
        }

        if (!GC.GameStart)
        {
            GUI.Label(new Rect(0, 0, Screen.width, 60), "High Score: " + GC.GetDeviceHighScore(), CustomGS.GetStyle("label"));
            GUI.Label(new Rect(0, 60, Screen.width, 60), "You Scored: " + (GC.getScore() - 1), CustomGS.GetStyle("label"));

            if (PlayerPrefs.HasKey("DeviceScorePouch"))
            {
                int ScorePouch = PlayerPrefs.GetInt("DeviceScorePouch");
                GUI.Label(new Rect(0, 120, Screen.width, 60), "Points: " + ScorePouch, CustomGS.GetStyle("label"));
            }
            else
            {
                GUI.Label(new Rect(0, 120, Screen.width, 60), "Points: " + 0, CustomGS.GetStyle("label"));
            }
        }
        else
        {
            if (GUI.Button(new Rect(0, 0, Screen.width / 8, Screen.width / 8), "", CustomGS.GetStyle("restartbutton")))
            {
                Time.timeScale = 1;
                Invoke("StartGameFunc", 0.1f);
            }
        }


        if (!GC.GameStart && StartMenu)
        {  

            if (GUI.Button(new Rect(Screen.width / 2 - (Button_width / 2), Screen.height / 2, Button_width, Button_height), "Start", CustomGS.GetStyle("button")))
            {
                Time.timeScale = 1;
                Invoke("StartGameFunc", 0.1f);
                
            }

            if (GUI.Button(new Rect(Screen.width / 2 - (Button_width / 2), Screen.height / 2 + Button_height + 10, Button_width, Button_height), "Scores", CustomGS.GetStyle("button")))
            {

                TurnOffEverything();
                ScoreMenu = true;
            }
            if (GUI.Button(new Rect(Screen.width / 2 - (Button_width / 2), Screen.height / 2 + (Button_height + 10) * 2, Button_width, Button_height), "Characters", CustomGS.GetStyle("button")))
            {
                TurnOffEverything();
                ProjectileMenu = true;                
            }

        }

        if (ScoreMenu)
        {        
            if (GUI.Button(new Rect(Screen.width / 2 - (Button_width / 2), Screen.height / 2, Button_width, Button_height), "Submit Score", CustomGS.GetStyle("button")))
            {
                TurnOffEverything();
                SubmitScoreMenu = true;
            }

            if (GUI.Button(new Rect(Screen.width / 2 - (Button_width / 2), Screen.height / 2 + (Button_height + 10), Button_width, Button_height), "Leaderboard", CustomGS.GetStyle("button")))
            {
                TurnOffEverything();
                LeaderboardMenu = true;                             
            }

            if (GUI.Button(new Rect(Screen.width / 2 - (Button_width / 2), Screen.height / 2 + (Button_height + 10) * 2, Button_width, Button_height), "Back", CustomGS.GetStyle("button")))
            {
                TurnOffEverything();
                StartMenu = true;                
            }

        }

        if (SubmitScoreMenu)
        {
            stringToEdit = GUI.TextField(new Rect(Screen.width / 2 - (Button_width / 2), Screen.height / 2, Button_width, Button_height), stringToEdit, 25, CustomGS.GetStyle("textfield"));
            
            if (GUI.Button(new Rect(Screen.width / 2 - (Button_width / 2), Screen.height / 2 + Button_height + 20, Button_width, Button_height), "Submit", CustomGS.GetStyle("button")))
            {
                GC.SubmitScore(stringToEdit);
            }

            if (GUI.Button(new Rect(Screen.width / 2 - (Button_width / 2), Screen.height / 2 + (Button_height * 2) + 40, Button_width, Button_height), "Back", CustomGS.GetStyle("button")))
            {
                TurnOffEverything();
                ScoreMenu = true;                
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
                showMessage = false;
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

            if (GUI.Button(new Rect(Screen.width / 2 - (Button_width / 2), Screen.height / 2 + 300, Button_width, Button_height), "Back", CustomGS.GetStyle("button")))
            {
                TurnOffEverything();
                ScoreMenu = true;                
            }
        }

        if (ProjectileMenu)
        {
            int NumProjectiles = gameObject.transform.childCount;
            float scrollwidth = Screen.width / 1.2f;
            int scrollheight = Screen.height / 4;

            float Screen_x_name_width = Screen.width / 2;
            int Screen_x_name_height = Screen.height / 12;

            int Screen_img_width = Screen.width / 8;


            int Screen_y = 0;

            scrollPosition = GUI.BeginScrollView(
                    new Rect(Screen.width / 2 - (scrollwidth / 2), Screen.height / 2 - (scrollheight / 2), scrollwidth, scrollheight),
                    scrollPosition,
                    new Rect(0, 0, 400, (Screen_img_width + 10) * NumProjectiles),
                    false,
                    true,
                    CustomGS.GetStyle("horizontalscrollbar"),
                    CustomGS.GetStyle("verticalscrollbar"));

            //IcoSphereBlock
            AddProjectile("IcoSphere", "Ico ", IcoSphereImg, 0, Screen_y);

            //ColoredBallImg
            Screen_y = Screen_y + Screen_img_width + 10;
            AddProjectile("Beach", "Beach", ColoredBallImg, 50, Screen_y);

            //Spike Sphere
            Screen_y = Screen_y + Screen_img_width + 10;
            AddProjectile("SpikedSphere", "Spiked", SpikedSphereImg, 100, Screen_y);

            //Apple
            Screen_y = Screen_y + Screen_img_width + 10;
            AddProjectile("Apple", "Apple", AppleImg, 500, Screen_y);            

            GUI.EndScrollView();

            if (GUI.Button(new Rect(Screen.width / 2 - (Button_width / 2), Screen.height / 2 + (Button_height * 2), Button_width, Button_height), "Back", CustomGS.GetStyle("button")))
            {
                TurnOffEverything();
                StartMenu = true;
            }

        }

        if (PurchasingMenu)
        {
            float Label_width = Screen.width / 1.2f;

            float Screen_img_width = Label_width / 4;

            GUI.DrawTexture(new Rect(Screen.width / 2 - (Label_width * ((1 / 2f))), Screen.height / 2 - 10, Screen_img_width, Screen_img_width), itemImg, ScaleMode.ScaleToFit, true);

            GUI.Label(new Rect(Screen.width / 2 - (Label_width * ((1 / 2f))) + Screen_img_width, Screen.height / 2 - 10, Label_width * (3/4f), Screen_img_width), "Purchase " + itemLabel + " for "+ itemPrice +"?", CustomGS.GetStyle("itemlabel"));

            if (GUI.Button(new Rect(Screen.width / 2 - (Button_width / 2), Screen.height / 2 + Screen_img_width, Button_width, Button_height), "Purchase", CustomGS.GetStyle("button")))
            {
                if (GC.PurchaseProjectile(itemToPurchase, itemPrice))
                {
                    TurnOffEverything();
                    ProjectileMenu = true;
                }                
            }

            if (GUI.Button(new Rect(Screen.width / 2 - (Button_width / 2), Screen.height / 2 + (Screen_img_width) *2 + 10, Button_width, Button_height), "Back", CustomGS.GetStyle("button")))
            {
                TurnOffEverything();
                ProjectileMenu = true;
            }
        }
    }

    void StartGameFunc()
    {
        GC.resumeGame();        
    }

    void TurnOffEverything()
    {
        SubmitScoreMenu = false;
        ProjectileMenu = false;
        StartMenu = false;
        ScoreMenu = false;
        LeaderboardMenu = false;
        PurchasingMenu = false;

    }

    //Shows the message on screen for duration, then stops
    public void SetShortMessage(string Message, float duration)
    {
        showMessage = true;
        MsgPrompt = Message;
        Invoke("MsgOff", duration);        
    }

    void MsgOff()
    {
        showMessage = false;
    }

    //
    private void AddProjectile(string ProjectileKey, string ProjectileName, Texture ProjectileImg, int ProjectileCost, float screen_y)
    {

        float Screen_x_name_width = Screen.width / 2;
        int Screen_x_name_height = Screen.height / 12;

        int Screen_img_width = Screen.width / 8;

        int Screen_x_name_posit = 0;

        if (PlayerPrefs.HasKey(ProjectileKey))
        {
            string priceString = "";

            GUI.DrawTexture(new Rect(Screen_x_name_posit, screen_y, Screen_img_width, Screen_img_width), ProjectileImg, ScaleMode.ScaleToFit, true);
            if (GUI.Button(new Rect(Screen_x_name_posit + Screen_img_width, screen_y, Screen_x_name_width, Screen_img_width), ProjectileName, CustomGS.GetStyle("button")))
            {

                if (PlayerPrefs.GetInt(ProjectileKey) == 1)
                {
                    GC.ChangePlayerModel(ProjectileKey);
                    SetShortMessage("Changed to " + ProjectileName, 2);
                }
                else
                {
                    itemLabel = ProjectileName;
                    itemToPurchase = ProjectileKey;
                    itemImg = ProjectileImg;
                    itemPrice = ProjectileCost;
                    TurnOffEverything();
                    PurchasingMenu = true;
                }
            }

            if (PlayerPrefs.GetInt(ProjectileKey) == 0)
            {
                priceString = ProjectileCost.ToString();
            }

            GUI.Label(new Rect(Screen_x_name_posit + Screen_img_width + Screen_x_name_width, screen_y, Screen_x_name_width, Screen_img_width), priceString, CustomGS.GetStyle("UnlockModelButton"));
        }
        else
        {
            if (ProjectileKey == "IcoSphere")
            {
                PlayerPrefs.SetInt(ProjectileKey, 1);
            }
            else
            {
                PlayerPrefs.SetInt(ProjectileKey, 0);
            }
            
        }


    }
}
