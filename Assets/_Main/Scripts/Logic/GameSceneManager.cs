using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    public List<String> sceneNames;
    internal void LoadScene(int sceneCode, Action OnLoaded)
    {
        StartCoroutine(AsyncLoading(sceneCode, OnLoaded));
    }
    AsyncOperation op;
    IEnumerator AsyncLoading(int sceneCode , Action OnLoaded)
    {
        op = SceneManager.LoadSceneAsync(sceneNames[sceneCode], LoadSceneMode.Additive);

        //阻止当加载完成自动切换  
        //op.allowSceneActivation = false;  

        yield return op;
        OnLoaded?.Invoke();
    }
    public float loadingValue;
    void Update()
    {
        if (op != null)
        {
            loadingValue = op.progress;

            if (op.progress >= 0.9f)
            {
                //operation.progress的值最大为0.9  
                loadingValue = 1.0f;
                FindObjectOfType<GameManager>().LoadingScene = false;
            }
        }
    }
}