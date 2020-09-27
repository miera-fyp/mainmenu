﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    public Text playerDisplay;
    public Text scoreDisplay;

    private void Awake()
    {
        if (DBManager.username == null)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }

        playerDisplay.text = "Player : " + DBManager.username;
        scoreDisplay.text = "Score : " + DBManager.score;
    }

    public void CallSaveData()
    {
        StartCoroutine(SavePlayerData());
    }

    IEnumerator SavePlayerData()
    {
        WWWForm form = new WWWForm();
        form.AddField("name" ,DBManager.username);
        form.AddField("score" ,DBManager.score);

        WWW www = new WWW("http://localhost/sqlconnect/savedata.php", form);
        yield return www;
        //checking if there is any score has been return 
        if (www.text == "0")
        {
            Debug.Log("Game Saved");
        }

        DBManager.LogOut();
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public void IncreaseScore()
    {
        //score calculation 
        DBManager.score++;
        scoreDisplay.text = "Score : " + DBManager.score;
    }


}
