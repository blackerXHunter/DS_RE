using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour {
    public WeaponManager wm;
    public WeaponData wdata;

    // Use this for initialization
    private void Awake() {
        wdata = GetComponentInChildren<WeaponData>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public float GetAtk() {
        return wdata.ATK + wm.am.sm.ATK;
    }
}
