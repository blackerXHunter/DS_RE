using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
public class BattleManager : MonoBehaviour {

    [SerializeField]
    private Animator animator;

    private void OnTriggerEnter(Collider other) {
        animator.SetTrigger("damage");
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
