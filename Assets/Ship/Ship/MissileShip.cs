using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MissileShip: Ship
{
    private Vector3 _pos;
    private bool istimesup;
    private Transform barreltransform;


    private void Start()
    {
        _pos = movemanager.getRandomPointOnScreen();
        barreltransform = transform.GetChild(0);
    }
    public override void move()
    {
        movemanager.Rotate(_pos);
     
        if (movemanager.Move(_pos, speed, true))
            _pos = movemanager.getRandomPointOnScreen();
    }
    public override void attack()
    {
        if (!istimesup) return;

        Instantiate(bullet, barreltransform);
        istimesup = false;
    }
    public override IEnumerator waitToAction()
    {
        if (istimesup) yield return null;

        yield return new WaitForSeconds(actiontime);
        istimesup = true;
    }
}
