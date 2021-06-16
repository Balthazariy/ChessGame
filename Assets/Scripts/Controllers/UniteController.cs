using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniteController
{
    private GameManager _gameManager;
    private GridController _gridController;
    public UnitsModel unitsModel;

    public GameObject singleUnit;
    public List<UnitsModel> unitsList;

    private GameObject _groupBlackArmy, _groupWhiteArmy;
    private int tileIndex = 0;

    public UniteController(GameObject game)
    {
        _groupBlackArmy = game.transform.Find("Group_BlackArmy").gameObject;
        _groupWhiteArmy = game.transform.Find("Group_WhiteArmy").gameObject;
    }

    public void Start()
    {
        _gameManager = Main.Instance.gameManager;
        _gridController = Main.Instance.gameManager.gridController;
        unitsList = new List<UnitsModel>
        {
            new UnitsModel(6,2,1, 2, Enums.UniteType.Pawn, Enums.PlayerType.BlackArmy),
            new UnitsModel(7,3,4, 5, Enums.UniteType.Knight, Enums.PlayerType.BlackArmy),
            new UnitsModel(8,4,4, 5, Enums.UniteType.Bishop, Enums.PlayerType.BlackArmy),
            new UnitsModel(9,5,4, 8, Enums.UniteType.Rook, Enums.PlayerType.BlackArmy),
            new UnitsModel(10,6,4, 12, Enums.UniteType.Queen, Enums.PlayerType.BlackArmy),
        };
    }

    public void SpawnUnit(Enums.UniteType type)
    {

        singleUnit = Object.Instantiate(unitsModel.unitObject, new Vector3(_gameManager.gridController.tilesPositions[tileIndex].transform.position.x, 0,
        _gameManager.gridController.tilesPositions[tileIndex].transform.position.z), Quaternion.identity, _groupBlackArmy.transform);

        singleUnit.name = singleUnit.name.Replace("(Clone)", "");
        tileIndex += 1;
    }

    public void SetUniteType(Enums.UniteType type)
    {
        unitsModel = GetUnitDataByType(type);
    }

    private UnitsModel GetUnitDataByType(Enums.UniteType type)
    {
        return unitsList.Find(x => x.unitType == type);
    }

    public void TakeDamage(int damage, Enums.UniteType type)
    {
        Debug.Log("damage" + unitsModel.damage);
    }
}
