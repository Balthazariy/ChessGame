using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
    public GridController gridController;
    private UniteController _uniteController;
    
    public GameObject _groupGame;
    public Camera mainCamera;
    private Vector2Int _currentHover;

    private Vector3 _mouseStartPos, _mouseEndPos;

    public int gold;

    public bool isGameStart;

    public GameManager()
    {
        _groupGame = GameObject.Find("Game").gameObject;
        gridController = new GridController(_groupGame);
        _uniteController = new UniteController(_groupGame);
        mainCamera = Camera.main;
    }

    public void Start()
    {
        isGameStart = false;
        gold = 20;
        gridController.Start();
        _uniteController.Start();
    }

    public void Update()
    {
        RaycastHit raycastHit;
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out raycastHit, 100, LayerMask.GetMask("Tile", "Hover")))
        {
            Vector2Int hitPosition = LookUpTileIndex(raycastHit.transform.gameObject);

            if(_currentHover == Vector2Int.one * -1)
            {
                _currentHover = hitPosition;
                gridController.tiles[hitPosition.x, hitPosition.y].layer = LayerMask.NameToLayer("Hover");
            }
            
            if(_currentHover != hitPosition)
            {
                gridController.tiles[_currentHover.x, _currentHover.y].layer = LayerMask.NameToLayer("Tile");
                gridController.tiles[_currentHover.x, _currentHover.y].GetComponent<MeshRenderer>().material = gridController.tileMaterial;
                _currentHover = hitPosition;
                gridController.tiles[hitPosition.x, hitPosition.y].layer = LayerMask.NameToLayer("Hover");
                gridController.tiles[hitPosition.x, hitPosition.y].GetComponent<MeshRenderer>().material = gridController.howerTileMaterial;
            }

            if(Input.GetMouseButtonDown(0))
            {

                if (raycastHit.transform.position == _uniteController.unitsModel.unitObject.transform.position)
                {
                    Debug.Log("Its Unit");
                    _mouseStartPos = _uniteController.unitsModel.unitObject.transform.position;
                    Debug.Log(_mouseStartPos);
                }
            }

            if(Input.GetMouseButtonUp(0))
            {
                _mouseEndPos = raycastHit.transform.position;
                
                // _uniteController.unitsModel.unitObject.transform.Translate(_mouseEndPos);
                // _uniteController.unitsModel.unitObject.transform.position = Vector3.Lerp(_mouseStartPos, _mouseEndPos, 0.25f);
            }
        }
        else
        {
            if(_currentHover != Vector2Int.one * -1)
            {
                gridController.tiles[_currentHover.x, _currentHover.y].layer = LayerMask.NameToLayer("Tile");
                _currentHover = Vector2Int.one * -1;
            }
        }
    }

    public Vector2Int LookUpTileIndex(GameObject hitInfo)
    {
        for(int i = 0; i < GridController.gridRows; i++)
            for(int j = 0; j < GridController.gridColumns; j++)
                if(gridController.tiles[i, j] == hitInfo)
                    return new Vector2Int(i, j);
        return Vector2Int.one * -1;
    }

    public void BuyAUnit(Enums.UniteType type)
    {
        _uniteController.SetUniteType(type);
        if(_uniteController.unitsModel.unitCost > gold) Debug.Log("Not enight money");
        if(_uniteController.unitsModel.unitCost <= gold) 
        {
            gold -= _uniteController.unitsModel.unitCost;
            Debug.Log("You spend: " + _uniteController.unitsModel.unitCost);
            Debug.Log(gold + "-------------");
            _uniteController.SpawnUnit(type);
        }
    }
}
