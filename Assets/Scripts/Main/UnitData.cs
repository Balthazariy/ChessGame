using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class UnitData
{
    public Enums.UniteType unityType { get; set; }
    public int damage { get; set; }
    public int health { get; set; }
    public int moveDistance { get; set; }

    public UnitData()
    {
        var json = Resources.Load<TextAsset>($"JsonData/Units").text;
        GameDataSerializeHelper helper = JsonConvert.DeserializeObject<GameDataSerializeHelper>(json);
        unityType = helper.unityTypeEnum;
        damage = helper.damageValue;
        health = helper.healthValue;
        moveDistance = helper.moveDistanceValue;
    }
}

[Serializable]
public class GameDataSerializeHelper
{
    public Enums.UniteType unityTypeEnum { get; set; }
    public int damageValue { get; set; }
    public int healthValue { get; set; }
    public int moveDistanceValue { get; set; }
}
