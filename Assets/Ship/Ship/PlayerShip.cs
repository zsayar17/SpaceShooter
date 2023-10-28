using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShip : Ship
{
    // Start is called before the first frame update
    [SerializeField] private MoveManager _movemanager;
    Vector3 _pos;

    void Awake()
    {
        _movemanager = new MoveManager(this.transform);

    }
    void FixedUpdate()
    {
        _pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        _movemanager.Rotate(_pos);
        _movemanager.Move(_pos, speed, true);
    }
}
