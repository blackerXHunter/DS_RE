using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour {
    public WeaponManager wm;
    public DS_RE.WeaponData wdata;

    // Use this for initialization
    private void Awake() {
        wdata = GetComponentInChildren<DS_RE.WeaponData>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public float GetAtk() {
        if (wdata == null)
        {
            Debug.Log(gameObject.name+ " has not wdata !");
        }
        return wdata.ATK + wm.am.sm.ATK;
    }
}
