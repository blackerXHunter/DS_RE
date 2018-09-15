using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
public class BattleManager : IActorManager {

    private CapsuleCollider defenseCollider;
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Weapon")) {
            am.TryDoDamage();
        }
    }

    // Use this for initialization
    void Start () {
        defenseCollider = GetComponent<CapsuleCollider>();
        defenseCollider.center = new Vector3(0,1.1f,0);
        defenseCollider.height = 1.0f;
        defenseCollider.radius = 0.4f;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
