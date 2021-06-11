using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
    public GameObject _game;
    public GridController gridController;
    public UniteController uniteController;
    public Camera mainCamera;
    private Vector2Int _currentHover;

    public int gold;

    public bool isGameStart;

    public GameManager()
    {
        _game = GameObject.Find("Game").gameObject;

        mainCamera = Camera.main;
        gridController = new GridController(_game);
        uniteController = new UniteController(_game);
    }

    public void Start()
    {
        isGameStart = false;
        gold = 20;

        gridController.Start();
        uniteController.Start();
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

            }

            if(Input.GetMouseButtonUp(0))
            {

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
        uniteController.SetUniteType(type);
        if(uniteController.unitsModel.unitCost > gold) Debug.Log("Not enight money");
        if(uniteController.unitsModel.unitCost <= gold) 
        {
            gold -= uniteController.unitsModel.unitCost;
            Debug.Log("You spend: " + uniteController.unitsModel.unitCost);
            Debug.Log(gold + "-------------");
            uniteController.SpawnUnit(type);
        }
    }
}
