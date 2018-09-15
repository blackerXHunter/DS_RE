using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : IActorManager {

    private void Start() {
        HP = Mathf.Clamp(HP, 0, HPMAX);
    }

    private float HP = 15.0f;
    private float HPMAX = 25.0f;
    public void AddHP(float value) {
        HP += value;
        HP = Mathf.Clamp(HP, 0, HPMAX);
    }

    public bool IsDie
    {
        get
        {
            return HP == 0;
        }
    }
}
