using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
    public GridController gridController;
    private UniteController _uniteController;
    public GameObject groupGame;
    public Camera mainCamera;
    public int gold;
    public bool isGameStart, isUniteSelected, isEnemyTakeDamage;

    private Material _defaultTile, _highlightTile;
    private Transform _selectionTile;
    private GameObject _selectedPlayerUnit, _selectedEnemyUnit;

    public GameManager()
    {
        groupGame = GameObject.Find("Game").gameObject;
        gridController = new GridController(groupGame);
        _uniteController = new UniteController(groupGame);
        mainCamera = Camera.main;
        _defaultTile = Resources.Load<Material>("Materials/DefaultTileMat");
        _highlightTile = Resources.Load<Material>("Materials/HighlightTileMat");
    }

    public void Start()
    {
        isGameStart = false;
        isUniteSelected = false;
        isEnemyTakeDamage = false;
        gold = 2000;
        gridController.Start();
        _uniteController.Start();
    }

    public void Update()
    {
        if (isGameStart)
        {
            RaycastObjectsOnScene();
        }
    }

    public void RaycastObjectsOnScene()
    {
        int tempIndexOfTile = 0;
        if (_selectionTile != null)
        {
            Renderer tileRenderer = _selectionTile.GetComponent<Renderer>();
            tileRenderer.material = _defaultTile;
            _selectionTile = null;
        }
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100, LayerMask.GetMask("Tile")))
        {
            GameObject tileSelection = hit.transform.gameObject;
            Renderer tileRenderer = tileSelection.GetComponent<Renderer>();
            if (tileRenderer != null)
            {
                tempIndexOfTile = gridController.tilesPositions.IndexOf(tileSelection);
                tileRenderer.material = _highlightTile;
                if (Input.GetMouseButtonDown(0))
                {
                    if (isUniteSelected)
                    {
                        isUniteSelected = false;
                        _selectedPlayerUnit.transform.position = new Vector3(tileSelection.transform.position.x, 0, tileSelection.transform.position.z);
                        if (!isUniteSelected)
                        {
                            mainCamera.transform.position = new Vector3(12, 17, -1);
                            mainCamera.transform.eulerAngles = new Vector3(60, 0, 0);
                        }
                    }
                }
            }
            _selectionTile = tileSelection.transform;
        }

        if (Physics.Raycast(ray, out hit, 100, LayerMask.GetMask("PlayerUnit")))
        {
            GameObject playerSelection = hit.transform.gameObject;
            Renderer playerUnitRenderer = playerSelection.GetComponent<Renderer>();
            if (playerUnitRenderer != null)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Enum.TryParse(_selectedPlayerUnit.name, out Enums.UniteType type);
                    _uniteController.SetUniteType(type);
                    mainCamera.transform.position = new Vector3(_selectedPlayerUnit.transform.position.x, 12, _selectedPlayerUnit.transform.position.z);
                    mainCamera.transform.eulerAngles = new Vector3(90, 90, 0);
                    gridController.indexOfTile = tempIndexOfTile;
                    isUniteSelected = true;
                }
            }
            _selectedPlayerUnit = playerSelection;
        }

        if (Physics.Raycast(ray, out hit, 100, LayerMask.GetMask("EnemyUnit")))
        {
            GameObject enemySelection = hit.transform.gameObject;
            Renderer enemyUnitRenderer = enemySelection.GetComponent<Renderer>();
            if (enemyUnitRenderer != null)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Enum.TryParse(_selectedEnemyUnit.name, out Enums.UniteType type);
                    _uniteController.SetUniteType(type);
                    _uniteController.TakeDamage(_uniteController.unitsModel.damage, type);
                }
            }
            _selectedEnemyUnit = enemySelection;
        }
    }

    public void BuyAUnit(Enums.UniteType type)
    {
        _uniteController.SetUniteType(type);
        if (_uniteController.unitsModel.unitCost <= gold)
        {
            gold -= _uniteController.unitsModel.unitCost;
            _uniteController.SpawnUnit(type);
        }
    }
}
