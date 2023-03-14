using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour {

    public GameObject pooledObj;
    public float poolAmount;

    List<GameObject> poolObjs;
    void Start() {
        poolObjs = new List<GameObject>();
        //Instatiating the objects and putting it in the object pool
        for (int i = 0; i < poolAmount; i++)
        {
            GameObject obj = (GameObject)Instantiate(pooledObj);
            obj.SetActive(false);
            poolObjs.Add(obj);
        }

    }

    public GameObject GetPoolingP()
    {
        //When the object is being called when the generation point is and making it visible
        for (int i = 0; i < poolObjs.Count; i++)
        {
            if(!poolObjs[i].activeInHierarchy)
                return poolObjs[i];
        }

        //Again sending it into the object pool
        GameObject obj = (GameObject)Instantiate(pooledObj);
        obj.SetActive(false);
        poolObjs.Add(obj);
        return obj;
    }
   


}
