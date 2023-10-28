using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShip : Ship
{
    Vector3 _pos;
    public override void move()
    {
        _pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _pos.z = 0;

        movemanager.Rotate(_pos);
        movemanager.Move(_pos, speed, true);
    }

    private void Update()
    {
        //if (Input.GetMouseButtonDown(0)) //get from pool 
    }

}
