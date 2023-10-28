using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEditor.Rendering;
using UnityEngine;

public class Ship : MonoBehaviour
{
    [SerializeField] public float speed;
    [SerializeField] public float health;
    [SerializeField] protected float actiontime;
    [SerializeField] protected MoveManager movemanager;
protected ActionManager actionmanager;

    public float Speed { get { return speed; } set {  speed = value; } }
    public float Health { get { return health; } set {  health = value; } }

    void Awake()
    {
        movemanager = new MoveManager(transform);
        actionmanager = new ActionManager(this.gameObject);
    }
    private void FixedUpdate()
    {
        attack();
        move();
    }

    public virtual IEnumerator waitToAction() { return null; }
    public virtual void move() { }
    public virtual void attack() { }
}
