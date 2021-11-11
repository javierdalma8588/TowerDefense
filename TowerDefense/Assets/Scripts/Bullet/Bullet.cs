using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform target;

    public float speed = 30;

    public int amountDamage = 20;

    public void Seek (Transform _target)
    {
        target = _target;
    }

    void Update()
    {
    	//If the target was destroyed by other bullet we remove it
        if(target == null)
        {
            Destroy(gameObject);
            return;
        }

        //check the distance between the target and the bullet
        Vector3 dir = target.position - transform.position;
        float distanceFrame = speed * Time.deltaTime;

        //we check the current distance to the target and if that is less we already hit the object
        if(dir.magnitude <= distanceFrame)
        {
            Hit();
            return;
        }

        transform.Translate(dir.normalized * distanceFrame, Space.World);
    }

    void Hit()
    {
        Damage(target);
    }

    void Damage(Transform enemy)
    {
    	//Get enemy script to reduce damage
        BaseEnemy en = enemy.GetComponent<BaseEnemy>();
        en.TakeDamage(amountDamage);
        Destroy(gameObject);
    }
}
