using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMissile : Missile
{

    Vector3 target_pos;

    private void Start()
    {
        target_pos.Set(-1, -1, -1);
    }
    public override void move()
    {
        movemanager.Move(target_pos, speed, false);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if ((owner == "Enemy" && actionmanager.hitDamage(collider, "Player", damage))
            || actionmanager.hitDamage(collider, "Enemy", damage))
            Destroy(gameObject);
    }
}
