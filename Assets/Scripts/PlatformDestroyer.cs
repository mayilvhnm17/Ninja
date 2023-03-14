using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformDestroyer : MonoBehaviour {

    public GameObject PlatformDestructionpt;
	
	void Start () {
        PlatformDestructionpt = GameObject.Find("PlatformDestructionPoint");
        
    }
	
	
	void Update () {
        //For sending the platform or enemy or spike into its respective object pool
            if (transform.position.x < PlatformDestructionpt.transform.position.x)
            {
                //Destroy(gameObject);
                gameObject.SetActive(false);
               
            }
        
		
	}
}
