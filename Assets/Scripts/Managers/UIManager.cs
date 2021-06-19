using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager
{
    private GameManager _gameManager;

    private Canvas _canvas;
    private Button _marketBtn, _closeMarketBtn, _finishPreparingBtn, _spawnNewEnemyBtn, _buyPawnBtn, _buyKnightBtn, _buyBishopBtn, _buyRookBtn, _buyQueenBtn;
    private Text _goldText;
    private GameObject _groupMarket;

    public UIManager()
    {
        _canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        _groupMarket = _canvas.transform.Find("Group_Market").gameObject;
        _goldText = _groupMarket.transform.Find("Text_GoldAmount").GetComponent<Text>();

        _marketBtn = _canvas.transform.Find("Btn_Market").GetComponent<Button>();
        _closeMarketBtn = _canvas.transform.Find("Btn_CloseMarket").GetComponent<Button>();
        _finishPreparingBtn = _canvas.transform.Find("Btn_FinishPreparing").GetComponent<Button>();
        _spawnNewEnemyBtn = _canvas.transform.Find("Btn_SpawnNewEnemy").GetComponent<Button>();

        _buyPawnBtn = _groupMarket.transform.Find("Group_Pawn/Btn_BuyPawn").GetComponent<Button>();
        _buyKnightBtn = _groupMarket.transform.Find("Group_Knight/Btn_BuyKnight").GetComponent<Button>();
        _buyBishopBtn = _groupMarket.transform.Find("Group_Bishop/Btn_BuyBishop").GetComponent<Button>();
        _buyRookBtn = _groupMarket.transform.Find("Group_Rook/Btn_BuyRook").GetComponent<Button>();
        _buyQueenBtn = _groupMarket.transform.Find("Group_Queen/Btn_BuyQueen").GetComponent<Button>();

        _marketBtn.onClick.AddListener(OpenMarket);
        _closeMarketBtn.onClick.AddListener(CloseMarket);
        _finishPreparingBtn.onClick.AddListener(StartGame);
        _spawnNewEnemyBtn.onClick.AddListener(SpawnNewEnemy);

        _buyPawnBtn.onClick.AddListener(BuyPawn);
        _buyKnightBtn.onClick.AddListener(BuyKnight);
        _buyBishopBtn.onClick.AddListener(BuyBishop);
        _buyRookBtn.onClick.AddListener(BuyRook);
        _buyQueenBtn.onClick.AddListener(BuyQueen);
    }

    public void Start()
    {
        _gameManager = Main.Instance.gameManager;
        _groupMarket.SetActive(false);
        _closeMarketBtn.gameObject.SetActive(false);
        _finishPreparingBtn.gameObject.SetActive(false);
        _marketBtn.gameObject.SetActive(true);
        _spawnNewEnemyBtn.gameObject.SetActive(false);
    }

    public void Update()
    {
        _goldText.text = "Gold: " + _gameManager.gold.ToString();
    }

    private void OpenMarket()
    {
        _groupMarket.SetActive(true);
        _marketBtn.gameObject.SetActive(false);
        _closeMarketBtn.gameObject.SetActive(true);
    }

    private void CloseMarket()
    {
        _groupMarket.SetActive(false);
        _closeMarketBtn.gameObject.SetActive(false);
        _finishPreparingBtn.gameObject.SetActive(true);
        _gameManager.isGameStart = true;
        _spawnNewEnemyBtn.gameObject.SetActive(true);
    }

    private void StartGame()
    {
        _finishPreparingBtn.gameObject.SetActive(false);
        _gameManager.SetPlayer();
    }

    private void BuyPawn()
    {
        _gameManager.BuyAUnit(Enums.UniteType.Pawn);
    }

    private void BuyKnight()
    {
        _gameManager.BuyAUnit(Enums.UniteType.Knight);
    }

    private void BuyBishop()
    {
        _gameManager.BuyAUnit(Enums.UniteType.Bishop);
    }

    private void BuyRook()
    {
        _gameManager.BuyAUnit(Enums.UniteType.Rook);
    }

    private void BuyQueen()
    {
        _gameManager.BuyAUnit(Enums.UniteType.Queen);
    }

    public void NewRound()
    {
        _finishPreparingBtn.gameObject.SetActive(true);
        Debug.Log("New Round");
    }

    private void SpawnNewEnemy()
    {
        _gameManager.uniteController.SpawnEnemyUnit(Enums.UniteType.Queen);
    }
}
