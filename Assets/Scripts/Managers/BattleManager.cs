using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
public class BattleManager : IActorManager {

    private CapsuleCollider defenseCollider;
    private void OnTriggerEnter(Collider other) {

        if (other.CompareTag("Weapon")) {
            WeaponController wc = other.GetComponentInParent<WeaponController>();

            GameObject attcker = wc.wm.am.gameObject;
            GameObject reciver = am.gameObject;

            float counterAngle1 = Vector3.Angle(reciver.transform.forward, attcker.transform.forward);

            bool attackVeild = true;
            bool counterVeild = Mathf.Abs(counterAngle1 - 180) < 35;
            Debug.Log(counterAngle1);
            am.TryDoDamage(wc, attackVeild, counterVeild);
        }
    }

    // Use this for initialization
    void Start() {
        defenseCollider = GetComponent<CapsuleCollider>();
        defenseCollider.center = new Vector3(0, 1.1f, 0);
        defenseCollider.height = 1.0f;
        defenseCollider.radius = 0.4f;
    }

    // Update is called once per frame
    void Update() {

    }
}
