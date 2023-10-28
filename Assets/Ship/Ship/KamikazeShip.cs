using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KamikazeShip : Ship
{
    public Transform playertransform;
    public float damage;

    public override void move()
    {
        movemanager.Rotate(playertransform.position);
        movemanager.Move(playertransform.position, speed, true);
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (actionmanager.hitDamage(collider, "Player", damage))
            Destroy(gameObject);
    }
}