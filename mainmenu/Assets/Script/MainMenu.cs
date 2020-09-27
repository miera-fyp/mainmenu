using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Text playerDisplay;

    private void Start()
    {
        if (DBManager.LoggedIn)
        {
            playerDisplay.text = "Welcome " + DBManager.username;
        }
    }

    public void GoToRegister()
    {
        SceneManager.LoadScene(1);
        //load scene registermenu
                          
    }

    public void GoToLogin()
    {
        SceneManager.LoadScene(2);
        //load scene loginmenu

    }
}
