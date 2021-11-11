using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTurret : MonoBehaviour
{
    [Header("Turret Specs")]
    public float range = 10;
    public float shootInterval = 2f;

    public GameObject bulletPrefab;

    public bool isLaser = false;
    public LineRenderer laser;
    public int laserDamage = 40;

    public Vector3 shootOffset = new Vector3(0, 1.5f, 0);

    //Set state machine states
    public enum State
    {
        LookForEnemy,
        ShootEnemy
    }

    public State currentState;

    public BaseEnemy currentEnemy;

    // set the first state to look for enemy
    void Start()
    {
        SetState(State.LookForEnemy);
    }

    //coroutine calle when we are on the shootEnemy state
    IEnumerator OnShootingEnemy()
    {
        //OnStateEnter
        float shootTimer = 0;

        while (currentEnemy !=null && !GameManager._instance.gameOver && currentEnemy.startHealth > 0 && Vector3.Distance(transform.position, currentEnemy.transform.position) < range + 0.5f)
        {
            //look at enemy
            transform.LookAt(currentEnemy.transform);

            //if the turret is specifically a laser
            if(isLaser)
            {
                //damage the enemy with the laser damage by time.deltatime
                currentEnemy.TakeDamage(laserDamage * Time.deltaTime);

                if (!laser.enabled)
                {
                //enable the laser
                    laser.enabled = true;
                }

                //check if the enemy is in range and if thet is the case
                if(Vector3.Distance(transform.position, currentEnemy.transform.position) < range + 0.5f)
                {
                    //we set the laser (that is a line renderer) starting position and end position
                    laser.SetPosition(0, gameObject.transform.position - shootOffset);
                    laser.SetPosition(1, currentEnemy.transform.position);
                }
            }
            //if its no laser
            else
            {
                //we set the shoot interval
                shootTimer += Time.deltaTime;
                //if shoottimer is more than the interval
                if (shootTimer > shootInterval)
                {
                    //Debug.LogError("shoot");
                    //shoot the enemy
                    ShootAt(currentEnemy.transform, currentEnemy.transform.position + shootOffset);
                    shootTimer = 0;
                }
            }
            
            //OnStateUpdate
            yield return null;
        }

        //if we dont have enemy, enemy went out of range and is laser we disable the laser
        if(isLaser)
        {
            laser.enabled = false;
        }

        //if we dont have an enemy we go back to the state look for enemy
        currentEnemy = null;
        SetState(State.LookForEnemy);
    }

    //coroutine to call when we are on the state look for enemies
    IEnumerator OnLookforEnemies()
    {
        //OnStateEnter
        yield return new WaitForSeconds(0.5f);
        while (currentEnemy == null)
        {
            LookForEnemies();
            //OnStateUpdate
            yield return null;

        }
        SetState(State.LookForEnemy);
    }

    void LookForEnemies()
    {
        //Get all units around us
        Collider[] surroundingColliders = Physics.OverlapSphere(transform.position, range);
        foreach (Collider c in surroundingColliders)
        {
            //get if they are an enemy
            BaseEnemy otherUnit = c.GetComponent<BaseEnemy>();
            if (otherUnit != null && otherUnit.startHealth > 0)
            {
                //we set the enemy and call the shooting enemy state
                currentEnemy = otherUnit;
                SetState(State.ShootEnemy);
                return;
            }
        }
    }

    //state machine switch
    public void SetState(State newState)
    {
        //We stop the coroutine of the state that is currently running
        StopAllCoroutines();
        currentState = newState;
        //Based on the currentState enum, we start the corresponding Coroutine
        switch (currentState)
        {
            case State.LookForEnemy:
                StartCoroutine(OnLookforEnemies());
                break;
            case State.ShootEnemy:
                StartCoroutine(OnShootingEnemy());
                break;
        }
    }

    //shoot function
    protected void ShootAt(Transform targetObj, Vector3 targetPos)
    {
        //instantiate the bullet
        GameObject bulletIns = (GameObject)Instantiate(bulletPrefab, gameObject.transform.position - shootOffset, gameObject.transform.rotation);
        Bullet bullet = bulletIns.GetComponent<Bullet>();

        //bullet lloks for enemy
        if (bullet != null)
            bullet.Seek(currentEnemy.transform);
    }

    //gizmo just to represent the range of each turret this is just for testing
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}


