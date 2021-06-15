using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
    public GridController gridController;
    private UniteController _uniteController;
    
    public GameObject _groupGame;
    public Camera mainCamera;
    public int gold;
    public bool isGameStart, isUniteSelected;

    private Transform _selectionTile, _selectionUnit;
    private Material _defaultTile, _highliteTile;
    private GameObject selectedUnit;

    public GameManager()
    {
        _groupGame = GameObject.Find("Game").gameObject;
        gridController = new GridController(_groupGame);
        _uniteController = new UniteController(_groupGame);
        mainCamera = Camera.main;
        _defaultTile = Resources.Load<Material>("Materials/DefaultTileMat");
        _highliteTile = Resources.Load<Material>("Materials/HighliteTileMat");
    }

    public void Start()
    {
        isGameStart = false;
        isUniteSelected = false;
        gold = 20;
        gridController.Start();
        _uniteController.Start();
    }

    public void Update()
    {
        UpdateTiles();
        UpdateUnits();
    }

    public void UpdateTiles()
    {
        if(_selectionTile != null)
        {
            var selectionRender = _selectionTile.GetComponent<Renderer>();
            selectionRender.material = _defaultTile;
            _selectionTile = null;
        }

        var ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, 100, LayerMask.GetMask("Tile")))
        {
            var selection = hit.transform;
            var selectionRender = selection.GetComponent<Renderer>();
            if(selectionRender != null)
            {
                selectionRender.material = _highliteTile;
                if(Input.GetMouseButtonDown(0))
                {
                    if(isUniteSelected)
                    {
                        selectedUnit.transform.position = new Vector3(selection.position.x, 0, selection.position.z);
                        isUniteSelected = false;
                        if(!isUniteSelected)
                        {
                            mainCamera.transform.position = new Vector3(12, 24, 11);
                            mainCamera.transform.eulerAngles = new Vector3(90, 0, 0);
                        }
                    }
                }
            }
            _selectionTile = selection;
        }
    }

    public void UpdateUnits()
    {
        var ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, 100, LayerMask.GetMask("PlayerUnit")))
        {
            var selection = hit.transform.gameObject;
            var selectionRender = selection.GetComponent<Renderer>();
            if(selectionRender != null)
            {
                if(Input.GetMouseButtonDown(0))
                {
                    selectedUnit = selection;
                    mainCamera.transform.position = new Vector3(selectedUnit.transform.position.x, 10, selectedUnit.transform.position.z - 4);
                    isUniteSelected = true;
                }
            }
            _selectionUnit = selection.transform;
        }
    }

    public void BuyAUnit(Enums.UniteType type)
    {
        _uniteController.SetUniteType(type);
        if(_uniteController.unitsModel.unitCost <= gold) 
        {
            gold -= _uniteController.unitsModel.unitCost;
            _uniteController.SpawnUnit(type);
        }
    }
}
