using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniteController
{
    public GameObject unit;
    public UnitsModel unitsModel;
    public UnitData unitData;

    public List<UnitsModel> units;

    public UniteController()
    {
        unitData = new UnitData();
    }

    public void Start()
    {
        units = new List<UnitsModel>
        {
            new UnitsModel(6,2,1, 2, Enums.UniteType.Pawn, Enums.PlayerType.Black),
            new UnitsModel(7,3,4, 5, Enums.UniteType.Knight, Enums.PlayerType.Black),
            new UnitsModel(8,4,4, 5, Enums.UniteType.Bishop, Enums.PlayerType.White),
            new UnitsModel(9,5,4, 8, Enums.UniteType.Rook, Enums.PlayerType.Black),
            new UnitsModel(10,6,4, 12, Enums.UniteType.Queen, Enums.PlayerType.White),
        };
    }

    public void SetUniteType(Enums.UniteType type)
    {
        unitsModel = GetUnitDataByType(type);
    }

    private UnitsModel GetUnitDataByType(Enums.UniteType type)
    {
        return units.Find(x => x.unitType == type);
    }
}
