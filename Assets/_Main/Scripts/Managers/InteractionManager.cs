using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionManager : IActorManager {
    public CapsuleCollider interaCol;
    public List<EventCasterManager> ecastmanaList = new List<EventCasterManager>();
	// Use this for initialization
	void Start () {
        interaCol = GetComponent<CapsuleCollider>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other) {
        EventCasterManager[] ecms = other.GetComponents<EventCasterManager>();
        foreach (var ecm in ecms) {
            if (!ecastmanaList.Contains(ecm)) {
                ecastmanaList.Add(ecm);
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        EventCasterManager[] ecms = other.GetComponents<EventCasterManager>();
        foreach (var ecm in ecms) {
            if (ecastmanaList.Contains(ecm)) {
                ecastmanaList.Remove(ecm);
            }
        }
    }
}
