using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using FLAG = System.Boolean;
namespace DS_RE
{

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
        public GameObjectManager cm;
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
            var enemy = cm.LoadEnemy(sceneConfig.enemyPos, scene);
            enemy.GetComponent<DS_RE.BlackAI>().Init();
            var playerController = player.GetComponent<DS_RE.PlayerController>();
            hudm.playerController = playerController;

            var box = cm.LoadBox(Vector3.zero, scene);
            //box.GetComponent<ActorManager>().ac.camCtrl = playerAm.ac.camCtrl;
            box.transform.position = sceneConfig.boxPos;

            var lever = cm.LoadLever(Vector3.zero, scene);
            //lever.GetComponent<ActorManager>().ac.camCtrl = playerAm.ac.camCtrl;
            lever.transform.position = sceneConfig.leverPos;

            hudm.gameObject.SetActive(true);
            yield return new WaitUntil(() => playerController.interactionController != null);
            playerController.interactionController.OnECMEnter += () =>
            {
                foreach (var ecm in playerController.interactionController.ecastControllerList)
                {
                    if (ecm.active)
                    {
                        switch (ecm.ect)
                        {
                            case DS_RE.EventCasterController.EventCasterType.attack:
                                hudm.tp.tipText.text = "处决！";
                                break;
                            case DS_RE.EventCasterController.EventCasterType.item:
                                hudm.tp.tipText.text = "按 E 键拾取";
                                break;
                            case DS_RE.EventCasterController.EventCasterType.lever:
                                hudm.tp.tipText.text = "按 E 键互动";
                                break;
                            case DS_RE.EventCasterController.EventCasterType.box:
                                hudm.tp.tipText.text = "按 E 键开启";
                                break;
                        }
                        hudm.tp.gameObject.SetActive(true);
                    }
                }
            };
            playerController.interactionController.OnECMStay += () =>
            {
                EventCasterController soto_ecm = null;
                foreach (var ecm in playerController.interactionController.ecastControllerList)
                {
                    if (ecm.active)
                    {
                        soto_ecm = ecm;
                        switch (ecm.ect)
                        {
                            case EventCasterController.EventCasterType.attack:
                                hudm.tp.tipText.text = "处决！";
                                break;
                            case EventCasterController.EventCasterType.item:
                                hudm.tp.tipText.text = "按 E 键拾取";
                                break;
                            case EventCasterController.EventCasterType.lever:
                                hudm.tp.tipText.text = "按 E 键互动";
                                break;
                            case EventCasterController.EventCasterType.box:
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
            playerController.interactionController.OnECMExit += () => hudm.tp.gameObject.SetActive(false);

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
}
