using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour {
    public Collider weaponCollider;
    public ActorManager am;
    public GameObject whL, whR;

    private void Start() {
        whR = transform.DeepFind("weaponHandleR").gameObject;
        whL = transform.DeepFind("weaponHandleL").gameObject;

        weaponCollider = whR.GetComponentInChildren<Collider>();
    }

    private void WeaponEnable() {
        weaponCollider.enabled = true;
    }
    private void WeaponDisable() {
        weaponCollider.enabled = false;
    }
}
