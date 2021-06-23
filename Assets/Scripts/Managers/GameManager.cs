using UnityEngine;

public class GameManager
{
    public GridController gridController;
    public UniteController uniteController;
    public Camera mainCamera;
    public int gold;
    public bool isGameStart;
    private GameObject _groupGame;
    public Data unitData;

    public Ray ray;

    public GameManager()
    {
        unitData = new Data();
        _groupGame = GameObject.Find("Game").gameObject;
        gridController = new GridController(_groupGame);
        uniteController = new UniteController(_groupGame, unitData);
        mainCamera = Camera.main;
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
        if (isGameStart)
        {
            gridController.Update();
            ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        }
    }

    public void BuyAUnit(Enums.UniteType type)
    {
        if (uniteController.GetUnitCostByUnitType(type) <= gold)
        {
            gold -= uniteController.GetUnitCostByUnitType(type);
            // uniteController.SpawnEnemyUnit(type);
            uniteController.SpawnPlayerUnit(type);
        }
    }
}
