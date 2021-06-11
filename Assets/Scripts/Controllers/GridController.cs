using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController
{
    public const int gridRows = 25;
    public const int gridColumns = 25;
    private const int _tileSize = 3;

    private GameManager _gameManager;
    private GameObject _gridParent;
    private MeshRenderer _meshRenderer;
    public Material tileMaterial, howerTileMaterial;

    public GameObject tileObject;
    public GameObject[,] tiles;
    private GameObject _tilePrefab;

    public GridController(GameObject parent)
    {
        _gridParent = parent.transform.Find("Group_Grid").gameObject;
    }

    public void Start()
    {
        _gameManager = Main.Instance.gameManager;

        tileMaterial = Resources.Load<Material>("Materials/TileMaterial");
        howerTileMaterial = Resources.Load<Material>("Materials/HowerTileMaterial");
        _tilePrefab = Resources.Load<GameObject>("Prefabs/Tile");

        _meshRenderer = _tilePrefab.GetComponent<MeshRenderer>();

        GenerateTiles(_tileSize, gridRows, gridColumns);
    }

    public void GenerateTiles(int size, int rows, int columns)
    {
        tiles = new GameObject[rows, columns];
        for(int i = 0; i < rows; i++)
        {
            for(int j = 0; j < columns; j++)
            {
                tileObject = Object.Instantiate(_tilePrefab, new Vector3(i * size, 0, j * size), Quaternion.identity, _gridParent.transform);
                _meshRenderer.material = tileMaterial;
                tiles[i, j] = tileObject;
            }
        }
    }


}
