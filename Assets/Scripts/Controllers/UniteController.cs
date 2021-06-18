using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniteController
{
    private GameManager _gameManager;
    private GridController _gridController;
    public UnitsModel unitsModel;
    private Data _data;

    private GameObject _groupBlackArmy, _groupWhiteArmy;

    private int _spawnPosForPlayer = 0, _spawnPosForEnemy = 0;
    public GameObject selectedPlayerUnit, selectedEnemyUnit;
    public bool isUniteSelected, isEnemySelected;
    private List<UnitsModel> _playerUnits, _enemyUnits;

    public UniteController(GameObject game, Data data)
    {
        _data = data;
        _groupBlackArmy = game.transform.Find("Group_BlackArmy").gameObject;
        _groupWhiteArmy = game.transform.Find("Group_WhiteArmy").gameObject;
    }

    public void Start()
    {
        _gameManager = Main.Instance.gameManager;
        _gridController = Main.Instance.gameManager.gridController;
        isUniteSelected = false;
        isEnemySelected = false;
        _playerUnits = new List<UnitsModel>();
        _enemyUnits = new List<UnitsModel>();

        _spawnPosForEnemy = _gridController.tiles.Count - 1;
    }

    public void Update()
    {
        Ray ray = _gameManager.mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100, LayerMask.GetMask("BlackArmy")))
        {
            GameObject playerSelection = hit.transform.gameObject;
            Renderer playerUnitRenderer = playerSelection.GetComponent<Renderer>();
            if (playerUnitRenderer != null)
            {
                selectedPlayerUnit = playerSelection;
                if (Input.GetMouseButtonDown(0))
                {
                    for (int i = 0; i <= _playerUnits.Count - 1; i++)
                    {
                        if (_playerUnits[i].unitObject == selectedPlayerUnit)
                        {
                            _gridController.HideAvailableTile();
                            selectedPlayerUnit = _playerUnits[i].unitObject;
                            _gridController.HighlightAvailableTiles(selectedPlayerUnit.transform.position, _playerUnits[i].moveDistance);
                            isUniteSelected = true;
                        }
                    }
                }
            }
        }

        if (Physics.Raycast(ray, out hit, 100, LayerMask.GetMask("WhiteArmy")))
        {
            GameObject enemySelection = hit.transform.gameObject;
            Renderer enemyUnitRenderer = enemySelection.GetComponent<Renderer>();
            if (enemyUnitRenderer != null)
            {
                selectedEnemyUnit = enemySelection;
                if (Input.GetMouseButtonDown(0))
                {
                    for (int i = 0; i <= _enemyUnits.Count - 1; i++)
                    {
                        if (_playerUnits[i].unitObject == selectedEnemyUnit)
                        {
                            selectedEnemyUnit = _enemyUnits[i].unitObject;
                            isEnemySelected = true;
                        }
                    }
                }
            }
        }
    }

    public void SpawnPlayerUnit(Enums.UniteType type)
    {
        Vector3 pos = new Vector3(_gameManager.gridController.tiles[_spawnPosForPlayer].transform.position.x, 0,
                                          _gameManager.gridController.tiles[_spawnPosForPlayer].transform.position.z);

        _playerUnits.Add(new UnitsModel(_data.allUnits.Find(x => x.unitType == type), pos, _groupBlackArmy.transform, Enums.PlayerType.BlackArmy));
        _spawnPosForPlayer += 1;
    }

    public void SpawnEnemyUnit(Enums.UniteType type)
    {
        Vector3 pos = new Vector3(_gameManager.gridController.tiles[_spawnPosForEnemy].transform.position.x, 0,
                            _gameManager.gridController.tiles[_spawnPosForEnemy].transform.position.z);

        _enemyUnits.Add(new UnitsModel(_data.allUnits.Find(x => x.unitType == type), pos, _groupWhiteArmy.transform, Enums.PlayerType.WhiteArmy));
        _spawnPosForEnemy -= 1;
    }

    public void GetUnitCostByUnitType(Enums.UniteType type)
    {
        unitsModel = new UnitsModel(_data.allUnits.Find(x => x.unitType == type));
    }

    private void TakeDamage(int damage, Enums.UniteType type)
    {
        Debug.Log("damage" + unitsModel.damage);
    }
}