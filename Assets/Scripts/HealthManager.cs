using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour {

    GameManager gameManager;

    GameObject[] Health;
    int noOfIcons,i=0;


	void Start () {
        gameManager = FindObjectOfType<GameManager>();
        Health= GameObject.FindGameObjectsWithTag("Health");
        showHealth();
        noOfIcons = Health.Length;
	}

    //For displaying Health
    public void showHealth()
    {
        foreach(GameObject g in Health)
        {
            g.SetActive(true);
        }
        i = 0;
        noOfIcons = Health.Length;
    }

    //When Spike is Touched by the player
    //Used for enemy but get two health icons reduced because of two colliders in the player
    public void SpikeHit()
    {
        if (noOfIcons > 1)
        {
            Debug.Log("spike");
            //Destroy(Health[i], 0.2f);
            Health[i].SetActive(false);
            noOfIcons -= 1;
            //showHealth();
            i++;
        }else if(noOfIcons==1)
        {
            //For last Health icon
            Debug.Log("LastSpike");
            //Destroy(Health[i], 0.2f);
            Health[i].SetActive(false);
            gameManager.Die();
            
        }
    }
}
