using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    public List<String> sceneNames;
    public List<SceneConfiguration> configurations;
    public SceneConfiguration currentConfig;
    public static string currentLevelSceneName;
    public int loadSceneIndex = 0;
    public IEnumerator LoadScene()
    {
        yield return LoadScene(loadSceneIndex);
        currentConfig = configurations[loadSceneIndex];
        currentLevelSceneName = sceneNames[loadSceneIndex];
    }
    private IEnumerator LoadScene(int sceneCode)
    {
        yield return AsyncLoading(sceneCode);
    }
    AsyncOperation op;
    IEnumerator AsyncLoading(int sceneCode )
    {
        op = SceneManager.LoadSceneAsync(sceneNames[sceneCode], LoadSceneMode.Additive);

        //阻止当加载完成自动切换  
        //op.allowSceneActivation = false;  
        
        yield return op;
    }
    [HideInInspector]
    public float loadingValue;
    void Update()
    {
        if (op != null)
        {
            loadingValue = op.progress;
        }
    }
}