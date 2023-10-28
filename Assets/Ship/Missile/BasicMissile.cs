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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((owner == "enemy" && actionmanager.hitDamage(collision, "player", damage))
            || actionmanager.hitDamage(collision, "enemy", damage))
            Destroy(gameObject);
    }
}
