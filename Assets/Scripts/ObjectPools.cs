using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class SpawnManager
{
    public EnemyPool enemyPool;
    
    public void Awake(MonoBehaviour manager)
    {
        enemyPool.Initilized(manager.transform);

        EnemyPool.Instance = enemyPool;
    }

}


[Serializable]
public class ObjectPools
{
     [SerializeField] List<SpaceObject> spaceObjects;
     List<GameObject> outPool, inPool;

    public void Initilized(Transform context)
    {
        outPool = new List<GameObject>();
        inPool = new List<GameObject>();

        for(int i = 0; i < spaceObjects.Count; i++)
        {
            GameObject spaceObject = null;
            for(int j = 0; j < spaceObjects[i].count; j++)
            {
                spaceObject = spaceObjects[i].spaceObject;
                GameObject spcObject = MonoBehaviour.Instantiate(spaceObject, spaceObject.transform.position, spaceObject.transform.rotation, context);

                In(spcObject);

            }

        }
    }

    public void In(GameObject inObject)
    {
        inPool.Add(inObject);
        outPool.Remove(inObject);

        inObject.SetActive(false);
    }
    public void Out(GameObject outObject)
    {
        outPool.Add(outObject);
        outPool.Remove(outObject);

        outObject.SetActive(false);
    }

}

[Serializable]
public class EnemyPool : ObjectPools
{
    private static EnemyPool instance;

    public static EnemyPool Instance
    {
        get
        {
            if(instance == null)
            {
                instance = new EnemyPool();
            }
            return instance;
        }

        set
        {
            instance = value;
        }
    }
    

}



[Serializable]
public class SpaceObject
{
    public int count;
    public GameObject spaceObject;
}


