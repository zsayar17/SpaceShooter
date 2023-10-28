using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Rendering;

public class BasicShip : Ship
{
    public Transform playertransform;
    private Transform rightwing, leftwing;
    private bool istimesup;

    private void Start()
    {
        rightwing = transform.GetChild(0).transform;
        leftwing = transform.GetChild(1).transform;
        istimesup = false;
    }

    public override void move()
    {
        movemanager.Rotate(playertransform.position);
        movemanager.Move(playertransform.position, speed, true, 1, 15);
    }
    public override void attack()
    {
        if (!istimesup) return;

        Instantiate(bullet, rightwing.position,rightwing.rotation).GetComponent<BasicMissile>().owner = "Enemy";
        Instantiate(bullet, leftwing.position,leftwing.rotation).GetComponent<BasicMissile>().owner = "Enemy";
        istimesup = false;
    }
    public override IEnumerator waitToAction()
    {
        if (istimesup) yield break;
        Debug.Log("deneme");
        yield return new WaitForSeconds(actiontime);
        istimesup = true;
    }
}
