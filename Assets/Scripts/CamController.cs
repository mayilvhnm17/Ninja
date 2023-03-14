using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CamController : MonoBehaviour {
    public GameObject target;  
    Vector3 offset;
    
    //for making the camera to move with the player
    void Start () {
        offset = transform.position - target.transform.position; 
	}
	
	void Update () {
        transform.position = target.transform.position + offset;
    }
}
