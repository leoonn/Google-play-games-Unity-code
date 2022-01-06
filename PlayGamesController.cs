using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;
using TMPro;

public class PlayGamesController : MonoBehaviour
{  
    //messages 
    public TMP_Text MainText;
    public TMP_Text LoggedTxet;
    public TMP_Text ErrorloggedTxt;
    public TMP_Text Errormessage;
    
    public GameObject buttonLogin;
    void Start()
    {
        AuthenticateUser();
    }

    void AuthenticateUser() //get info of google play games user 
    {
        //Initialize platform google play games
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.Activate();
        Social.localUser.Authenticate((bool success) =>
        {
            if (success == true) //user logged
            {
                Debug.Log("Logged in to Google Play Games Service");
                MainText.gameObject.SetActive(false);
                LoggedTxet.gameObject.SetActive(true);
                LoggedTxet.color = Color.green;
                buttonLogin.SetActive(false);
                ErrorloggedTxt.gameObject.SetActive(false);
                SceneManager.LoadScene("Menu");
            }
            else  // false: not logged
            {
                Debug.LogError("Unable to sign in to Google Play Games Services");
                MainText.gameObject.SetActive(false);
                buttonLogin.SetActive(true);

                ErrorloggedTxt.gameObject.SetActive(true);
                Errormessage.gameObject.SetActive(true);
                ErrorloggedTxt.color = Color.red;
            }
        });
    }

    public static void PostToLeaderBoard(long newScore) //post leaderboards values points
    {
        //(points, leaderboards created on google play )
        Social.ReportScore(newScore, GPGSIds.leaderboard_high_score, (bool sucsess) =>
          {
              if (sucsess)
              {
                  Debug.Log("Posted new Score to leaderboard");
              }
              else
              {
                  Debug.LogError("Unable to post new score to leaderboard");
              }
          });
    
    }

    //open leaderboards google play games
    public static void ShowLeaderboardUI()
    {
        PlayGamesPlatform.Instance.ShowLeaderboardUI(GPGSIds.leaderboard_high_score);
    }

    public void LoginButtonGooglePlay() //login button 
    {
        MainText.gameObject.SetActive(true);
        ErrorloggedTxt.gameObject.SetActive(false);
        buttonLogin.SetActive(false);
        AuthenticateUser();
    }
}
