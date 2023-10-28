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

    }
    
}


[Serializable]
public class ObjectPools
{
     [SerializeField] List<SpaceObject> spaceObjects;
     public List<GameObject> outPool, inPool;

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
        inPool.Remove(outObject);

        outObject.SetActive(true);
    }
    public GameObject Out(int index)
    {
        outPool.Add(inPool[index]);
        inPool[index].SetActive(true);
        ///inPool.Remove(inPool[index]);
        return inPool[index];
    }
}

[Serializable]
public class EnemyPool : ObjectPools
{
    public Transform origin;

    public float repeatTime;
    public float width,height;

    private RepeatTime rptime = new RepeatTime();


    public void Spawn()
    {
        if (rptime.Repeat(repeatTime) && inPool.Count>0)
        {
            Transform trsfm;
            float x = UnityEngine.Random.Range(origin.transform.position.x - width / 2, origin.transform.position.x + width / 2);
            float y = UnityEngine.Random.Range(origin.transform.position.y - height / 2, origin.transform.position.y + height / 2);
            trsfm = Out(UnityEngine.Random.Range(0, inPool.Count - 1)).transform;
            trsfm.position = new Vector3(x, y, 0);

            inPool.Remove(trsfm.gameObject);

        }
    }



}



[Serializable]
public class SpaceObject
{
    public int count;
    public GameObject spaceObject;
}

public class RepeatTime
{

    private float currentTime;
    

    public bool Repeat(float repeatTime)
    {
        if (Time.time >= currentTime)
        {

            currentTime = Time.time + repeatTime;
            return true;
        }
        return false;
    }



}


