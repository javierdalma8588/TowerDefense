using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public Color hoverColor;
    private Color startColor;

    private Renderer rend;

    [HideInInspector]
    public GameObject turret;

    public Vector3 offset;

    private void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
    }

    private void OnMouseEnter()
    {
        //Only higlight the nodes with no turret
        if (!BuildManager.instance.CanBuild)
            return;

        //if player has money to pay we set the platform to a hover color
        if(BuildManager.instance.EnoughtMoney)
        {
            rend.material.color = hoverColor;
        }else
        {
        	//otherwise we set it to a red color this is more a visual aid to the user
            rend.material.color = Color.red;
        }
    }

    private void OnMouseDown()
    {
        if (!BuildManager.instance.CanBuild)
            return;

        if(turret != null)
        {
            //Already has a turret
            return;
        }

        //build a turret if we are available
        BuildManager.instance.BuildTurret(this);
    }

    private void OnMouseExit()
    {
    	//set color to normal color
        rend.material.color = startColor;
    }

    public Vector3 BuildPos()
    {
    	//build a turret with an offset
        return transform.position + offset;
    }
}
