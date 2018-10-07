using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionManager : IActorManager {
    public CapsuleCollider interaCol;
	// Use this for initialization
	void Start () {
        interaCol = GetComponent<CapsuleCollider>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerStay(Collider other) {
        EventCasterManager[] ecms = other.GetComponents<EventCasterManager>();
        foreach (var ecm in ecms) {
            print(ecm.eventName);
        }
    }
}
