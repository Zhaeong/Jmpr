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
            SendScoreObj personObj = new SendScoreObj(name, iScore - 1);
            string json = JsonUtility.ToJson(personObj);
            reference.Child("scores").Child(name).SetRawJsonValueAsync(json);
        }
        
    }

    public void GetLeaderboard()
    {
        FirebaseDatabase.DefaultInstance
                .GetReference("scores").OrderByChild("Score")
                .ValueChanged += HandleValueChanged;
    }

    void HandleValueChanged(object sender, ValueChangedEventArgs args)
    {
        if (args.DatabaseError != null)
        {
            Debug.LogError(args.DatabaseError.Message);
            return;
        }
        // Do something with the data in args.Snapshot

        DataSnapshot snapshot = args.Snapshot;
        ArrayList SendScoreList = new ArrayList();
        foreach (DataSnapshot child in snapshot.Children)
        {
            string personAlias = child.Child("PersonAlias").Value.ToString();
            string personScore = child.Child("Score").Value.ToString();

            SendScoreObj person = new SendScoreObj(personAlias, int.Parse(personScore));
            SendScoreList.Add(person);

            //Debug.Log(personAlias + " --" + personScore);
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


}

