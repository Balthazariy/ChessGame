using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitsModel
{
    public GameObject unitObject;

    public int health;
    public int damage;
    public int moveDistance;
    public Enums.UniteType unitType;

    public UnitsModel(int unitHealth, int unitDamage, int unitMoveDistance, Enums.UniteType unitTypeEnum)
    {
        health = unitHealth;
        damage = unitDamage;
        moveDistance = unitMoveDistance;
        unitType = unitTypeEnum;
        Start();
    }

    public void Start()
    {
        Debug.Log(health + " health");
        Debug.Log(damage + " damage");
        Debug.Log(moveDistance + " moveDistance");
        Debug.Log(unitType + " unitType");
    }

    public void SetAUniteType(Enums.UniteType type)
    {
        Debug.Log(type + " unitType");
    }
}
