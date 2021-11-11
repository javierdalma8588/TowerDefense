using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret1 : BaseTurret
{
	 //Class just created in case we need a special functionality out of the enemy
	//It takes all of his behaviour out of the Base Turret Class
    void Start()
    {
        SetState(State.LookForEnemy);
    }
}
