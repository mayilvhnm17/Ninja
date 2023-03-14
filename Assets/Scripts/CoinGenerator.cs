using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinGenerator : MonoBehaviour {

    public ObjectPooler coinPool;
    public int distanceBt;
	
    //Spawning three coins
    public void SpawnCoins(Vector3 startPosition)
    {
        GameObject coin1 = coinPool.GetPoolingP();
        coin1.transform.position = startPosition;
        coin1.SetActive(true);

        GameObject coin2 = coinPool.GetPoolingP();
        coin2.transform.position = new Vector3(startPosition.x-distanceBt,startPosition.y,startPosition.z);
        coin2.SetActive(true);

        GameObject coin3 = coinPool.GetPoolingP();
        coin3.transform.position = new Vector3(startPosition.x + distanceBt, startPosition.y, startPosition.z);
        coin3.SetActive(true);
    }
}
