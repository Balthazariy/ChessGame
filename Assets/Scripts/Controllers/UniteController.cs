using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniteController
{
    private GameManager _gameManager;
    public UnitsModel unitsModel;

    public GameObject unit;
    public List<UnitsModel> units;

    private Vector3[,] _unitPosition;
    private GameObject _groupBlackArmy, _groupWhiteArmy;
    private int currX = 0, currY = 0;
    private Vector2Int _currentHover;

    public UniteController(GameObject game)
    {
        _groupBlackArmy = game.transform.Find("Group_BlackArmy").gameObject;
        _groupWhiteArmy = game.transform.Find("Group_WhiteArmy").gameObject;
    }

    public void Start()
    {
        _gameManager = Main.Instance.gameManager;
        units = new List<UnitsModel>
        {
            new UnitsModel(6,2,1, 2, Enums.UniteType.Pawn, Enums.PlayerType.BlackArmy),
            new UnitsModel(7,3,4, 5, Enums.UniteType.Knight, Enums.PlayerType.BlackArmy),
            new UnitsModel(8,4,4, 5, Enums.UniteType.Bishop, Enums.PlayerType.WhiteArmy),
            new UnitsModel(9,5,4, 8, Enums.UniteType.Rook, Enums.PlayerType.BlackArmy),
            new UnitsModel(10,6,4, 12, Enums.UniteType.Queen, Enums.PlayerType.WhiteArmy),
        };
    }

    public void Update()
    {
        
    }

    public void SpawnUnit(Enums.UniteType type)
    {
        if(currX < GridController.gridRows)
        {
            unit = Object.Instantiate(unitsModel.unitObject, new Vector3(_gameManager.gridController.tiles[currX, currY].transform.position.x, 0, _gameManager.gridController.tiles[currX, currY].transform.position.z), Quaternion.identity, _groupBlackArmy.transform);
            currX += 1;
        }
        if(currX >= GridController.gridRows)
        {
            currX = 0;
            currY += 1;
        }
    }

    public void SetUniteType(Enums.UniteType type)
    {
        unitsModel = GetUnitDataByType(type);
    }

    private UnitsModel GetUnitDataByType(Enums.UniteType type)
    {
        return units.Find(x => x.unitType == type);
    }

    private List<Vector2Int> GetAvailableTiles()
    {
        List<Vector2Int> availableTiles = new List<Vector2Int>();
        return availableTiles;
    }
}
