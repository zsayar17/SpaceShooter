using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuidedMissile : Missile
{
    public Transform playertransform;

    public override void move()
    {
        movemanager.Rotate(playertransform.position);
        movemanager.Move(playertransform.position, speed, false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (actionmanager.hitDamage(collision, "Player", damage))
            Destroy(gameObject);
    }
}
