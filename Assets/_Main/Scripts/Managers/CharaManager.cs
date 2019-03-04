using System;
using UnityEngine;

public class CharaManager : MonoBehaviour
{
    public GameObject prefab_player;
    public GameObject prefab_enemy;

    internal GameObject LoadPlayer()
    {
        var player = Instantiate(prefab_player);
        return player;
    }

    internal GameObject LoadEnemy()
    {
        var enemy = Instantiate(prefab_enemy);
        return enemy;
    }
}