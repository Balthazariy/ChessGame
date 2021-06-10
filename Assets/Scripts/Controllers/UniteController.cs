using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniteController
{
    public UnitsModel unitsModel;
    public UnitData unitData;

    public UniteController()
    {
        unitData = new UnitData();

        unitsModel = new UnitsModel(unitData.health, unitData.damage, unitData.moveDistance, unitData.unityType);
    }

    public void Start()
    {

    }

    public void SendAData()
    {

    }
}
