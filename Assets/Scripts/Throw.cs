using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throw : MonoBehaviour {
    public PlayerController Player;
    Rigidbody2D kunai;
  
    public Vector2 speed;
    public int delay;

    void Start () {
        Player = FindObjectOfType<PlayerController>();
        kunai = GetComponent<Rigidbody2D>();
        kunai.velocity = speed;
        
    }
	
	//for destroying the object after instantiating
	void Update () {
        Destroy(gameObject, delay);
	}

    //For playing the animation
    public void Kunai()
    {
        Player.anim.SetTrigger("Kunai");
        Player.Fire();
        //ButtonPressed = true;
    }

    //for sending the enemy to object pool
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Debug.Log("Hit");
            other.gameObject.SetActive(false);
        }
    }
}
