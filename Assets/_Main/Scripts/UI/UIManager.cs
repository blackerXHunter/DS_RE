using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject startUI;

    internal void start()
    {
       startUI.SetActive(true);
    }
}
