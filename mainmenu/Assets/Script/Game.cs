using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    //getting input from database
    public Text playerDisplay;
    public Text scoreDisplay;
    public Text timedisplay;
    public Text dbtestdisplay;
    public Text floattestdisplay;

    //*********TIMER*************//
    public float timeStart;
    //to see the time ticking 
    public Text timeText;
    //play pause 
    public Text startBtnText;

    bool timerActivate = false;
    //*********TIMER*************//

    public void Start()
    {
        timeText.text = timeStart.ToString();
    }

    public void Update()
    {
        if (timerActivate)
        {
            timeStart += Time.deltaTime;
            timeText.text = timeStart.ToString();
        }
        timedisplay.text = "Time : " + DBManager.duration;
    }

    /*public void timerButton()
    {
        timerActivate = !timerActivate; // if true
        startBtnText.text = timerActivate ? "Pause" : "Start";

    }*/

    private void Awake()
    {
        if (DBManager.username == null)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
        //getting all username, score and time from database 
        playerDisplay.text = "Player : " + DBManager.username;
        scoreDisplay.text = "Score : " + DBManager.score;
        timedisplay.text = "Time : " + DBManager.duration;
        dbtestdisplay.text = "DB Test : " + DBManager.dbtest;
        floattestdisplay.text = "Float Testing retrieve data : " + DBManager.dbfloat;

        //timedisplay.text = "Time : " + DBManager.playertime;
    }

    public void CallSaveData()
    {
        StartCoroutine(SavePlayerData());
    }

    IEnumerator SavePlayerData()
    {
        WWWForm form = new WWWForm();
        //add data to form 
        //save to database 
        form.AddField("name", DBManager.username);
        form.AddField("score", DBManager.score);
        form.AddField("time", DBManager.duration);
        form.AddField("testingdb", DBManager.dbtest);
        form.AddField("floattestingdb", DBManager.dbfloat.ToString());

        WWW www = new WWW("http://localhost/sqlconnect/savedata.php", form);
        yield return www;
        //checking if there is any score has been return 
        if (www.text == "0")
        {
            Debug.Log("Game Saved");
        }
        else
        {
            Debug.Log("Save failed. Error # " + www.text);
        }

        DBManager.LogOut();
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public void IncreaseScore()
    {
        //score calculation 
        DBManager.score++;
        //update score display
        scoreDisplay.text = "Score : " + DBManager.score;
    }

    public void IncreaseTime()
    {
        timerActivate = !timerActivate; // if true
        startBtnText.text = timerActivate ? "Pause" : "Start";
    }


    public void IncreaseDBTesting()
    {
        DBManager.dbtest++;
        dbtestdisplay.text = "DB Test : " + DBManager.dbtest;
    }

    public void IncreaseFloat()
    {
        DBManager.dbfloat++;
        floattestdisplay.text = "Float Testing calculation  : " + DBManager.dbfloat;
    }
}

