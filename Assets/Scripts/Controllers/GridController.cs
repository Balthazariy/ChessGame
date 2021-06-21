using System.Collections.Generic;
using UnityEngine;

public class GridController
{
    private const int _gridRows = 25, _gridColumns = 25;
    private const int _tileSize = 1;

    private GameObject _gridParent;
    private GameManager _gameManager;
    private UniteController _unitController;

    private GameObject _tilePrefab, _singleTile;
    private Material _defaultTile, _highlightTile, _defaultAvailableTiles, _highlightAvailableTiles, _defaultLightTile, _defaultDarkTile;
    private Transform _selectionTile;
    public Transform availableSelectionTile;

    private GameObject[,] _tilesPositions;
    private List<GameObject> _highlight;
    public List<GameObject> tiles;
    private bool _darkPiece;

    public GridController(GameObject parent)
    {
        _gridParent = parent.transform.Find("Group_Grid").gameObject;
        _defaultTile = Resources.Load<Material>("Materials/DefaultTileMat");
        _highlightTile = Resources.Load<Material>("Materials/HighlightTileMat");
    }

    public void Start()
    {
        _gameManager = Main.Instance.gameManager;
        _unitController = Main.Instance.gameManager.uniteController;

        _tilePrefab = Resources.Load<GameObject>("Prefabs/Tile");
        _defaultAvailableTiles = Resources.Load<Material>("Materials/DefaultAvailableTilesMat");
        _highlightAvailableTiles = Resources.Load<Material>("Materials/HighliteAvailableTilesMat");
        _defaultLightTile = Resources.Load<Material>("Materials/DefaultLightMat");
        _defaultDarkTile = Resources.Load<Material>("Materials/DefaultDarkMat");

        tiles = new List<GameObject>();
        _tilesPositions = new GameObject[_gridRows, _gridColumns];
        _highlight = new List<GameObject>();

        _darkPiece = true;

        GenerateTiles();
    }

    public void Update()
    {
        // if (_selectionTile != null)
        // {
        //     Renderer tileRenderer = _selectionTile.GetComponent<Renderer>();
        //     tileRenderer.material = _defaultTile;
        //     _selectionTile = null;
        // }
        // RaycastHit hit;
        // if (Physics.Raycast(_gameManager.ray, out hit, 100, LayerMask.GetMask("Tile")))
        // {
        //     GameObject tileSelection = hit.transform.gameObject;
        //     Renderer tileRenderer = tileSelection.GetComponent<Renderer>();
        //     if (tileRenderer != null)
        //     {
        //         tileRenderer.material = _highlightTile;
        //     }
        //     _selectionTile = tileSelection.transform;
        // }

        // if (availableSelectionTile != null)
        // {
        //     Renderer availableTileRenderer = availableSelectionTile.GetComponent<Renderer>();
        //     availableTileRenderer.material = _defaultAvailableTiles;
        //     availableSelectionTile = null;
        // }
        // if (Physics.Raycast(_gameManager.ray, out hit, 100, LayerMask.GetMask("AvailableTile")))
        // {
        //     GameObject availableTileSelection = hit.transform.gameObject;
        //     Renderer availableTileRenderer = availableTileSelection.GetComponent<Renderer>();
        //     if (availableTileRenderer != null)
        //     {
        //         availableSelectionTile = availableTileSelection.transform;
        //         availableTileRenderer.material = _highlightAvailableTiles;
        //     }
        // }
    }

    private void GenerateTiles()
    {
        // for (int x = 0; x < _gridRows; x++)
        // {
        //     for (int y = 0; y < _gridColumns; y++)
        //     {
        //         _singleTile = GameObject.Instantiate(_tilePrefab, new Vector3(x * _tileSize, 0, y * _tileSize), Quaternion.identity, _gridParent.transform);
        //         _singleTile.layer = LayerMask.NameToLayer("Tile");
        //         _singleTile.name = $"Tile X:{x} Y:{y}";
        //         tiles.Add(_singleTile);
        //         _tilesPositions[x, y] = _singleTile;
        //     }
        // }

        for (int x = 0; x < _gridRows; x++)
        {
            for (int y = 0; y < _gridColumns; y++)
            {
                if (_darkPiece)
                {
                    _singleTile = GameObject.Instantiate(_tilePrefab, new Vector3(x * _tileSize, 0, y * _tileSize), Quaternion.identity, _gridParent.transform);
                    _singleTile.GetComponent<Renderer>().material = _defaultDarkTile;
                    _darkPiece = false;
                }
                else
                {
                    _singleTile = GameObject.Instantiate(_tilePrefab, new Vector3(x * _tileSize, 0, y * _tileSize), Quaternion.identity, _gridParent.transform);
                    _singleTile.GetComponent<Renderer>().material = _defaultLightTile;
                    _darkPiece = true;
                }
                _singleTile.layer = LayerMask.NameToLayer("Tile");
                _singleTile.name = $"Tile X:{x} Y:{y}";
                tiles.Add(_singleTile);
                _tilesPositions[x, y] = _singleTile;
            }
        }
    }

    public void HighlightAvailableTiles(Vector3 selectedUnitPos, int moveDistance)
    {
        int xPos = (int)selectedUnitPos.x;
        int zPos = (int)selectedUnitPos.z;

        for (int i = 0; i <= moveDistance; i++)
        {
            for (int j = 0; j <= moveDistance; j++)
            {
                if (xPos + i < _gridRows && zPos + j < _gridColumns)
                {
                    _highlight.Add(_tilesPositions[xPos + i, zPos + j]);
                }

                if (xPos - i >= 0 && zPos + j < _gridColumns)
                {
                    _highlight.Add(_tilesPositions[xPos - i, zPos + j]);
                }

                if (xPos + i < _gridRows && zPos - j >= 0)
                {
                    _highlight.Add(_tilesPositions[xPos + i, zPos - j]); // BAG
                }

                if (xPos - i >= 0 && zPos - j >= 0)
                {
                    _highlight.Add(_tilesPositions[xPos - i, zPos - j]); // BAG
                }
            }
        }

        foreach (var item in _highlight)
        {
            item.GetComponent<MeshRenderer>().material = _defaultAvailableTiles;
            item.layer = LayerMask.NameToLayer("AvailableTile");
        }
    }

    public void HideAvailableTile()
    {
        foreach (var item in _highlight)
        {
            item.GetComponent<MeshRenderer>().material = _defaultTile;
            item.layer = LayerMask.NameToLayer("Tile");
        }
        _highlight.Clear();
    }
}