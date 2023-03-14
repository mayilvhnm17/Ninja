using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public Transform PlatformGenPoint;
    private Vector3 plaformStartPoint;

    public PlayerController thePlayer;
    private Vector3 playerStartPoint;
    private float speed;

    public GameObject bottom;
    private Vector3 bottomPosition;
    public Text score;
    private PlatformDestroyer[] platforms;
  

    GameObject[] pauseObjects,DieObjects;
    bool DieBool=false,paused=false,died=false;

    HealthManager health;
    float highscore;
   

    void Start () {
        health = FindObjectOfType<HealthManager>();
        plaformStartPoint = PlatformGenPoint.position;
        playerStartPoint = thePlayer.transform.position;
        bottomPosition = bottom.transform.position;
        speed = thePlayer.GetComponent<PlayerController>().maxSpeed;
        pauseObjects = GameObject.FindGameObjectsWithTag("ShowOnPause");
        DieObjects = GameObject.FindGameObjectsWithTag("DieObjs");
        hidePaused();
        hideDieM();
    }
		
    //For Restart button
    public void RestartGame()
    {
        StartCoroutine("Restart");
        Restart();
        
        score.text = "SCORE:";
        if (Time.timeScale == 0 && paused==true)
        {
            play();
            paused = false;
        }
        else if(Time.timeScale == 0 && died == true)
        {
            Die();
            died = false;
        }
    }

    //Coroutine for restart
    public IEnumerator Restart()
    {
        thePlayer.gameObject.SetActive(false);
        yield return new WaitForEndOfFrame();
        platforms = FindObjectsOfType<PlatformDestroyer>();
        for(int i=0;i<platforms.Length;i++)
        {
            platforms[i].gameObject.SetActive(false);
        }
        PlatformGenPoint.position = plaformStartPoint;
        thePlayer.transform.position = playerStartPoint;
        bottom.transform.position = bottomPosition;
        thePlayer.maxSpeed = speed;
        thePlayer.score = 0;
        thePlayer.interval = 0;
        thePlayer.gameObject.SetActive(true);
        health.showHealth();
        highscore = PlayerPrefs.GetFloat("Highscore");
        thePlayer.Hscore.text = "HIGHSCORE:" + (int)highscore;
        //hideDieM();
    }

    //for hiding the pause menu
    private void hidePaused()
    {
        
        foreach (GameObject g in pauseObjects)
            g.SetActive(false);
       
    }

    //for showing the pause menu
    private void showPaused()
    {
        foreach (GameObject g in pauseObjects)
            g.SetActive(true);

    }

    //for hiding the restart menu
    private void hideDieM()
    {
        
        DieBool = false;
        foreach (GameObject g in DieObjects)
            g.SetActive(false);
        Time.timeScale = 1;
    }

    //for showing the restart menu
    private void showDieM()
    {
        DieBool = true;
        foreach (GameObject g in DieObjects)
            g.SetActive(true);

    }

    //for pause button
    public void pause()
    {
        paused = true;
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
            showPaused();
            Invoke("showPaused", 0.3f);
        }
        else if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
            hidePaused();
            Invoke("hidePaused", 0.3f);
        }

    }

    //commands necessary for death to occur
     public void Die()
    {
        died = true;
        if (Time.timeScale == 1 && DieBool == false)
         {
             Debug.Log("Dead");
            Time.timeScale = 0;
            showDieM();
             Invoke("showDieM", 0.3f);
         }
        
         else if(Time.timeScale ==0 && DieBool==true)
         {
            Debug.Log("Dead22");
            Time.timeScale = 1;
             hideDieM();
             Invoke("hideDieM", 0.3f);
         }      
    }

    //for play button
    public void play()
    {
        Debug.Log("Resume");
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
            showPaused();
            Invoke("showPaused", 0.3f);
        }
        else if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
            hidePaused();
            Invoke("hidePaused", 0.3f);
        }
    }

    //for menu button
    public void Menu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }
}
