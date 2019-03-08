using System;
using UnityEngine;

public class CharaManager : MonoBehaviour
{
    public GameObject prefab_player;
    public GameObject prefab_enemy;

    public GameObject prefab_box;

    public GameObject prefab_lever;

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

    public GameObject LoadBox(){
        var box = Instantiate(prefab_box);
        return box;
    }

    public GameObject LoadLever(){
        var lever = Instantiate(prefab_lever);
        return lever;
    }
}