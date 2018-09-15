using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : IActorManager {

    public Collider weaponColliderL, weaponColliderR;
    public GameObject whL, whR;

    private void Start() {
        whR = transform.DeepFind("weaponHandleR").gameObject;
        whL = transform.DeepFind("weaponHandleL").gameObject;

        weaponColliderR = whR.GetComponentInChildren<Collider>();
        weaponColliderL = whL.GetComponentInChildren<Collider>();
    }

    private void WeaponEnable() {
        weaponColliderR.enabled = true;
        weaponColliderL.enabled = true;
    }
    private void WeaponDisable() {
        weaponColliderR.enabled = false;
        weaponColliderL.enabled = false;
    }
}
