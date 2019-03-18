using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : IActorManager {

    public Collider weaponColliderL, weaponColliderR;
    public GameObject whL, whR;
    public WeaponController wcL, wcR;

    private void Start() {
        try {
            whR = transform.DeepFind("weaponHandleR").gameObject;
            wcR = BindWeaponController(whR);
            weaponColliderR = whR.GetComponentInChildren<Collider>();
        }
        catch (System.Exception) {
            Debug.LogWarning("right weapon is null");
        }

        try {
            whL = transform.DeepFind("weaponHandleL").gameObject;
            wcL = BindWeaponController(whL);
            weaponColliderL = whL.GetComponentInChildren<Collider>();
        }
        catch (System.Exception) {
            Debug.LogWarning("left weapon is null");
        }

    }

    private WeaponController BindWeaponController(GameObject bindObj) {
        WeaponController tempWc;
        tempWc = bindObj.GetComponent<WeaponController>();
        if (tempWc == null) {
            tempWc = bindObj.AddComponent<WeaponController>();
        }
        tempWc.wm = this;
        return tempWc;
    }

    private void WeaponEnable() {
        weaponColliderR.enabled = true;
        weaponColliderL.enabled = true;
    }
    private void WeaponDisable() {
        weaponColliderR.enabled = false;
        weaponColliderL.enabled = false;
    }

    private void CounterBackEnable() {
        am.SetCounterBack(true);
    }
    private void CounterBackDisable() {
        am.SetCounterBack(false);
    }
    
    private void OnCounterBackExit(){
        am.SetCounterBack(false);
    }

}
