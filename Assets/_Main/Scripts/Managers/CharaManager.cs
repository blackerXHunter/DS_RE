using System;
using UnityEngine;

public class CharaManager : MonoBehaviour
{
    public GameObject prefab_player;
    public GameObject prefab_enemy;

    internal void LoadPlayer()
    {
        var player = Instantiate(prefab_player);
    }

    internal void LoadEnemy()
    {
        var enemy = Instantiate(prefab_enemy);
    }
}