using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour {
    public GameObject Platform,Blocker;
    public Transform generationPoint,BlockPoint;
    public float Distancebtwn, disMin, disMax;
    float[] PlatformLength;
    float disy;
    int PlatformSelector;
    public int random,spikeRandom,enemyRandom;
    public ObjectPooler[] objpoolers;
    private CoinGenerator coinGenerator;
    public ObjectPooler spikePool,enemyPool;

	void Start () {
        PlatformLength = new float[objpoolers.Length];
        //for getting the length of the platforms
        for (int i=0;i<objpoolers.Length;i++)
        {
            PlatformLength[i] = objpoolers[i].pooledObj.GetComponent<BoxCollider2D>().size.x;
        }
        coinGenerator = FindObjectOfType<CoinGenerator>();
      
    }


    void Update()
    {
        //spawning at generating point after the Platformgeneration point is reached
        if(transform.position.x<generationPoint.position.x)
        {
            //Platform generation using object pooling
            Distancebtwn = Random.Range(disMin, disMax);
            PlatformSelector= Random.Range(0, (objpoolers.Length-1));
            disy = Random.Range(-3, 5);
            transform.position = new Vector3(transform.position.x + (PlatformLength[PlatformSelector]/2) + Distancebtwn, (transform.position.y+disy), transform.position.z);
            //Instantiate(Platform, transform.position, transform.rotation);
            GameObject newPlatform = objpoolers[PlatformSelector].GetPoolingP();
            newPlatform.transform.position = transform.position;
            newPlatform.transform.rotation = transform.rotation;
            newPlatform.SetActive(true);

            //for coin generation
            if (Random.Range(0, 100) < random)
            {
                coinGenerator.SpawnCoins(new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z));
            }

            //For spike generation
            if (PlatformLength[PlatformSelector] >6.0f && Random.Range(0, 100) < spikeRandom)
            {
                float r = (Random.Range(-30, 30))/10;
                GameObject newSpike = spikePool.GetPoolingP();
                Vector3 spikePos = new Vector3(r, 0.7f, 0);
                newSpike.transform.position = transform.position+spikePos;
                newSpike.transform.rotation = transform.rotation;
                newSpike.SetActive(true);
            }

            //For Enemy generation
            if (PlatformLength[PlatformSelector] > 4.0f && Random.Range(0, 100) < enemyRandom)
            {
                GameObject newEnemy = enemyPool.GetPoolingP();
                Vector3 enemyPos = new Vector3(0.7f, 2.0f, 0);
                newEnemy.transform.position = transform.position + enemyPos;
                newEnemy.transform.rotation = transform.rotation;
                newEnemy.SetActive(true);
            }

            transform.position = new Vector3(transform.position.x + (PlatformLength[PlatformSelector]-1), transform.position.y, transform.position.z);

        }

        //For generating a blocker at the back of the player if any bug happens
        if(Blocker.transform.position.x<BlockPoint.position.x)
        {
            Blocker.transform.position = new Vector3(Blocker.transform.position.x + 18, Blocker.transform.position.y, Blocker.transform.position.z);
            
        }
    }
}
