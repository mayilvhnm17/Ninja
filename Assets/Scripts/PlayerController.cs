using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PlayerController : MonoBehaviour {

    public float maxSpeed = 5.0f, jumpF = 10.0f,interval, score;
    public static bool facingRight = true;
    public Rigidbody2D ninja, cam,Dcollider;
    public GameObject KWeapon;
    public Animator anim;
    bool grounded = false, doublejump = false;
    public Transform groundCheck,firePos;
    float gradius = 0.5f,highscore;
    public LayerMask whatIsGround;
    public Text Score,Hscore;
    public GameManager gamemanager;

    private AudioSource jump,throwSound;

    void Start () {
        anim=GetComponent<Animator>();
        ninja=GetComponent<Rigidbody2D>();
        highscore = PlayerPrefs.GetFloat("Highscore");
        jump = GameObject.Find("Jump").GetComponent<AudioSource>();
        throwSound = GameObject.Find("Throw").GetComponent<AudioSource>();
        Hscore.text = "HIGHSCORE:" + (int)highscore;
    }
	
	
	void FixedUpdate () {
        //groundCheck
        grounded = Physics2D.OverlapCircle(groundCheck.position, gradius, whatIsGround);
        anim.SetBool("Ground", grounded);
        anim.SetFloat("vSpeed", ninja.velocity.y);
        if (grounded)
            doublejump = false;

        //moving the camera
        //float move = CrossPlatformInputManager.GetAxis("Horizontal");
        cam.velocity = new Vector2(0.8f * maxSpeed, cam.velocity.y);

        //for playing animation and moving the player   
        anim.SetFloat("Speed",Mathf.Abs(0.8f));
        ninja.velocity = new Vector2(cam.velocity.x, ninja.velocity.y);
        Dcollider.velocity = new Vector2(cam.velocity.x, Dcollider.velocity.y);

        //CheckRight
        /*if (move > 0 && !facingRight)
            Flip();
        else if (move < 0 && facingRight)
            Flip();
        */
    }

    void Update()
    {
        //Score updation and when reached a certain limit the speed of the player and camera increases
        if (Time.timeScale == 1)
        {
            score += (ninja.velocity.x / 4);
            Score.text = "SCORE:" + (int)score;
            interval += (ninja.velocity.x / 4);
            if (interval > 2000)
            {
                maxSpeed += 0.7f;
                interval = 0;
            }
        }
        
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        //Checking for the player is falling down and when collided
        if (other.gameObject.tag=="Bottom")
        {
            gamemanager.Die();
            //Highscore reset
            if (score > highscore)
            {
                PlayerPrefs.SetFloat("Highscore", score);
            }
            else if (highscore == 0)
            {
                PlayerPrefs.SetFloat("Highscore", score);
            }
            highscore = PlayerPrefs.GetFloat("Highscore");
            Debug.Log(highscore);
        }
     }

    
    //For Instantiating Kunai weapon
    public void Fire()
    {
        Debug.Log("Fired");
        Instantiate(KWeapon, firePos.position,Quaternion.identity);
    }

    //fliping the player
    /*void Flip()
    {
        facingRight = !facingRight;
        Vector3 Scale = transform.localScale;
        Scale.x *= -1;
        transform.localScale = Scale;
    }
    */

    //For jump button and check for double jump possible or not
    public void Jump()
    {
        if ((grounded || !doublejump))
        {
            anim.SetBool("Ground", false);
            ninja.AddForce(new Vector2(0, jumpF), ForceMode2D.Impulse);
            jump.Play();
            if (!doublejump && !grounded)
                doublejump = true;
        }
    }

    //Button for firing the Kunai weapon
    public void Kunai()
    {
        anim.SetTrigger("Kunai");
        Fire();
        throwSound.Play();
    }

    //Button for PushingDown the player
    public void PushDown()
    {
        anim.SetTrigger("PushDown");
        ninja.AddForce(new Vector2(0,-15), ForceMode2D.Impulse);
    }
}
