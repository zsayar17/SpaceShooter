using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;


[System.Serializable]
public class MoveManager
{
    private Transform _transform;
    [Range(0.0f, 10.0f)]
    [SerializeField]
    private float _rotate_speed;

    public MoveManager(Transform transform)
    {
        this._transform = transform;
        _rotate_speed = 4f;
    }

    private float AngleDiff(float v1, float v2)
    {
        float angle_diff;

        angle_diff = v1 - v2;
        if (angle_diff < 0) angle_diff += 360;
        return angle_diff;
    }

    private float GetAngle(Vector3 pos1, Vector3 pos2, float oriantation_angle = 0)
    {
        Vector3 delta;
        float   angle;

        delta = pos1 - pos2;
        angle = Mathf.Atan2(delta.y, delta.x) * Mathf.Rad2Deg - oriantation_angle;
        if (angle < 0) angle += 360;
        return (angle);
    }

    public bool Rotate(Vector3 target_pos, float delta_time = 1.0f)
    {
        float angle;
        float old_angle = 0.0f;
        float rotate_angle = 0.0f;
        int direction = 1;

        angle = GetAngle(target_pos, _transform.position, 90);
        if (old_angle != angle)
        {
            rotate_angle = _transform.eulerAngles.z;
            direction = (AngleDiff(angle, rotate_angle)) > 180 ? -1 : 1;
            old_angle = angle;
        }
        else if (rotate_angle == angle) return (true);
        if (AngleDiff(angle, rotate_angle) <= _rotate_speed || AngleDiff(rotate_angle, angle) <= _rotate_speed)
        {
            rotate_angle = angle;
            return (true);
        }
        rotate_angle += _rotate_speed * direction;
        _transform.rotation = Quaternion.Euler(0, 0, rotate_angle);
        return (false);
    }

    public bool Move(Vector3 target_pos, float speed, bool focus_on_target, float delta_time = 1.0f, float max_distance = 1.0f)
    {
        float distance;
        float angle;
        float pov = 90;

        if (target_pos.z < 0) goto MOVETOFORWARD;

        distance = Vector2.Distance(target_pos, _transform.position);
        if (distance <= max_distance) return (true);

        angle = Mathf.Abs(GetAngle(target_pos, _transform.position, 90) - _transform.eulerAngles.z);
        if (angle <= pov && focus_on_target)
        {
            speed *= pov / (pov + angle);
            _transform.position =  Vector2.MoveTowards(_transform.position, target_pos, speed *  delta_time);
            return (false);
        }
        
        MOVETOFORWARD:
        _transform.localPosition += _transform.up * speed * delta_time;
        return (false);
    }

    public Vector3 getRandomPointOnScreen()
    {
        Vector3 bottomleft;
        Vector3 topright;
        float randomx, randomy;
        float bufferx, buffery;

        bottomleft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));
        topright = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0));

        bufferx = (topright.x - bottomleft.x) / 4;
        buffery = (topright.y - bottomleft.y) / 4;

        randomx = UnityEngine.Random.Range(bottomleft.x + bufferx, topright.x - bufferx);
        randomy = UnityEngine.Random.Range(bottomleft.y + buffery, topright.y - buffery);

        return (new Vector3(randomx, randomy, 0));
    }
}
