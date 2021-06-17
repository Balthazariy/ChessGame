using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniteController
{
    private GameManager _gameManager;
    private GridController _gridController;
    public UnitsModel unitsModel;

    private GameObject _singleUnit;

    private GameObject _groupBlackArmy, _groupWhiteArmy;
    private int tileIndex = 0;
    public GameObject selectedPlayerUnit, selectedEnemyUnit;
    public bool isUniteSelected, isEnemyTakeDamage;
    private Data _data;
    public Vector3 _currentPlayerUnitPosition, _currentEnemyUnitPosition;

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
        isEnemyTakeDamage = false;
    }

    public void Update()
    {
        Ray ray = _gameManager.mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100, LayerMask.GetMask("PlayerUnit")))
        {
            GameObject playerSelection = hit.transform.gameObject;
            Renderer playerUnitRenderer = playerSelection.GetComponent<Renderer>();
            if (playerUnitRenderer != null)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Enum.TryParse(selectedPlayerUnit.name, out Enums.UniteType type);
                    SetUniteType(type);
                    isUniteSelected = true;
                    _gridController.HighlightAvailableTiles(_currentPlayerUnitPosition, _gameManager.uniteController.unitsModel.moveDistance);
                }
            }
            selectedPlayerUnit = playerSelection;
            _currentPlayerUnitPosition = selectedPlayerUnit.transform.position;
        }

        if (Physics.Raycast(ray, out hit, 100, LayerMask.GetMask("EnemyUnit")))
        {
            GameObject enemySelection = hit.transform.gameObject;
            Renderer enemyUnitRenderer = enemySelection.GetComponent<Renderer>();
            if (enemyUnitRenderer != null)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Enum.TryParse(selectedEnemyUnit.name, out Enums.UniteType type);
                    SetUniteType(type);
                    TakeDamage(unitsModel.damage, type);
                }
            }
            selectedEnemyUnit = enemySelection;
            _currentEnemyUnitPosition = selectedEnemyUnit.transform.position;
        }
    }

    public void SpawnUnit(Enums.UniteType type)
    {

        _singleUnit = GameObject.Instantiate(unitsModel.unitObject, new Vector3(_gameManager.gridController.tiles[tileIndex].transform.position.x, 0,
        _gameManager.gridController.tiles[tileIndex].transform.position.z), Quaternion.identity, _groupBlackArmy.transform);

        _singleUnit.name = _singleUnit.name.Replace("(Clone)", "");
        tileIndex += 1;
    }

    public void SetUniteType(Enums.UniteType type)
    {
        unitsModel = new UnitsModel(_data.allUnits.Find(x => x.unitType == type));
    }

    private void TakeDamage(int damage, Enums.UniteType type)
    {
        Debug.Log("damage" + unitsModel.damage);
    }
}