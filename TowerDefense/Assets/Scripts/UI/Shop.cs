using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    //turret prefabs
    public TurretSpecs turret1;
    public TurretSpecs turret2;
    public TurretSpecs turret3;

    //set the turret to build 
    public void PurchaseTurret1()
    {
        BuildManager.instance.setTurretBuild(turret1);
    }

    public void PurchaseTurret2()
    {
        BuildManager.instance.setTurretBuild(turret2);
    }

    public void PurchaseTurret3()
    {
        BuildManager.instance.setTurretBuild(turret3);
    }
}
