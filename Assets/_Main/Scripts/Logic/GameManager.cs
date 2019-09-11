using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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

    public UIManager um;
    public GameSceneManager gsm;
    public CharaManager cm;
    public HUDManager hudm;
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
        yield return gsm.LoadScene();

        var sceneConfig = gsm.currentConfig;

        var scene = SceneManager.GetSceneByName(gsm.sceneNames[gsm.loadSceneIndex]);

        var player = cm.LoadPlayer(sceneConfig.playerPos, scene);
        var enemy = cm.LoadEnemy(sceneConfig.EnemyPos, scene);
        enemy.GetComponent<BlackKnightAI>().Init();
        var playerAm = player.GetComponent<ActorManager>();
        hudm.playerAm = playerAm;

        var box = cm.LoadBox(Vector3.zero, scene);
        //box.GetComponent<ActorManager>().ac.camCtrl = playerAm.ac.camCtrl;
        box.transform.position = Vector3.zero;

        var lever = cm.LoadLever(Vector3.zero, scene);
        //lever.GetComponent<ActorManager>().ac.camCtrl = playerAm.ac.camCtrl;
        lever.transform.position = Vector3.zero + new Vector3(5, 0, 0);

        hudm.gameObject.SetActive(true);
        yield return new WaitUntil(() => playerAm.im != null);
        playerAm.im.OnECMEnter += () =>
        {
            foreach (var ecm in playerAm.im.ecastmanaList)
            {
                if (ecm.active)
                {
                    switch (ecm.ect)
                    {
                        case EventCasterManager.EventCasterType.attack:
                            hudm.tp.tipText.text = "处决！";
                            break;
                        case EventCasterManager.EventCasterType.item:
                            hudm.tp.tipText.text = "按 E 键拾取";
                            break;
                        case EventCasterManager.EventCasterType.lever:
                            hudm.tp.tipText.text = "按 E 键互动";
                            break;
                        case EventCasterManager.EventCasterType.box:
                            hudm.tp.tipText.text = "按 E 键开启";
                            break;
                    }
                    hudm.tp.gameObject.SetActive(true);
                }
            }
        };
        playerAm.im.OnECMStay += () =>
        {
            EventCasterManager soto_ecm = null;
            foreach (var ecm in playerAm.im.ecastmanaList)
            {
                if (ecm.active)
                {
                    soto_ecm = ecm;
                    switch (ecm.ect)
                    {
                        case EventCasterManager.EventCasterType.attack:
                            hudm.tp.tipText.text = "处决！";
                            break;
                        case EventCasterManager.EventCasterType.item:
                            hudm.tp.tipText.text = "按 E 键拾取";
                            break;
                        case EventCasterManager.EventCasterType.lever:
                            hudm.tp.tipText.text = "按 E 键互动";
                            break;
                        case EventCasterManager.EventCasterType.box:
                            hudm.tp.tipText.text = "按 E 键开启";
                            break;
                    }
                    hudm.tp.gameObject.SetActive(true);
                }
            }
            if (soto_ecm == null)
            {
                hudm.tp.gameObject.SetActive(false);
            }
            // else
            // {
            //     hudm.tp.gameObject.SetActive(true);
            // }
        };
        playerAm.im.OnECMExit += () => hudm.tp.gameObject.SetActive(false);

        gameState = GameState.playing;
        um.CloseLoadingUI();
    }

    // Update is called once per frame
    void Update()
    {
        if (LoadingScene)
        {
            um.SetLoadingValue(gsm.loadingValue);
        }
    }
}
