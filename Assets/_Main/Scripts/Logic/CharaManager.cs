using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharaManager : MonoBehaviour
{
    public GameObject prefab_player;
    public GameObject prefab_enemy;

    public GameObject prefab_box;

    public GameObject prefab_lever;

    internal GameObject LoadPlayer(Vector3 Pos, Scene scene)
    {
        var player = Instantiate(prefab_player);
        if (scene != null)
        {
            SceneManager.MoveGameObjectToScene(player.gameObject, scene);
        }
        player.transform.position = Pos;
        return player;
    }

    internal GameObject LoadEnemy(Vector3 Pos, Scene scene)
    {
        var enemy = Instantiate(prefab_enemy);
        if (scene != null)
        {
            SceneManager.MoveGameObjectToScene(enemy.gameObject, scene);
        }
        enemy.transform.position = Pos;
        return enemy;
    }

    public GameObject LoadBox(Vector3 Pos, Scene scene)
    {
        var box = Instantiate(prefab_box);
        if (scene != null)
        {
            SceneManager.MoveGameObjectToScene(box.gameObject, scene);
        }
        box.transform.position = Pos;
        return box;
    }

    public GameObject LoadLever(Vector3 Pos, Scene scene)
    {
        var lever = Instantiate(prefab_lever);
        if (scene != null)
        {

            SceneManager.MoveGameObjectToScene(lever.gameObject, scene);
        }
        lever.transform.position = Pos;
        return lever;
    }
}