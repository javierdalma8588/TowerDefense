using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    //initialization of the singleton
    public static BuildManager instance;

    private TurretSpecs selectedTurret;

    public GameObject turret1Prefab;
    public GameObject turret2Prefab;
    public GameObject turret3Prefab;

    void Awake()
    {
        //Singleton
        if (instance == null)
        {

            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    //property to check if the selected turrent is not null and return true or false 
    public bool CanBuild { 
        get 
        { 
            return selectedTurret != null; 
        } 
    }
    
    //property to check if the player has enought cash to pay the turret he wants to spawn
    public bool EnoughtMoney
    {
        get
        {
            return Stats.cash >= selectedTurret.cost;
        }
    }

    //build turrets funtionality
    public void BuildTurret(Platform node)
    {
        if(Stats.cash < selectedTurret.cost)
        {
            //Not enought cash
            return;
        }

        //reduce the cash amount by the turret cost
        Stats.cash -= selectedTurret.cost;
        //update the cash ui text
        UIManager.instance.cashText.text = "$" +Stats.cash.ToString();

        //instantiate a turret and store the variable on a gameobject
        GameObject turret = (GameObject) Instantiate(selectedTurret.prefab, node.BuildPos(), Quaternion.identity);
        node.turret = turret;
    }

    public void setTurretBuild(TurretSpecs turret)
    {
        selectedTurret = turret;
    }
}
