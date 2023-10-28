using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionManager
{
    private GameObject obj;
    public ActionManager(GameObject obj) => this.obj = obj;

    public bool hitDamage(Collision2D collision, string tag, float damage)
    {
        Ship ship;
    
        if (!collision.gameObject.CompareTag(tag)) return false;
        ship = collision.gameObject.GetComponent<Ship>();
        ship.Health -= damage;

        if (ship.health < 0) MonoBehaviour.Destroy(collision.gameObject);
        return true;
    }  
    public void destroyOutScreen()
    {
        Vector3 viewpos = Camera.main.WorldToViewportPoint(obj.transform.position);
        if (viewpos.x < 0 || viewpos.x > 1 || viewpos.y < 0 || viewpos.y > 0)  MonoBehaviour.Destroy(obj);;  
    }
}
