using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitsModel
{
    public GameObject unitObject;
    private MeshRenderer _meshRenderer;
    public Material defaultUnitMaterial;
    public Sprite helthBar;

    public int health;
    public int damage;
    public int moveDistance;
    public int unitCost;
    public Enums.UniteType unitType;
    public Enums.PlayerType playerType;

    private UnitData _unitData;

    public UnitsModel(UnitData unitData)
    {
        _unitData = unitData;
        health = _unitData.health;
        damage = _unitData.damage;
        moveDistance = _unitData.moveDistance;
        unitCost = _unitData.unitCost;
        unitType = _unitData.unitType;
        unitObject = Resources.Load<GameObject>("Prefabs/" + unitType);
        // helthBar = unitObject.transform.Find("Sprite_HealthBar").GetComponent<Sprite>();
    }
}