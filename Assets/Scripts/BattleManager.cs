using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
public class BattleManager : MonoBehaviour {


    public ActorManager am;
    private CapsuleCollider defenseCollider;
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Weapon")) {
            am.DoDamage();
        }
    }

    // Use this for initialization
    void Start () {
        defenseCollider = GetComponent<CapsuleCollider>();
        defenseCollider.center = new Vector3(0,1,0);
        defenseCollider.height = 2.0f;
        defenseCollider.radius = 0.25f;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
