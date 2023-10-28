using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShip : Ship
{
    Vector3 _pos;
    public float dashForce;
    private float dashLerp;

    public float stamina = 100;
    
    public override void move()
    {
        _pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _pos.z = 0;

        movemanager.Rotate(_pos);
        movemanager.Move(_pos, speed + dashLerp, true);
    }

    private void Update()
    {
        if (dashLerp != 0f)
            dashLerp = Mathf.Lerp(dashLerp, 0, 10 * Time.deltaTime);
        
        if (Input.GetKeyDown(KeyCode.Space) && stamina >= 50f)
        {
            dashLerp = dashForce;
            stamina -= 50f;
            
            UI_Manager.instance.ChangeStamina(50f);
        }

        if (stamina < 100 && UI_Manager.instance.canGainStamina)
        {
            stamina += 10 * Time.deltaTime;
            UI_Manager.instance.ChangeStamina(10 * Time.deltaTime, true);
        }
    }

}
