using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum GameState{
        ui,
        playing,
        die
    }
    public GameState gameState = GameState.ui;

    public GameObject prefab_player;
    public GameObject prefab_dark_knight;

    public UIManager um;
    // Start is called before the first frame update
    void Start()
    {
        InitStartUI();
    }

    private void InitStartUI()
    {
        um.start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
