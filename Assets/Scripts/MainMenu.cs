using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {
    public Text Hscore;
    float highscore;

    void Start()
    {
        highscore = PlayerPrefs.GetFloat("Highscore");
        Hscore.text = "HIGHSCORE:" + (int)highscore;
    }

    //for play button
	public void StartLevel()
    {
        SceneManager.LoadScene("InfinitePlatformScroller");
    }

    //for close button
    public void Quit()
    {
        Application.Quit();
    }

    //for reset the highscore
    public void Reset()
    {
        PlayerPrefs.SetFloat("Highscore", 0.00f);
        highscore = PlayerPrefs.GetFloat("Highscore");
        Hscore.text = "HIGHSCORE:" + (int)highscore;
    }

}
