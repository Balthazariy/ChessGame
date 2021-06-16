using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController
{
    public const int gridRows = 25, gridColumns = 25;
    private const int _tileSize = 1;
    private GameObject _gridParent;

    private GameObject _tilePrefab, _singleTile;
    public List<GameObject> tilesPositions;

    private GameManager _gameManager;
    private Material _highlightAvailableTilesMat;
    public Renderer renderer;
    public int indexOfTile = 0;

    public GridController(GameObject parent)
    {
        _gridParent = parent.transform.Find("Group_Grid").gameObject;
    }

    public void Start()
    {
        _gameManager = Main.Instance.gameManager;
        _tilePrefab = Resources.Load<GameObject>("Prefabs/Tile");
        _highlightAvailableTilesMat = Resources.Load<Material>("Materials/HighliteAvailableTilesMat");
        tilesPositions = new List<GameObject>();
        GenerateTiles();
    }

    public void GenerateTiles()
    {
        for (int x = 0; x < gridRows; x++)
        {
            for (int y = 0; y < gridColumns; y++)
            {
                _singleTile = Object.Instantiate(_tilePrefab, new Vector3(x * _tileSize, 0, y * _tileSize), Quaternion.identity, _gridParent.transform);
                _singleTile.layer = LayerMask.NameToLayer("Tile");
                _singleTile.name = $"Tile X:{x} Y:{y}";
                tilesPositions.Add(_singleTile);
            }
        }
    }

    public void HighlightAvailableTiles(GameObject selectedUnit, int moveDistance)
    {
        Vector3 currentPosOfSelectedUnit = selectedUnit.transform.position;

        List<GameObject> availableTiles = new List<GameObject>();

        // availableTiles.Add(tilesPositions[indexOfTile]);
        // availableTiles.Add(tilesPositions[indexOfTile]);
        // availableTiles.Add(tilesPositions[indexOfTile]);
        // availableTiles.Add(tilesPositions[indexOfTile]);

        // availableTiles.Add(tilesPositions[indexOfTile]);
        // availableTiles.Add(tilesPositions[indexOfTile]);
        // availableTiles.Add(tilesPositions[indexOfTile]);
        // availableTiles.Add(tilesPositions[indexOfTile]);
    }
}