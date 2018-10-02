using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : IActorManager {

    public Collider weaponColliderL, weaponColliderR;
    public GameObject whL, whR;
    public WeaponController wcL, wcR;

    private void Start() {
        whR = transform.DeepFind("weaponHandleR").gameObject;
        whL = transform.DeepFind("weaponHandleL").gameObject;

        wcR = BindWeaponController(whR);
        wcL = BindWeaponController(whL);

        weaponColliderR = whR.GetComponentInChildren<Collider>();
        weaponColliderL = whL.GetComponentInChildren<Collider>();
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
}
