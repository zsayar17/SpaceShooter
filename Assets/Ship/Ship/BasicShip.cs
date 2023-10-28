using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicShip : Ship
{
    public Transform playertransform;
    private bool istimesup;

    public override void move()
    {
        movemanager.Rotate(playertransform.position);
        movemanager.Move(playertransform.position, speed, true, 1, 15);
    }
    public override void attack()
    {
        if (!istimesup) return;

        //actionmanager.spawn()
        //actionmanager.spawn()
        istimesup = false;
    }
    public override IEnumerator waitToAction()
    {
        if (istimesup) yield return null;

        yield return new WaitForSeconds(actiontime);
        istimesup = true;
    }
}
