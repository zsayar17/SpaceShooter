using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class BasicShip : Ship
{
    public Transform playertransform;
    private Transform rightwing, leftwing;
    private bool istimesup;

    private void Start()
    {
        rightwing = transform.GetChild(0).transform;
        leftwing = transform.GetChild(1).transform;
    }

    public override void move()
    {
        movemanager.Rotate(playertransform.position);
        movemanager.Move(playertransform.position, speed, true, 1, 15);
    }
    public override void attack()
    {
        if (!istimesup) return;

        Instantiate(bullet, rightwing).GetComponent<BasicMissile>().owner = "Enemy";
        Instantiate(bullet, leftwing).GetComponent<BasicMissile>().owner = "Enemy";
        istimesup = false;
    }
    public override IEnumerator waitToAction()
    {
        if (istimesup) yield return null;

        yield return new WaitForSeconds(actiontime);
        istimesup = true;
    }
}
