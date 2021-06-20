using UnityEngine;

public class UnitsModel
{
    public GameObject unitObject;
    private Material _defaultUnitMaterial;
    public SpriteRenderer healthBar;

    public int health;
    public int damage;
    public int moveDistance;
    public int unitCost;
    private Enums.UniteType _unitType;
    private Enums.PlayerType _playerType;

    private UnitData _unitData;
    public bool isUnitCanMove;

    public UnitsModel(UnitData unitData, Vector3 possition, Transform parent, Enums.PlayerType playerTypeEnum) // Default constructor for new Units
    {
        _unitData = unitData;
        health = _unitData.health;
        damage = _unitData.damage;
        moveDistance = _unitData.moveDistance;
        _unitType = _unitData.unitType;
        _playerType = playerTypeEnum;
        isUnitCanMove = true;

        unitObject = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/" + _unitType), possition, Quaternion.identity, parent);
        unitObject.name = unitObject.name.Replace("(Clone)", "");
        unitObject.layer = LayerMask.NameToLayer(_playerType.ToString());

        _defaultUnitMaterial = Resources.Load<Material>("Materials/" + _playerType);

        unitObject.GetComponent<MeshRenderer>().material = _defaultUnitMaterial;


        healthBar = unitObject.transform.Find("Sprite_HealthBar").GetComponent<SpriteRenderer>();
        healthBar.gameObject.transform.localScale = new Vector3(health, 1, 1);


        GetComponentForPlayerUnits(_playerType);
    }

    private void GetComponentForPlayerUnits(Enums.PlayerType type)
    {
        if (type == Enums.PlayerType.BlackArmy)
            unitObject.GetComponent<MouseEventsArgs>().PlayerUnitSelectEvent += Main.Instance.gameManager.uniteController.SelectPlayerUnitEventHandler;
    }

    public UnitsModel(UnitData unitData) // Constructor for getting unit Cost. Using [unitCost] in GameManager.
    {
        _unitData = unitData;
        unitCost = _unitData.unitCost;
    }
}