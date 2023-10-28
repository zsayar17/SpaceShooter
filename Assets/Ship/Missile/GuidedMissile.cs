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

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (actionmanager.hitDamage(collider, "Player", damage))
            Destroy(gameObject);
    }
}
