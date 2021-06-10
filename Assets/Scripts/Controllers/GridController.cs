using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController
{
    private GameManager _gameManager;
    private const int _gridRows = 25;
    private const int _gridColumns = 25;
    private const int _tileSize = 3;
    private GameObject[,] _tiles;
    private GameObject _gridParent;
    private GameObject _tileObject;
    private Material _tileMaterial, _howerTileMaterial;
    private Vector2Int _currentHover;
    private MeshRenderer _meshRenderer;

    private Camera _mainCamera;

    public GridController(GameObject parent)
    {
        _gridParent = parent.transform.Find("Group_Grid").gameObject;
        _mainCamera = Camera.main;
    }

    public void Start()
    {
        _gameManager = Main.Instance.gameManager;

        _tileMaterial = Resources.Load<Material>("Materials/TileMaterial");
        _howerTileMaterial = Resources.Load<Material>("Materials/HowerTileMaterial");

        GenerateTiles(_tileSize, _gridRows, _gridColumns);
    }

    public void Update()
    {
        RaycastHit raycastHit;
        Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out raycastHit, 100, LayerMask.GetMask("Tile", "Hover")))
        {
            Vector2Int hitPosition = LookUpTileIndex(raycastHit.transform.gameObject);

            if(_currentHover == Vector2Int.one * -1)
            {
                _currentHover = hitPosition;
                _tiles[hitPosition.x, hitPosition.y].layer = LayerMask.NameToLayer("Hover");
            }

            if(_currentHover != hitPosition)
            {
                _tiles[_currentHover.x, _currentHover.y].layer = LayerMask.NameToLayer("Tile");
                _tiles[_currentHover.x, _currentHover.y].GetComponent<MeshRenderer>().material = _tileMaterial;
                _currentHover = hitPosition;
                _tiles[hitPosition.x, hitPosition.y].layer = LayerMask.NameToLayer("Hover");
                _tiles[hitPosition.x, hitPosition.y].GetComponent<MeshRenderer>().material = _howerTileMaterial;
            }
        }
        else
        {
            if(_currentHover != Vector2Int.one * -1)
            {
                _tiles[_currentHover.x, _currentHover.y].layer = LayerMask.NameToLayer("Tile");
                _currentHover = Vector2Int.one * -1;
            }
        }
    }

    public void GenerateTiles(int size, int rows, int columns)
    {
        _tiles = new GameObject[rows, columns];
        for(int i = 0; i < rows; i++)
        {
            for(int j = 0; j < columns; j++)
            {
                _tiles[i, j] = GenerateSingleTile(size, i, j);
            }
        }
    }

    public GameObject GenerateSingleTile(int size, int x, int y)
    {
        _tileObject = new GameObject(string.Format("X:{0}, Y:{1}", x, y));
        _tileObject.transform.SetParent(_gridParent.transform);

        Mesh mesh = new Mesh();
        _tileObject.AddComponent<MeshFilter>().mesh = mesh;
        _tileObject.AddComponent<MeshRenderer>().material = _tileMaterial;

        Vector3[] vertices = new Vector3[4];
        vertices[0] = new Vector3(x * size, 0, y * size);
        vertices[1] = new Vector3(x * size, 0, (y + 1) * size);
        vertices[2] = new Vector3((x + 1) * size, 0, y * size);
        vertices[3] = new Vector3((x + 1) * size, 0, (y + 1) * size);

        int[] tris = new int[] { 0, 1, 2, 1, 3, 2 };

        mesh.vertices = vertices;
        mesh.triangles = tris;
        mesh.RecalculateNormals();

        _tileObject.layer = LayerMask.NameToLayer("Tile");
        _tileObject.AddComponent<BoxCollider>();

        return _tileObject;
    }

    public Vector2Int LookUpTileIndex(GameObject hitInfo)
    {
        for(int i = 0; i < _gridRows; i++)
            for(int j = 0; j < _gridColumns; j++)
                if(_tiles[i, j] == hitInfo)
                    return new Vector2Int(i, j);
        return Vector2Int.one * -1;
    }
}
