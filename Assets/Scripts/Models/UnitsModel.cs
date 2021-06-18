using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitsModel
{
    public GameObject unitObject;
    public Material defaultUnitMaterial;
    public SpriteRenderer healthBar;

    public int health;
    public int damage;
    public int moveDistance;
    public int unitCost;
    public Enums.UniteType unitType;
    public Enums.PlayerType playerType;

    private UnitData _unitData;
    public bool isUnitCanMove;

    public UnitsModel(UnitData unitData, Vector3 possition, Transform parent, Enums.PlayerType playerTypeEnum)
    {
        _unitData = unitData;
        health = _unitData.health;
        damage = _unitData.damage;
        moveDistance = _unitData.moveDistance;
        unitType = _unitData.unitType;
        playerType = playerTypeEnum;
        isUnitCanMove = true;

        unitObject = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/" + unitType), possition, Quaternion.identity, parent);
        unitObject.name = unitObject.name.Replace("(Clone)", "");
        unitObject.layer = LayerMask.NameToLayer(playerType.ToString());

        defaultUnitMaterial = Resources.Load<Material>("Materials/" + playerType);

        unitObject.GetComponent<MeshRenderer>().material = defaultUnitMaterial;


        healthBar = unitObject.transform.Find("Sprite_HealthBar").GetComponent<SpriteRenderer>();
        healthBar.gameObject.transform.localScale = new Vector3(health, 1, 1);

    }

    public UnitsModel(UnitData unitData) // Constructor for getting unit Cost. Using [unitCost] in GameManager. Row - 34
    {
        _unitData = unitData;
        unitCost = _unitData.unitCost;
    }
}