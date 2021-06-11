using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitsModel
{
    public GameObject unitObject;
    private MeshRenderer _meshRenderer;
    private Material _material;

    public int health;
    public int damage;
    public int moveDistance;
    public int unitCost;
    public Enums.UniteType unitType;
    public Enums.PlayerType playerType;

    private int currentX;
    private int currentY;
    private Vector3 desirePosition;
    private Vector3 desireScale;

    public UnitsModel(int unitHealth, int unitDamage, int unitMoveDistance, int cost, Enums.UniteType unitTypeEnum, Enums.PlayerType playerTypeEnum)
    {
        health = unitHealth;
        damage = unitDamage;
        moveDistance = unitMoveDistance;
        unitCost = cost;
        unitType = unitTypeEnum;
        playerType = playerTypeEnum;
        unitObject = Resources.Load<GameObject>("Prefabs/" + unitTypeEnum);
        _material = Resources.Load<Material>("Materials/" + playerTypeEnum);
        _meshRenderer = unitObject.GetComponent<MeshRenderer>();
        _meshRenderer.material = _material;
    }
}
