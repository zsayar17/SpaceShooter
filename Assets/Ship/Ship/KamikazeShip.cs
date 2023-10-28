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
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if (actionmanager.hitDamage(collision, "player", damage))
        //    Destroy(gameObject); //give back to pool
    }
}