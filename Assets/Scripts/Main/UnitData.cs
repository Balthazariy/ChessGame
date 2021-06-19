using System;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

[Serializable]
public class UnitData
{
    public Enums.UniteType unitType;
    public int damage;
    public int health;
    public int moveDistance;
    public int unitCost;
}

public class Data
{
    public List<UnitData> allUnits;
    public Data()
    {
        var json = Resources.Load<TextAsset>($"JsonData/Units").text;
        GameDataSerializeHelper helper = JsonConvert.DeserializeObject<GameDataSerializeHelper>(json);
        allUnits = helper.Units;
    }
}

[Serializable]
public class GameDataSerializeHelper
{
    public List<UnitData> Units;
}