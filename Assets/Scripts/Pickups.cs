using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickups : MonoBehaviour {

    public int ScoreAdded;
    public PlayerController player;
    private AudioSource coins;

	void Start () {
        player = FindObjectOfType<PlayerController>();
        coins = GameObject.Find("CoinSound").GetComponent<AudioSource>();
	}
	
	//When coin is touched Score increases and coin is sent to object pool
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag=="Player")
        {
            player.score += ScoreAdded;
            gameObject.SetActive(false);
            //bug fix for playing multiple coin sounds
            if(coins.isPlaying)
            {
                coins.Stop();
                coins.Play();
            }
            else
                coins.Play();
        }
    }
}
