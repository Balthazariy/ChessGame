using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileModel
{
    public GameObject singleTile;
    public MeshRenderer meshRender, highlightRender;

    public TileModel(Vector3 pos, GameObject parent, Material material)
    {
        singleTile = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Tile"), pos, Quaternion.identity, parent.transform);
        meshRender = singleTile.GetComponent<MeshRenderer>();
        highlightRender = singleTile.GetComponent<MeshRenderer>();
    }

    public void ChangeMaterialOfTile(Material material)
    {
        highlightRender.material = material;
    }
}
