using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FLAG = System.Boolean;
public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        ui,
        playing,
        die
    }
    public GameState gameState = GameState.ui;

    public GameObject prefab_player;
    public GameObject prefab_dark_knight;

    public UIManager um;
    public GameSceneManager gsm;
    public CharaManager cm;
    public FLAG LoadingScene = false;
    // Start is called before the first frame update
    void Start()
    {
        InitStartUI();
    }

    private void InitStartUI()
    {
        um.OpenStartUI();
    }
    public void StartGame()
    {
        StartCoroutine(StartGame_Coro());
    }

    private IEnumerator StartGame_Coro()
    {
        um.CloseStartUI();
        um.OpenLoadingUI();
        gsm.LoadScene(0, () => um.CloseLoadingUI());
        LoadingScene = true;
        yield return new WaitUntil(()=>LoadingScene == false);
        cm.LoadPlayer();
        cm.LoadEnemy();
    }

    // Update is called once per frame
    void Update()
    {
        if (LoadingScene)
        {
            Debug.Log(gsm.loadingValue);
            um.SetLoadingValue(gsm.loadingValue);
        }
    }
}
