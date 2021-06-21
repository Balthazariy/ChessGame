using System;
using System.Collections.Generic;
using UnityEngine;

public class UniteController
{
    public event Action newRoundEvent;

    private GameManager _gameManager;
    private GridController _gridController;
    public UnitsModel unitsModel;
    private Data _data;

    private GameObject _groupBlackArmy, _groupWhiteArmy;

    private int _spawnPosForPlayer = 0, _spawnPosForEnemy = 0;
    public GameObject selectedPlayerUnit, selectedEnemyUnit;
    public List<UnitsModel> playerUnits, enemyUnits;
    public bool isEnemySelected, isPlayerSelected;

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

        playerUnits = new List<UnitsModel>();
        enemyUnits = new List<UnitsModel>();

        isEnemySelected = false;
        isPlayerSelected = false;

        _spawnPosForEnemy = _gridController.tiles.Count - 1;
    }

    public void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(_gameManager.ray, out hit, 100, LayerMask.GetMask("WhiteArmy")))
        {
            GameObject enemySelection = hit.transform.gameObject;
            Renderer enemyUnitRenderer = enemySelection.GetComponent<Renderer>();
            if (enemyUnitRenderer != null)
            {
                selectedEnemyUnit = enemySelection;
                for (int i = 0; i < enemyUnits.Count; i++)
                {
                    if (enemyUnits[i].unitObject == selectedEnemyUnit)
                    {
                        isEnemySelected = true;
                        selectedEnemyUnit = enemyUnits[i].unitObject;
                    }
                }
            }
        }

        if (isPlayerSelected)
        {
            MovingOnHighlightingTiles();
        }
    }

    public void SelectPlayerUnitEventHandler()
    {
        RaycastHit hit;

        if (Physics.Raycast(_gameManager.ray, out hit, 100, LayerMask.GetMask("BlackArmy")))
        {
            GameObject playerSelection = hit.transform.gameObject;
            selectedPlayerUnit = playerSelection;

            for (int i = 0; i < playerUnits.Count; i++)
            {
                if (playerUnits[i].unitObject == selectedPlayerUnit)
                {
                    if (playerUnits[i].isUnitCanMove)
                    {
                        _gridController.HideAvailableTile();
                        isPlayerSelected = true;
                        selectedPlayerUnit = playerUnits[i].unitObject;
                        _gridController.HighlightAvailableTiles(selectedPlayerUnit.transform.position, playerUnits[i].moveDistance);
                    }
                    else
                    {
                        _gridController.HideAvailableTile();
                        isPlayerSelected = false;
                    }
                }
            }
        }
    }

    private void MovingOnHighlightingTiles()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (isEnemySelected && enemyUnits.Count != 0)
            {
                if (selectedEnemyUnit.transform.position == _gridController.availableSelectionTile.transform.position)
                {
                    TakeDamageToEnemy(selectedPlayerUnit, selectedEnemyUnit);
                    foreach (var item in playerUnits)
                    {
                        if (item.unitObject == selectedPlayerUnit) item.isUnitCanMove = false;
                    }
                    isPlayerSelected = false;
                    _gridController.HideAvailableTile();
                    return;
                }
            }
            if (selectedPlayerUnit.transform.position != _gridController.availableSelectionTile.transform.position)
            {
                selectedPlayerUnit.transform.position = new Vector3(_gridController.availableSelectionTile.transform.position.x, 0, _gridController.availableSelectionTile.transform.position.z);
                foreach (var item in playerUnits)
                {
                    if (item.unitObject == selectedPlayerUnit) item.isUnitCanMove = false;
                }
                isPlayerSelected = false;
                _gridController.HideAvailableTile();
            }
        }
    }

    public void SpawnPlayerUnit(Enums.UniteType type)
    {
        Vector3 pos = new Vector3(_gridController.tiles[_spawnPosForPlayer].transform.position.x, 0,
                                    _gridController.tiles[_spawnPosForPlayer].transform.position.z);

        playerUnits.Add(new UnitsModel(_data.allUnits.Find(x => x.unitType == type), pos, _groupBlackArmy.transform, Enums.PlayerType.BlackArmy));
        foreach (var item in playerUnits)
        {
            item.unitObject.GetComponent<MouseEventsArgs>().PlayerUnitSelectEvent += SelectPlayerUnitEventHandler;
        }
        _spawnPosForPlayer += 1;
    }

    public void SpawnEnemyUnit(Enums.UniteType type)
    {
        Vector3 pos = new Vector3(_gridController.tiles[_spawnPosForEnemy].transform.position.x, 0,
                                    _gridController.tiles[_spawnPosForEnemy].transform.position.z);

        enemyUnits.Add(new UnitsModel(_data.allUnits.Find(x => x.unitType == type), pos, _groupWhiteArmy.transform, Enums.PlayerType.WhiteArmy));
        _spawnPosForEnemy -= 1;
    }

    public int GetUnitCostByUnitType(Enums.UniteType type)
    {
        return _data.allUnits.Find(x => x.unitType == type).unitCost;
    }

    public void TakeDamageToEnemy(GameObject attackingObject, GameObject targetObject)
    {
        for (int i = 0; i < playerUnits.Count; i++)
        {
            if (playerUnits[i].unitObject == attackingObject)
            {
                for (int j = 0; j < enemyUnits.Count; j++)
                {
                    if (enemyUnits[j].unitObject == targetObject)
                    {
                        enemyUnits[j].health -= playerUnits[i].damage;
                        enemyUnits[j].healthBar.transform.localScale = new Vector3(enemyUnits[j].health, 1, 1);
                        if (enemyUnits[j].health <= 0)
                        {
                            Disspose(enemyUnits[j].unitObject);
                            enemyUnits.Remove(enemyUnits[j]);
                            isEnemySelected = false;
                        }
                    }
                }
            }
        }
    }

    private void Disspose(GameObject unit) // Destroy game object
    {
        GameObject.Destroy(unit);
    }

    public void PlayNewRound()
    {
        for (int i = 0; i < playerUnits.Count; i++)
        {
            playerUnits[i].isUnitCanMove = true;
        }
        newRoundEvent?.Invoke();
    }
}