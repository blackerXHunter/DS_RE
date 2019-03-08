using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject startUI;
    public GameObject loadingUI;

    internal void OpenStartUI()
    {
       startUI.SetActive(true);
    }

    internal void CloseStartUI()
    {
        startUI.SetActive(false);
    }

    internal void OpenLoadingUI()
    {
        loadingUI.SetActive(true);
    }

    public void SetLoadingValue(float val){
        Text lodingText = loadingUI.GetComponentInChildren<Text>();
        lodingText.text = "loading:"+ ((int) (val*100f)).ToString()+"%";
    }

    internal void CloseLoadingUI()
    {
        loadingUI.SetActive(false);
    }
}
