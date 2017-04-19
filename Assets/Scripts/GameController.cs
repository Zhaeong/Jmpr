using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;


public class GameController : MonoBehaviour {

    private DatabaseReference reference;
    public int iScore;
    public bool onPlatform;
    public bool SpawnPlatform;
    public bool GameStart;

    public string UserId;

    private PlatformSpawner PS;
    private GuiController GC;
    private BackgroundSpawnerController BSC;


    // Use this for initialization
    void Start () {
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://jmpr-909dd.firebaseio.com/");
        reference = FirebaseDatabase.DefaultInstance.RootReference;

        iScore = 0;
        onPlatform = false;
        SpawnPlatform = false;
        GameStart = false;

        PS = GameObject.FindGameObjectWithTag("PlatformSpawner").GetComponent<PlatformSpawner>();
        GC = GameObject.FindGameObjectWithTag("Player").GetComponent<GuiController>();
        BSC = GameObject.FindGameObjectWithTag("BackgroundSpawner").GetComponent<BackgroundSpawnerController>();

        GenerateUserId();
        
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

        int DeviceHighScore = GetDeviceHighScore();
        if (DeviceHighScore < iScore -1 )
        {
            PlayerPrefs.SetInt("DeviceHighScore", iScore - 1);
        }

        if (PlayerPrefs.HasKey("DeviceScorePouch"))
        {
            int ScorePouch = PlayerPrefs.GetInt("DeviceScorePouch");
            PlayerPrefs.SetInt("DeviceScorePouch", ScorePouch +=iScore - 1);
        }
        else
        {
            PlayerPrefs.SetInt("DeviceScorePouch", iScore - 1);
        }
    }

    public void resumeGame()
    {
        GameStart = true;
        
        iScore = 0;
        PS.RespawnGameStart();

        PS.MovingPlatSpeed = 2;
        PS.BarrierMovingPlatSpeed = 2;

        BSC.Restart();
    }

    public void SubmitScore(string name)
    {
        if (name != null)
        {
            GC.showMessage = true;
            GC.MsgPrompt = "Submitting Scores";
            SendScoreObj personObj = new SendScoreObj(UserId, name, iScore - 1);
            string json = JsonUtility.ToJson(personObj);
            reference.Child("scores").Child(UserId).SetRawJsonValueAsync(json);

            GC.SetShortMessage("Score Submitted", 3);            
        }        
    }

    public void GetLeaderboard()
    {
        GC.SetShortMessage("Loading Leaderboard", 3);

        FirebaseDatabase.DefaultInstance
                .GetReference("scores").OrderByChild("Score")
                .ValueChanged += HandleValueChanged;
    }

    void HandleValueChanged(object sender, ValueChangedEventArgs args)
    {
        if (args.DatabaseError != null)
        {
            Debug.LogError(args.DatabaseError.Message);
            GC.SetShortMessage("Database Error", 3);

            return;
        }
        // Do something with the data in args.Snapshot
        

        DataSnapshot snapshot = args.Snapshot;
        ArrayList SendScoreList = new ArrayList();
        foreach (DataSnapshot child in snapshot.Children)
        {
            string personAlias = child.Child("PersonAlias").Value.ToString();
            string personScore = child.Child("Score").Value.ToString();

            SendScoreObj person = new SendScoreObj(UserId, personAlias, int.Parse(personScore));
            SendScoreList.Add(person);
        }
        GC.LeaderboardScores = SendScoreList;
    }

    public int GetDeviceHighScore()
    {
        if (PlayerPrefs.HasKey("DeviceHighScore"))
        {
            return PlayerPrefs.GetInt("DeviceHighScore");
        }
        else
        {            
            return 0;
        }

    }

    public void ChangePlayerModel(string ModelName)
    {
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            if (gameObject.transform.GetChild(i).transform.name == ModelName)
            {
                gameObject.transform.GetChild(i).tag = "IcoSphere";
                gameObject.transform.GetChild(i).GetComponent<MeshRenderer>().enabled = true;
            }
            else
            {
                gameObject.transform.GetChild(i).tag = "PlayerModel";
                gameObject.transform.GetChild(i).GetComponent<MeshRenderer>().enabled = false;
            }
            
        }
    }    

    public bool PurchaseProjectile(string projectile, int cost)
    {
        if (PlayerPrefs.HasKey("DeviceScorePouch"))
        {
            int DevicePouch = PlayerPrefs.GetInt("DeviceScorePouch");

            if (DevicePouch >= cost)
            {
                PlayerPrefs.SetInt(projectile, 1);
                PlayerPrefs.SetInt("DeviceScorePouch", DevicePouch -= cost);
                GC.SetShortMessage("Character Purchased", 2);
                ChangePlayerModel(projectile);
                return true;
            }
            else
            {
                GC.SetShortMessage("Not enough points", 2);
                return false;
            }
        }
        else
        {
            GC.SetShortMessage("Not enough points", 2);
            return false;
        }
    }

    private void GenerateUserId()
    {
        string glyphs = "abcdefghijklmnopqrstuvwxyz0123456789";
        string GenString = "";

        int charAmount = Random.Range(10, 20); //set those to the minimum and maximum length of your string

        for (int i = 0; i < charAmount; i++)
        {
            GenString += glyphs[Random.Range(0, glyphs.Length)];
        }

        UserId = GenString;
        //Debug.Log(UserId);
    }


}

