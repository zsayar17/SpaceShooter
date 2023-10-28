using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    [SerializeField] protected MoveManager movemanager;
                     protected ActionManager actionmanager;

    [SerializeField] public float speed;
    [SerializeField] public float damage;
    public string owner;

    void Awake()
    {
        movemanager = new MoveManager(transform);
        actionmanager = new ActionManager(this.gameObject);
    }

    private void FixedUpdate()
    {
        move();
        actionmanager.destroyOutScreen();
    }

    public virtual void move() { }
}
