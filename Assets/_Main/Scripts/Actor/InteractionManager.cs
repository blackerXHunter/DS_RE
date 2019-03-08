using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractionManager : IActorManager {
    public CapsuleCollider interaCol;
    public List<EventCasterManager> ecastmanaList = new List<EventCasterManager>();
	// Use this for initialization
	void Start () {
        interaCol = GetComponent<CapsuleCollider>();
	}
	public UnityAction OnECMEnter, OnECMExit;


    private void OnTriggerEnter(Collider other) {
        EventCasterManager[] ecms = other.GetComponents<EventCasterManager>();
        foreach (var ecm in ecms) {
            if (!ecastmanaList.Contains(ecm)) {
                ecastmanaList.Add(ecm);
                OnECMEnter?.Invoke();
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        EventCasterManager[] ecms = other.GetComponents<EventCasterManager>();
        foreach (var ecm in ecms) {
            if (ecastmanaList.Contains(ecm)) {
                ecastmanaList.Remove(ecm);
                OnECMExit?.Invoke();
            }
        }
    }
}
