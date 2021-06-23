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
    private Material _defaultTile, _highlightTile, _defaultAvailableTiles, _highlightAvailableTiles, _defaultDarkTile, _defaultLightTile;
    private Transform _selectionTile, _availableSelectionTile;

    private GameObject[,] _tilePos;
    public List<GameObject> highlight;
    public List<GameObject> tiles;
    private List<Material> _tileMaterials;

    private MeshRenderer _renderer;

    public GridController(GameObject parent)
    {
        _gridParent = parent.transform.Find("Group_Grid").gameObject;
        _tilePrefab = Resources.Load<GameObject>("Prefabs/Tile");
        _defaultTile = Resources.Load<Material>("Materials/DefaultTileMat");
        _highlightTile = Resources.Load<Material>("Materials/HighlightTileMat");
        _defaultAvailableTiles = Resources.Load<Material>("Materials/DefaultAvailableTilesMat");
        _highlightAvailableTiles = Resources.Load<Material>("Materials/HighliteAvailableTilesMat");
        _defaultDarkTile = Resources.Load<Material>("Materials/DefaultDarkTileMat");
        _defaultLightTile = Resources.Load<Material>("Materials/DefaultLightTileMat");
        _renderer = _tilePrefab.GetComponent<MeshRenderer>();
    }

    public void Start()
    {
        _gameManager = Main.Instance.gameManager;
        _unitController = Main.Instance.gameManager.uniteController;

        tiles = new List<GameObject>();
        _tilePos = new GameObject[_gridRows, _gridColumns];
        highlight = new List<GameObject>();
        _tileMaterials = new List<Material>();

        GenerateTiles();
    }

    public void Update()
    {
        if (_availableSelectionTile != null)
        {
            Renderer availableTileRenderer = _availableSelectionTile.GetComponent<Renderer>();
            availableTileRenderer.material = _defaultAvailableTiles;
            _availableSelectionTile = null;
        }
        RaycastHit hit;
        if (Physics.Raycast(_gameManager.ray, out hit, 100, LayerMask.GetMask("AvailableTile")))
        {
            GameObject availableTileSelection = hit.transform.gameObject;
            Renderer availableTileRenderer = availableTileSelection.GetComponent<Renderer>();
            if (availableTileRenderer != null)
            {
                _availableSelectionTile = availableTileSelection.transform;
                availableTileRenderer.material = _highlightAvailableTiles;
                if (Input.GetMouseButtonDown(0))
                {
                    if (_unitController.isEnemySelected && _unitController.enemyUnits.Count != 0)
                    {
                        if (_unitController.selectedEnemyUnit.transform.position == _availableSelectionTile.transform.position)
                        {
                            _unitController.TakeDamageToEnemy(_unitController.selectedPlayerUnit, _unitController.selectedEnemyUnit);
                            foreach (var item in _unitController.playerUnits)
                            {
                                if (item.unitObject == _unitController.selectedPlayerUnit) item.isUnitCanMove = false;
                            }
                            _unitController.isPlayerSelected = false;
                            HideAvailableTile();
                            return;
                        }
                    }
                    if (_unitController.selectedPlayerUnit.transform.position != _availableSelectionTile.transform.position)
                    {
                        _unitController.selectedPlayerUnit.transform.position = new Vector3(_availableSelectionTile.transform.position.x, 0, _availableSelectionTile.transform.position.z);
                        foreach (var item in _unitController.playerUnits)
                        {
                            if (item.unitObject == _unitController.selectedPlayerUnit) item.isUnitCanMove = false;
                        }
                        _unitController.isPlayerSelected = false;
                        HideAvailableTile();
                    }
                }
            }
        }
    }

    private void GenerateTiles()
    {
        bool isDarkTile = true;
        for (int x = 0; x < _gridRows; x++)
        {
            for (int y = 0; y < _gridColumns; y++)
            {
                if (isDarkTile)
                {
                    _renderer.material = _defaultDarkTile;
                    _singleTile = GameObject.Instantiate(_tilePrefab, new Vector3(x * _tileSize, 0, y * _tileSize), Quaternion.identity, _gridParent.transform);
                    isDarkTile = false;
                }
                else
                {
                    _renderer.material = _defaultLightTile;
                    _singleTile = GameObject.Instantiate(_tilePrefab, new Vector3(x * _tileSize, 0, y * _tileSize), Quaternion.identity, _gridParent.transform);
                    isDarkTile = true;
                }
                _singleTile.layer = LayerMask.NameToLayer("Tile");
                _singleTile.name = $"Tile X:{x} Y:{y}";
                tiles.Add(_singleTile);
                _tilePos[x, y] = _singleTile;
                _tileMaterials.Add(_singleTile.GetComponent<MeshRenderer>().material);
            }
        }
    }

    public void HighlightAvailableTiles(Vector3 selectedUnitPos, int moveDistance, Enums.UniteType unitType)
    {
        int xPos = (int)selectedUnitPos.x;
        int zPos = (int)selectedUnitPos.z;

        for (int i = 1; i <= moveDistance; i++)
        {
            for (int j = 1; j <= moveDistance; j++)
            {
                if (unitType == Enums.UniteType.Pawn) // Pawn Movement
                {
                    if (xPos + i < _gridRows)
                    {
                        highlight.Add(_tilePos[xPos + i, zPos]);
                    }

                    if (xPos - i >= 0)
                    {
                        highlight.Add(_tilePos[xPos - i, zPos]);
                    }

                    if (zPos - j >= 0)
                    {
                        highlight.Add(_tilePos[xPos, zPos - j]);
                    }

                    if (zPos + j < _gridColumns)
                    {
                        highlight.Add(_tilePos[xPos, zPos + j]);
                    }
                }

                if (unitType == Enums.UniteType.Knight) // Knight movement
                {
                    if (i == moveDistance)
                    {
                        if (xPos - i >= 0 && zPos + 1 < _gridColumns)
                        {
                            highlight.Add(_tilePos[xPos - i, zPos + 1]);
                        }

                        if (xPos - i >= 0 && zPos - 1 >= 0)
                        {
                            highlight.Add(_tilePos[xPos - i, zPos - 1]);
                        }

                        if (xPos + i < _gridRows && zPos + 1 < _gridColumns)
                        {
                            highlight.Add(_tilePos[xPos + i, zPos + 1]);
                        }

                        if (xPos + i < _gridRows && zPos - 1 >= 0)
                        {
                            highlight.Add(_tilePos[xPos + i, zPos - 1]);
                        }
                    }

                    if (j == moveDistance)
                    {
                        if (zPos - j >= 0 && xPos + 1 < _gridRows)
                        {
                            highlight.Add(_tilePos[xPos + 1, zPos - j]);
                        }

                        if (zPos - j >= 0 && xPos - 1 >= 0)
                        {
                            highlight.Add(_tilePos[xPos - 1, zPos - j]);
                        }

                        if (zPos + j < _gridColumns && xPos + 1 < _gridRows)
                        {
                            highlight.Add(_tilePos[xPos + 1, zPos + j]);
                        }

                        if (zPos + j < _gridColumns && xPos - 1 >= 0)
                        {
                            highlight.Add(_tilePos[xPos - 1, zPos + j]);
                        }
                    }
                }

                if (unitType == Enums.UniteType.Bishop) // Bishop movement
                {
                    if (xPos + i < _gridRows && zPos + i < _gridColumns)
                    {
                        highlight.Add(_tilePos[xPos + i, zPos + i]);
                    }

                    if (xPos - i >= 0 && zPos - i >= 0)
                    {
                        highlight.Add(_tilePos[xPos - i, zPos - i]);
                    }

                    if (xPos - j >= 0 && zPos + j < _gridColumns)
                    {
                        highlight.Add(_tilePos[xPos - j, zPos + j]);
                    }

                    if (xPos + j < _gridRows && zPos - j >= 0)
                    {
                        highlight.Add(_tilePos[xPos + j, zPos - j]);
                    }
                }

                if (unitType == Enums.UniteType.Rook) // Rook movement
                {
                    if (xPos + i < _gridRows)
                    {
                        highlight.Add(_tilePos[xPos + i, zPos]);
                    }

                    if (xPos - i >= 0)
                    {
                        highlight.Add(_tilePos[xPos - i, zPos]);
                    }

                    if (zPos - j >= 0)
                    {
                        highlight.Add(_tilePos[xPos, zPos - j]);
                    }

                    if (zPos + j < _gridColumns)
                    {
                        highlight.Add(_tilePos[xPos, zPos + j]);
                    }
                }

                if (unitType == Enums.UniteType.Queen) // Queen movement
                {
                    if (xPos + i < _gridRows)
                    {
                        highlight.Add(_tilePos[xPos + i, zPos]);
                    }

                    if (xPos - i >= 0)
                    {
                        highlight.Add(_tilePos[xPos - i, zPos]);
                    }

                    if (zPos - j >= 0)
                    {
                        highlight.Add(_tilePos[xPos, zPos - j]);
                    }

                    if (zPos + j < _gridColumns)
                    {
                        highlight.Add(_tilePos[xPos, zPos + j]);
                    }

                    if (xPos + i < _gridRows && zPos + i < _gridColumns)
                    {
                        highlight.Add(_tilePos[xPos + i, zPos + i]);
                    }

                    if (xPos - i >= 0 && zPos - i >= 0)
                    {
                        highlight.Add(_tilePos[xPos - i, zPos - i]);
                    }

                    if (xPos - j >= 0 && zPos + j < _gridColumns)
                    {
                        highlight.Add(_tilePos[xPos - j, zPos + j]);
                    }

                    if (xPos + j < _gridRows && zPos - j >= 0)
                    {
                        highlight.Add(_tilePos[xPos + j, zPos - j]);
                    }
                }
            }
        }

        for (int i = 0; i < highlight.Count; i++)
        {
            highlight[i].GetComponent<MeshRenderer>().material = _defaultAvailableTiles;
            highlight[i].layer = LayerMask.NameToLayer("AvailableTile");
        }
    }

    public void HideAvailableTile()
    {
        for (int i = 0; i < highlight.Count; i++)
        {
            highlight[i].GetComponent<MeshRenderer>().material = _tileMaterials[i];
            highlight[i].layer = LayerMask.NameToLayer("Tile");
        }

        for (int i = 0; i < tiles.Count; i++)
        {
            tiles[i].GetComponent<MeshRenderer>().material = _tileMaterials[i];
        }
        highlight.Clear();
    }
}