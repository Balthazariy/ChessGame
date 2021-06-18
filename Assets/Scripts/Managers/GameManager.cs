using System;
using UnityEngine;

public class GameManager
{
    public GridController gridController;
    public UniteController uniteController;
    private GameObject _groupGame;
    public Camera mainCamera;
    public int gold;
    public bool isGameStart;
    private Data _unitData;

    public GameManager()
    {
        _unitData = new Data();
        _groupGame = GameObject.Find("Game").gameObject;
        gridController = new GridController(_groupGame);
        uniteController = new UniteController(_groupGame, _unitData);
        mainCamera = Camera.main;
    }

    public void Start()
    {
        isGameStart = false;
        gold = 2000;
        gridController.Start();
        uniteController.Start();
    }

    public void Update()
    {
        if (isGameStart)
        {
            gridController.Update();
            uniteController.Update();
        }
    }

    public void BuyAUnit(Enums.UniteType type)
    {
        uniteController.GetUnitCostByUnitType(type);
        if (uniteController.unitsModel.unitCost <= gold)
        {
            gold -= uniteController.unitsModel.unitCost;
            // uniteController.SpawnEnemyUnit(type);
            uniteController.SpawnPlayerUnit(type);
        }
    }

    public void SetPlayer()
    {
        Debug.Log("ChangePlayer");
    }
}
