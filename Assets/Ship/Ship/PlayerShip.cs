using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShip : Ship
{
    Vector3 _pos;
    public float dashForce;
    private float dashLerp;

    public float stamina = 100;
    public float dashStamina;
    public float staminaGain;

    private Transform barrel;

    private void Start()
    {
        barrel = transform.GetChild(0);
    }

    public override void move()
    {
        _pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _pos.z = 0;

        movemanager.Rotate(_pos);
        movemanager.Move(_pos, speed + dashLerp, true);
    }

    private void Update()
    {
        if (health <= 0f)
            Game_Manager.instance.EndGame(false);
        
        
        if (dashLerp != 0f)
            dashLerp = Mathf.Lerp(dashLerp, 0, 10 * Time.deltaTime);
        
        if (Input.GetKeyDown(KeyCode.Space) && stamina >= dashStamina)
        {
            dashLerp = dashForce;
            stamina -= dashStamina;
            
            UI_Manager.instance.ChangeStamina(dashStamina);
        }

        if (stamina < 100 && UI_Manager.instance.canGainStamina)
        {
            stamina += staminaGain * Time.deltaTime;
            UI_Manager.instance.ChangeStamina(staminaGain * Time.deltaTime, true);
        }

        if (Input.GetMouseButtonDown(0))
            Instantiate(bullet, barrel.position,barrel.rotation).GetComponent<BasicMissile>().owner = "Player";

    }

}
